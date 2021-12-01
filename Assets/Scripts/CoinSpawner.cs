using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private Transform _pointsParent;
    [SerializeField] private Wallet _wallet;

    private Transform[] _points;

    private void OnEnable()
    {
        _wallet.CoinPickedUp += Spawn;
    }

    private void OnDisable()
    {
        _wallet.CoinPickedUp -= Spawn;
    }

    private void Start()
    {
        _points = new Transform[_pointsParent.childCount];

        for (int i = 0; i < _pointsParent.childCount; i++)
        {
            _points[i] = _pointsParent.GetChild(i);
        }

        Spawn();
    }

    private void Spawn()
    {
        int index = Random.Range(0, _points.Length);

        Instantiate(_coin, _points[index].position, Quaternion.identity);
    }
}