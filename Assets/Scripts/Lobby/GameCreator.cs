using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCreator : MonoBehaviour
{
    [SerializeField] private TMP_InputField gameName;
    [SerializeField] private GameObject warningObject;

    public void CreateGame()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (gameName.text == "")
            {
                gameName.placeholder.GetComponent<TextMeshProUGUI>().color = Color.red;
                warningObject.SetActive(true);
            }
            else
            {
                PhotonNetwork.CreateRoom(gameName.text, new Photon.Realtime.RoomOptions { MaxPlayers = 4 });
            }
        }
    }
}
