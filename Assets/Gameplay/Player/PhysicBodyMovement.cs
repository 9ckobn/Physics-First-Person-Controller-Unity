using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PhysicBodyMovement : MonoBehaviour, IMovable
{
    private const int GroundLayerMask = 1 << 6;

    private IInputService _inputService;
    private PlayerSettings _settigns;

    private Rigidbody _rb;

    private float _groundDistanceToJump = 1.1f;
    private Vector2 defaultGravity = new Vector3(0, -9.81f);

    private float _currentSpeed;

    [Inject]
    private void Construct(IInputService inputService, PlayerSettings settings)
    {
        _inputService = inputService;
        _settigns = settings;

        SetupRigidbody();
        SetupInput();

        _groundDistanceToJump = GetComponent<Collider>().bounds.center.y + 0.1f;
        _currentSpeed = settings.Speed;
    }

    private void SetupInput()
    {
        _inputService.OnJump += Jump;
        _inputService.OnSprintStart += () => _currentSpeed = _settigns.Speed * 1.4f;
        _inputService.OnSprintEnd += () => _currentSpeed = _settigns.Speed;
    }

    private void SetupRigidbody()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.mass = _settigns.PlayerMass;
        _rb.drag = _settigns.DefaultDrag;
    }

    public void Jump()
    {
        if (!IsGrounded())
            return;

        _rb.drag = 2;
        _currentSpeed = _settigns.Speed / 3f;
        Physics.gravity *= _settigns.GravityScale;

        float jumpForce = Mathf.Sqrt(_settigns.JumpHeight * Physics.gravity.y * -2) * _rb.mass;

        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void Move()
    {
        _rb.AddForce(GetMoveDirection(), ForceMode.Acceleration);
    }

    void FixedUpdate()
    {
        Move();
    }

    Vector3 GetMoveDirection()
    {
        return (transform.forward * _inputService.MoveDirection.z + transform.right * _inputService.MoveDirection.x).normalized * _currentSpeed;
    }

    public bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo, _groundDistanceToJump, GroundLayerMask))
        {
            if (hitInfo.collider)
            {
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (IsGrounded())
        {
            Physics.gravity = defaultGravity;
            _currentSpeed = _settigns.Speed;
            _rb.drag = _settigns.DefaultDrag;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * _groundDistanceToJump);
    }

    void OnDisable()
    {
        _inputService.OnJump -= Jump;
    }
}
