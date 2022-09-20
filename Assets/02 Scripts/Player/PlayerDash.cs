using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private int _maxDashCnt;
    [SerializeField] private float _dashCooltime;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _destinationBlockedOffset;
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashDistanceOffset;
    [SerializeField] private float _effectDurationOffset;
    [SerializeField] private DashEffect _dashEffect;

    private Vector3 _movementInput = Vector3.forward;

    [SerializeField] private int _dashCnt;
    private bool _isWaiting;

    private bool _useCoroutine;

    [SerializeField] private LayerMask _cantThroughLayer;

    private Camera _mainCam;

    public UnityEvent OnDashFeedBack;
    public UnityEvent OnBlockedDash;
    public UnityEvent OnDashEnd;

    private bool _isBlocked;



    private void Awake()
    {
        _mainCam = Define.MainCam;
    }

    private void Start()
    {
        if (!_useCoroutine && _dashCnt < _maxDashCnt)
        {
            _useCoroutine = true;
            StartCoroutine(DashCountCharging());
        }
        GameManager.Inst.UI.SetDashCount(_dashCnt);
    }

    public void MovementInput(Vector3 movement)
    {
        _movementInput = movement;
    }

    public void StartDash()
    {
        if (_isWaiting) return;
        if (_dashCnt <= 0) return;

        _isWaiting = true;
        SetDashCount();

        Vector3 currentPos = transform.position;

        Vector3 targetDir = CalulationTargetDir();
        Vector3 destination = CalculationDestination(targetDir, currentPos);
        float duration = CalulationDuration(destination, currentPos);

        if (_isBlocked)
        {
            OnBlockedDash?.Invoke();
        }
        OnDashFeedBack?.Invoke();
        _dashEffect.StartEffect(targetDir, duration + _effectDurationOffset);

        transform.DOMove(destination, duration).SetEase(Ease.InCubic).OnComplete(Release);
    }

    private void Release()
    {
        _isWaiting = false;
        OnDashEnd?.Invoke();
    }

    private void SetDashCount()
    {
        _dashCnt--;
        GameManager.Inst.UI.SetDashCount(_dashCnt);

        if (!_useCoroutine)
        {
            _useCoroutine = true;
            StartCoroutine(DashCountCharging());
        }
    }

    private float CalulationDuration(Vector3 destination, Vector3 currentPos)
    {

        currentPos.y = 0f;

        float duration = Vector3.Distance(currentPos, destination) / _dashSpeed;

        return duration;
    }

    private Vector3 CalulationTargetDir()
    {
        Vector3 forward = _mainCam.transform.TransformDirection(Vector3.forward);
        forward.y = 0f;

        if (_movementInput == Vector3.zero)
        {
            return forward;
        }

        Vector3 right = new Vector3(forward.z, 0f, -forward.x);
        Vector3 targetDir = forward * _movementInput.z + right * _movementInput.x;

        return targetDir;

    }

    private Vector3 CalculationDestination(Vector3 targetDir, Vector3 currentPos)
    {
        RaycastHit hit;
        Vector3 destination;
        Vector3 topPos = transform.position + Vector3.up * 1.5f;
        Vector3 middlePos = transform.position + Vector3.up * 0.9f;
        Vector3 bottomPos = transform.position + Vector3.up * 0.2f;

        Debug.DrawRay(topPos, targetDir.normalized * (_dashDistance + _dashDistanceOffset), Color.blue, 3f);
        Debug.DrawRay(middlePos, targetDir.normalized * (_dashDistance + _dashDistanceOffset), Color.blue,3f);
        Debug.DrawRay(bottomPos, targetDir.normalized * (_dashDistance + _dashDistanceOffset), Color.blue,3f);

        if (Physics.Raycast(bottomPos, targetDir.normalized, out hit, _dashDistance + _dashDistanceOffset, _cantThroughLayer) ||
            Physics.Raycast(middlePos, targetDir.normalized, out hit, _dashDistance + _dashDistanceOffset, _cantThroughLayer) ||
            Physics.Raycast(topPos, targetDir.normalized, out hit, _dashDistance + _dashDistanceOffset, _cantThroughLayer))
        {
            Debug.Log("blocked");
            _isBlocked = true;

            Vector3 hitPoint = hit.point;
            Vector3 originPos = currentPos;
            originPos.y = 0f;
            hitPoint.y = 0f;

            if(hit.collider.gameObject.layer == LayerMask.GetMask("Stair"))
            {
                hitPoint = hit.collider.transform.root.Find("DashCollider").position;
                hitPoint.y = 0f;
            }

            float distance = Vector3.Distance(originPos, hitPoint);
            destination = currentPos + targetDir.normalized * ( distance - _destinationBlockedOffset);
        }
        else
        {
            _isBlocked = false;
            destination = currentPos + targetDir.normalized * _dashDistance;
        }



        return destination;
    }

    private IEnumerator DashCountCharging()
    {
        while (_dashCnt < _maxDashCnt)
        {
            GameManager.Inst.UI.StartDashCoolTime(_dashCooltime);
            yield return new WaitForSeconds(_dashCooltime);

            _dashCnt++;
            GameManager.Inst.UI.SetDashCount(_dashCnt);
        }

        _useCoroutine = false;
    }

}
