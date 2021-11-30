using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    [SerializeField] private UnityEvent _killed;

    private Movement _movement;
    private const string Horizontal = "Horizontal";
    private Vector3 _startPosition;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _startPosition = transform.position;
    }

    private void Update()
    {
        float direction = Input.GetAxis(Horizontal);

        if (Input.GetKey(KeyCode.LeftShift))
            _movement.Move(new Vector2(direction, 0), true);
        else
            _movement.Move(new Vector2(direction, 0));

        if (Input.GetKeyDown(KeyCode.Space))
            _movement.ToJump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            transform.position = _startPosition;
            _killed.Invoke();
        }
    }
}