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
    [Range(0.1f, 1f)]
    [SerializeField] private float _destinationMultiplier;
    [SerializeField] private float _dashDistance;

    private Vector3 _movementInput = Vector3.forward;

    [SerializeField] private int _dashCnt;
    private bool _isWaiting;

    private bool _useCoroutine;

    [SerializeField] private LayerMask _cantThroughLayer;

    private Camera _mainCam;

    public UnityEvent OnDashFeedBack;
    public UnityEvent OnBlockedDash;

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
    }

    public void MovementInput(Vector3 movement)
    {
        if (movement == Vector3.zero) return;

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

        if(_isBlocked)
        {
            OnBlockedDash?.Invoke();
        }
        OnDashFeedBack?.Invoke();
        transform.DOMove(destination, duration).SetEase(Ease.InCubic).OnComplete(() => _isWaiting = false);
    }

    private void SetDashCount()
    {

        _dashCnt--;

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
        Vector3 right = new Vector3(forward.z, 0f, -forward.x);
        Vector3 targetDir = forward * _movementInput.z + right * _movementInput.x;

        return targetDir;

    }

    private Vector3 CalculationDestination(Vector3 targetDir, Vector3 currentPos)
    {
        RaycastHit hit;
        Vector3 destination;

        if (Physics.Raycast(transform.position, targetDir.normalized, out hit, _dashDistance, _cantThroughLayer))
        {
            _isBlocked = true;
            destination = hit.point * _destinationMultiplier;
        }
        else
        {
            _isBlocked = false;
            destination = targetDir.normalized * _dashDistance;
        }

        Debug.Log($"currentPos : {currentPos}");
        Debug.Log($"destination : {destination}");
        Debug.Log($"currentPos + destination : {currentPos + destination}");



        return currentPos + destination;
    }

    private IEnumerator DashCountCharging()
    {
        while(_dashCnt < _maxDashCnt)
        {
            yield return new WaitForSeconds(_dashCooltime);

            _dashCnt++;
        }

        _useCoroutine = false;
    }

}
