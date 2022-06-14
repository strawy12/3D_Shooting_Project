using UnityEngine;

public class PlayerMovement : AgentMovement
{
    [SerializeField] private float _jumpPower = 5f;

    protected override void ChildAwake()
    {
        _collider = GetComponent<Collider>();
    }
    public void Jump()
    {
        if (IsGround())
        {
            _rigid.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
    }

    protected override Vector3 GetForward()
    {
        return Define.MainCam.transform.TransformDirection(Vector3.forward);
    }
}
