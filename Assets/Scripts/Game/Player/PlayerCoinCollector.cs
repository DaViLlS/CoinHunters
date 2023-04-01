using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinCollector : MonoBehaviour
{
    private int _coinCount;

    public System.Action<int> onCoinCollected;

    private void Awake()
    {
        _coinCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            _coinCount++;
            onCoinCollected?.Invoke(_coinCount);
            Destroy(collision.gameObject);
        }
    }
}
