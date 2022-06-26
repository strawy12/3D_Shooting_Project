using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveTrigger : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private int _number;

    private static Transform _targetTrs;

    private bool _isEnter;
    private void Awake()
    {
        if (_targetTrs == null)
            _targetTrs = Define.PlayerRef;
    }

    public void Update()
    {
        if(Vector3.Distance(_targetTrs.position, transform.position) <= _distance)
        {
            if(_isEnter == false)
            {
                _isEnter = true;
                Param p = new Param();
                p.iParam = _number;
                p.sParam = "Arrived";
                PEventManager.TriggerEvent(Constant.ARRIVED_TARGET_POS, p);
            }
        }

        else
        {
            _isEnter = false;
        }
    }

}
