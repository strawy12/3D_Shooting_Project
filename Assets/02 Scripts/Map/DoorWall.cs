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
    [SerializeField] private bool _needKey;

    private bool _isOpen;
    private bool _takingAction;

    public void OpenDoor()
    {
        Vector3 rot = Vector3.zero;
        foreach(var door in _doorList)
        {
            rot = door.transform.eulerAngles;
            rot.y += door.openAngle;
            door.transform.DORotate(rot, _duration).OnComplete(EndAction);
        }
    }

    public void CloseDoor()
    {
        Vector3 rot = Vector3.zero;
        foreach (var door in _doorList)
        {
            door.transform.DORotate(rot, _duration).OnComplete(EndAction);
        }
    }

    private void EndAction()
    {
        _takingAction = false;
    }

    public override void TakeAction()
    {
        if (_takingAction) return;
        if (_needKey) return;
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

    public override string GetInteractionText()
    {
        if (_needKey)
            return "����ִ�";

        return _isOpen ? "�� �ݱ�" : "�� ����";
    }
}
