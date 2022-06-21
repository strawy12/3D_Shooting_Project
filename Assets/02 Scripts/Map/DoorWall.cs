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


public class DoorWall : MonoBehaviour
{
    [SerializeField] private List<Door> _doorList;
    [SerializeField] private float _duration;

    [ContextMenu("Open")]
    public void OpenDoor()
    {
        Vector3 rot = Vector3.zero;
        foreach(var door in _doorList)
        {
            rot = door.transform.eulerAngles;
            rot.y += door.openAngle;
            door.transform.DORotate(rot, _duration);
        }
    }

    [ContextMenu("Close")]
    public void CloseDoor()
    {
        Vector3 rot = Vector3.zero;
        foreach (var door in _doorList)
        {
            door.transform.DORotate(rot, _duration);
        }
    }


}
