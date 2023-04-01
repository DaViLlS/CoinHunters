using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CointCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCountText;

    private void Start()
    {
        PlayerGameConnector.Instance.onPlayerCreated += SetCoinCollectEvent;
        coinCountText.text = "0";
    }

    private void SetPlayerCoinCountText(int coinCount)
    {
        coinCountText.text = coinCount.ToString();
    }

    private void SetCoinCollectEvent(PlayerCoinCollector playerCoin)
    {
        playerCoin.onCoinCollected += SetPlayerCoinCountText;
    }
}
