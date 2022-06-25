using System;
using UnityEngine;

public class PlayerMovement : AgentMovement
{
    [SerializeField] private float _jumpPower = 5f;
    protected Rigidbody _rigid;
    protected Collider _collider;

    private bool _isDash;

    private bool _isJump;

    protected override void ChildAwake()
    {
        _rigid = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    protected override void ChildUpdate()
    {
        if (_isDash) return;

        Vector3 velocity = _currentDir;
        velocity.x *= _currentVelocity;
        velocity.y = _rigid.velocity.y;
        velocity.z *= _currentVelocity;

        _rigid.velocity = velocity;
        ChangeBody();

        if(_isJump && IsGround())
        {
            _isJump = false;
        }
  
    }

    

    public void ChangeDashState(bool isDash)
    {
        _isDash = isDash;
    }

    private void ChangeBody()
    {
        if (_currentVelocity > 0f)
        {
            Vector3 newForward = _rigid.velocity;
            newForward.y = 0f;

            transform.forward = Vector3.Lerp(transform.forward, newForward, _movementData.turnSpeed * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if (_isDash) return;
        if (_isJump) return;

        if (IsGround())
        {
            _rigid.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            _isJump = true;
        }
    }

    protected override Vector3 GetForward()
    {
        return Define.MainCam.transform.TransformDirection(Vector3.forward);
    }



    protected bool IsGround()
    {
        Vector3 pos = new Vector3(_collider.bounds.center.x, _collider.bounds.min.y, _collider.bounds.center.z);
        Vector3 size = _collider.bounds.size * 0.5f;
        size.y = 0.1f;
        return Physics.OverlapBox(pos, size, Quaternion.identity).Length > 1;
    }

    public override void ImmediatelyForwardBody()
    {
        if (_currentVelocity > 0f) return;
        Vector3 forward = GetForward();
        forward.y = 0f;
        transform.forward = forward;
    }



    public override void MovementInput(Vector3 movementInput)
    {
        if (movementInput.sqrMagnitude > 0)
        {
            // Define static 으로 만들기
            Vector3 forward = GetForward();
            forward.y = 0f;


            Vector3 right = new Vector3(forward.z, 0f, -forward.x);

            Vector3 targetDir = forward * movementInput.z + right * movementInput.x;

            Vector3 currentDir = _currentDir;
            currentDir.y = 0f;

            if (Vector3.Dot(currentDir, targetDir) < 0)
            {
                _currentVelocity = 0f;
            }
            _currentDir = Vector3.RotateTowards(_currentDir, targetDir, _movementData.rotateMoveSpeed * Time.deltaTime, 1000f);
            _currentDir.Normalize();
        }
        _currentVelocity = CalculateSpeed(movementInput);
    }
}
