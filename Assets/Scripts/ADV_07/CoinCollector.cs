using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public int Coins { get; private set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
            CollectCoin(coin);
    }

    private void CollectCoin(Coin coin)
    {
        Coins++;
        coin.gameObject.SetActive(false);
    }

    public void Empty()
    {
        Coins = 0;
    }
}
