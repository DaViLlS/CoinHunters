using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnRange;
    [SerializeField] private int coinsCount;
    [SerializeField] private GameObject coinPref;

    private void Start()
    {
        PlayerGameConnector.Instance.onGameStarted += SpawnCoins;
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < coinsCount; i++)
        {
            float deltaX = Random.Range(-spawnRange.x / 2, spawnRange.x / 2);
            float deltaY = Random.Range(-spawnRange.y / 2, spawnRange.y / 2);

            PhotonNetwork.InstantiateRoomObject(coinPref.name, new Vector2(deltaX, deltaY), Quaternion.identity).transform.SetParent(transform);
        }
    }
}
