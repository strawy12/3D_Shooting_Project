using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionObject : MonoBehaviour
{
    /// <summary>
    /// Radius
    /// </summary>
    [SerializeField] protected float _detactRange = 3;
    [SerializeField] protected float _detactStartRange = 10;
    [SerializeField] protected string _interactionText;

    protected virtual void Update()
    {
        if (CanDetact() == false) return;

        Detact();
    }

    private bool CanDetact()
    {
        Vector3 playerPos = Define.PlayerRef.position;
        playerPos.y = 0f;
        Vector3 currentPos = transform.position;
        currentPos.y = 0f;
        return Vector3.Distance(playerPos, currentPos) <= _detactStartRange;
    }

    public abstract void TakeAction();

    public virtual string GetInteractionText()
    {
        return _interactionText;
    }

    private void Detact()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, _detactRange, LayerMask.GetMask("Player"));
        
        if(col.Length != 0)
        {
            GameManager.Inst.UI.ShowInteractionUI(GetInteractionText(), this);
        }

        else
        {
            GameManager.Inst.UI.UnShowInteractionUI(this);
        }
    }
}
