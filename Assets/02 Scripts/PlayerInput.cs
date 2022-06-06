using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector3> OnMovementKeyPress;

    public UnityEvent OnFireButtonPress;
    public UnityEvent OnFireButtonRelease;
    public UnityEvent OnReloadButtonPress;

    private bool _fireButtonDown = false;

    void Update()
    {
        GetMovementInput();
        GetFireInput();
    }

    private void GetMovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        OnMovementKeyPress?.Invoke(new Vector3(h, 0f, v));
    }

    private void GetFireInput()
    {
        if(Input.GetMouseButton(0))
        {
            OnFireButtonPress?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnFireButtonRelease?.Invoke();
        }
    }
}
