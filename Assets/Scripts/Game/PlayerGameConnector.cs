using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGameConnector : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPref;
    [SerializeField] private Canvas canvas;
    [SerializeField] private List<GameObject> borders;
    public static PlayerGameConnector Instance;

    public System.Action onGameStarted;
    public System.Action<PlayerCoinCollector> onPlayerCreated;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        CreatePlayer();

        onGameStarted += ClearBorders;
    }

    private void CreatePlayer()
    {
        PlayerMover playerMover = PhotonNetwork.Instantiate(playerPref.name, Vector2.zero, Quaternion.identity).GetComponent<PlayerMover>();
        playerMover.SetPlayerMoveJoystick(canvas);
        playerMover.GetComponent<PlayerRotator>().SetPlayerRotateJoystick(canvas);
        onPlayerCreated?.Invoke(playerMover.GetComponent<PlayerCoinCollector>());
    }

    public void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(PhotonNetwork.PlayerList.Length);
        if (PhotonNetwork.PlayerList.Length >= 2)
        {
            onGameStarted?.Invoke();
        }
    }

    private void ClearBorders()
    {
        foreach (GameObject border in borders)
        {
            PhotonNetwork.Destroy(border);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("{0} left room", otherPlayer.NickName);
    }
}
