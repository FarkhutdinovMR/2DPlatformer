using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private UnityEvent _jumped;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private float _currentSpeed;
    private bool _rightDirection;

    private const string Speed = "Speed";
    private const string Jump = "Jump";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction, bool run = false)
    {
        if (run)
            _currentSpeed = _runSpeed;
        else
            _currentSpeed = _walkSpeed;

        transform.Translate(_currentSpeed * direction * Time.deltaTime);
        
        if (CheckGround())
            _animator.SetFloat(Speed, Mathf.Abs(_currentSpeed * direction.x));
        else
            _animator.SetFloat(Speed, 0f);

        if ((direction.x > 0 && _rightDirection) ||
            (direction.x < 0 && _rightDirection == false))
            Flip();
    }

    public void ToJump()
    {
        if (CheckGround())
        {
            _rigidbody2D.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
            _animator.SetTrigger(Jump);
            _jumped.Invoke();
        }
    }

    private bool CheckGround()
    {
        RaycastHit2D[] raycastHit2D = new RaycastHit2D[1];
        int results = _rigidbody2D.Cast(Vector2.down, raycastHit2D, _groundCheckDistance);

        if (results > 0)
            return true;
        else
            return false;
    }

    private void Flip()
    {
        _rightDirection = !_rightDirection;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}