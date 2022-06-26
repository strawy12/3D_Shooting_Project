using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

[System.Serializable]
public class Door
{
    public Transform transform;
    public float openAngle;
}


public class DoorWall : InteractionObject
{
    [SerializeField] private List<Door> _doorList;
    [SerializeField] private float _duration;
    [SerializeField] private bool _isLock;

    private DoorSound _doorSound;
    private bool _isOpen;
    private bool _takingAction;

    private void Awake()
    {
        _doorSound = GetComponent<DoorSound>();
    }

    public void OpenDoor()
    {
        Vector3 rot = Vector3.zero;
        foreach(var door in _doorList)
        {
            rot = door.transform.eulerAngles;
            rot.y += door.openAngle;
            door.transform.DORotate(rot, _duration).OnComplete(EndAction);
        }
        _doorSound.PlayOpenSound();
    }

    public void CloseDoor()
    {
        Vector3 rot = Vector3.zero;
        foreach (var door in _doorList)
        {
            door.transform.DORotate(rot, _duration).OnComplete(EndAction);
        }
        _doorSound.PlayCloseSound();
    }

    private void EndAction()
    {
        _takingAction = false;
    }

    public override void TakeAction()
    {
        if (_takingAction) return;
        if (_isLock)
        {
            Param p = new Param();
            p.iParam = 0;
            PEventManager.TriggerEvent(Constant.ON_TARGET_ACTION, p);
           _doorSound.PlayLockSound();
            return;
        }
        _takingAction = true;
        _isOpen = !_isOpen;

        if(_isOpen)
        {
            OpenDoor();
        }

        else
        {
            CloseDoor();
        }
    }

    public void UnLockDoor()
    {
        _isLock = false;
    }



    public override string GetInteractionText()
    {
        if (_isLock)
            return "잠겨있다";

        return _isOpen ? "문 닫기" : "문 열기";
    }
}
