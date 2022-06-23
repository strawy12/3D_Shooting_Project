using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCrouch : MonoBehaviour
{
    [SerializeField] private float _cooltimeDelay;
    [SerializeField] private float _crouchDelay;

    public UnityEvent CrouchFeedback;
    public UnityEvent EndCrouch;

    private bool _canCrouch = true;



    public void Crouch()
    {
        if (!CanCrouch()) return;
        _canCrouch = false;


        CrouchFeedback?.Invoke();
        StartCoroutine(CrouchCoroutine());
    }

    public bool CanCrouch()
    {
        return _canCrouch && GameManager.Inst.Data.GetItemCount(EItemType.Trap) != 0;
    }

    private IEnumerator CrouchCoroutine()
    {
        yield return new WaitForSeconds(_crouchDelay);

        Vector3 direction = Camera.main.transform.TransformDirection(Vector3.forward);
        direction.y = 0f;

        Trap trap = PoolManager.Inst.Pop("Trap") as Trap;

        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 999f, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            trap.transform.position = hit.point;
        }

        else
        {
            trap.transform.position = transform.position;
        }

        GameManager.Inst.SubItemCount(EItemType.HoshiTan);

        StartCoroutine(CooltimeDelay());
    }

    private IEnumerator CooltimeDelay()
    {
        yield return new WaitForSeconds(_cooltimeDelay);
        _canCrouch = true;
    }
}
