using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI moneyText;

    public void SetWinPanel(string name, int moneyCount)
    {
        gameObject.SetActive(true);
        playerName.text = "Победил" + name;
        moneyText.text = moneyCount.ToString();
    }
}
