using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    [SerializeField] private UnityEvent _coinPickedUp;

    public event UnityAction CoinPickedUp
    {
        add => _coinPickedUp.AddListener(value);
        remove => _coinPickedUp.RemoveListener(value);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Coin>(out Coin coin))
        {
            _coinPickedUp.Invoke();
            Destroy(coin.gameObject);
        }
    }
}