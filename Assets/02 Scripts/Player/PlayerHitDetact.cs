using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHitDetact : MonoBehaviour
{
    [SerializeField] private float _radius;
    [Range(0f, 360f)] [SerializeField] private float _angle;
    private int _attackCnt;

    [SerializeField] private int _attackDamage;
    [SerializeField] private float _delayTime;

    LayerMask _targetMask;

    public UnityEvent OnSuccessAttack;

    private void Awake()
    {
        _targetMask = LayerMask.GetMask("Enemy");
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DetactSetting(int attackCnt)
    {
        _attackCnt = attackCnt;

        switch (_attackCnt)
        {
            case 1:
                _radius = 1.6f;
                _angle = 210f;
                break;

            case 2:
                _radius = 2f;
                _angle = 360f;
                break;

            case 3:
    
                _radius = 1f;
                _angle = 110f;
                break;
        }

        StartCoroutine(DelayDetact());
    }

    private IEnumerator DelayDetact()
    {
        yield return new WaitForSeconds(_delayTime);
        OverlapHitRange();
    }

    public void OverlapHitRange()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        List<Transform> attackFinishTrs = new List<Transform>();

        foreach (var target in rangeChecks)
        {
            if (attackFinishTrs.Contains(target.transform.root)) continue;

            attackFinishTrs.Add(target.transform.root);

            Vector3 targetPos = target.transform.root.position +Vector3.up* (target.transform.root.localScale.y *0.5f);
            Vector3 currentPos = transform.position;
            Vector3 directionToTarget = (targetPos - currentPos).normalized;

            float dotAngle = Vector3.Dot(transform.forward, directionToTarget);

            if (dotAngle > Mathf.Cos((_angle / 2) * Mathf.Deg2Rad))
            {
                float distanceToTarget = Vector3.Distance(currentPos, targetPos);
                RaycastHit hit;
                Physics.Raycast(currentPos, directionToTarget, out hit, distanceToTarget);

                Debug.DrawRay(currentPos, directionToTarget * distanceToTarget, Color.green, 3f);

                if (hit.collider != null && hit.transform.root.gameObject.layer == 9)
                {

                    IHittable hitTarget = hit.transform.root.GetComponent<IHittable>();

                    hitTarget.HitPoint = hit.point;

                    hitTarget?.GetHit(_attackDamage * _attackCnt, gameObject);
                    OnSuccessAttack?.Invoke();
                }
            }
        }
    }



    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, _radius);

    //    float angle1 = _angle / 2 + transform.eulerAngles.y;
    //    float angle2 = -_angle / 2 + transform.eulerAngles.y;

    //    Vector3 Driection1 = new Vector3(Mathf.Sin(angle1 * Mathf.Deg2Rad), 0f, Mathf.Cos(angle1 * Mathf.Deg2Rad));
    //    Vector3 Driection2 = new Vector3(Mathf.Sin(angle2 * Mathf.Deg2Rad), 0f, Mathf.Cos(angle2 * Mathf.Deg2Rad));

    //    Gizmos.DrawLine(transform.position, transform.position + Driection1 * _radius);
    //    Gizmos.DrawLine(transform.position, transform.position + Driection2 * _radius);

    //}
}
