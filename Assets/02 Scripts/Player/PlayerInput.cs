using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector3> OnMovementKeyPress;

    public UnityEvent OnFireButtonPress;
    public UnityEvent OnFireButtonRelease;
    public UnityEvent OnJumpButtonPressDown;

    public UnityEvent<bool> OnRunButtonState;
    public UnityEvent OnThrowButtonPress;
    public UnityEvent OnThrowButtonRelease;
    public UnityEvent OnThrowCancel;

    private bool _fireButtonDown = false;


    void Update()
    {
        GetRunKeyInput();
        GetMovementInput();
        GetFireInput();
        GetJumpInput();
        GetThrowInput();
    }

    private void GetRunKeyInput()
    {
        bool state = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            state = false;
        }

        OnRunButtonState?.Invoke(state);
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

    private void GetJumpInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpButtonPressDown?.Invoke();
        }
    }

    private void GetThrowInput()
    {
        if (Input.GetKey(KeyCode.E))
        {
            OnThrowButtonPress?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (Input.GetMouseButton(1))
            {
                OnThrowCancel?.Invoke();
            }

            else
            {
                OnThrowButtonRelease?.Invoke();
            }
        }


    }
}
