using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;
    [SerializeField] private float _stoppingDistance;

    private Movement _movement;
    private Transform[] _points;
    private int _currentPoint;

    private void Start()
    {
        _movement = GetComponent<Movement>();

        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        Transform target = _points[_currentPoint];
        Vector2 direction = (target.position - transform.position).normalized;

        _movement.Move(direction);

        if (Vector3.Distance(transform.position, target.position) <= _stoppingDistance)
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
                _currentPoint = 0;
        }
    }
}