using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerGameConnector : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPref;
    [SerializeField] private Canvas canvas;
    [SerializeField] private List<GameObject> borders;
    [SerializeField] private HealthBarUI healthBarUI;
    [SerializeField] private WinPanel winPanel;
    public static PlayerGameConnector Instance;

    private List<PlayerHealth> players = new List<PlayerHealth>();

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
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        onGameStarted += ClearBorders;
    }

    private void CreatePlayer()
    {
        PlayerMover playerMover = PhotonNetwork.Instantiate(playerPref.name, Vector2.zero, Quaternion.identity).GetComponent<PlayerMover>();
        playerMover.SetPlayerMoveJoystick(canvas);
        playerMover.GetComponent<PlayerShoot>().SetPlayerRotateJoystick(canvas);
        playerMover.GetComponent<PlayerHealth>().PlayerName = PhotonNetwork.LocalPlayer.NickName;
        healthBarUI.PlayerHealth = playerMover.GetComponent<PlayerHealth>();
        healthBarUI.PlayerHealth.onHealthChanged += healthBarUI.UpdateBar;
        onPlayerCreated?.Invoke(playerMover.GetComponent<PlayerCoinCollector>());
        playerMover.GetComponent<PlayerHealth>().onDie += LeaveGame;
        AddPlayer(playerMover.GetComponent<PlayerHealth>());
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

            foreach (PlayerHealth player in players)
            {
                player.GetComponent<PlayerShoot>().canShoot = true;
            }
        }
    }

    private void AddPlayer(PlayerHealth playerHealth)
    {
        if (PhotonNetwork.PlayerList.Length >= 2)
        {
            playerHealth.GetComponent<PlayerShoot>().canShoot = true;
        }

        players.Add(playerHealth);
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
        foreach (PlayerHealth player in players)
        {
            if (player.PlayerName == otherPlayer.NickName)
            {
                Destroy(player.gameObject);
            }
        }

        if (PhotonNetwork.PlayerList.Length == 1)
        {
            PlayerHealth player = players.First();
            winPanel.SetWinPanel(player.PlayerName, player.GetComponent<PlayerCoinCollector>().CoinCount);
            //PhotonNetwork.CurrentRoom.IsOpen = false;
        }

        Debug.LogFormat("{0} left room", otherPlayer.NickName);
    }
}
