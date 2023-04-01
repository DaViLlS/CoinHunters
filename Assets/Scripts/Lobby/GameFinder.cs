using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameFinder : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField gameName;
    [SerializeField] private GameObject warningObject;

    public void FindGame()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRoom(gameName.text);
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Не удалось войти в игру");
    }
}
