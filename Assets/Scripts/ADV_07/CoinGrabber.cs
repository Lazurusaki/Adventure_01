using UnityEngine;

public class CoinGrabber : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            coin.gameObject.SetActive(false);
        }
    }
}
