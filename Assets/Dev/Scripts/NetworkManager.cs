using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    public TMP_InputField roomInput;

    [Header("Text")]
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI statusText;

    [Header("Buttons")]
    public Button createButton;
    public Button joinButton;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Start()
    {
        /*if (PhotonNetwork.IsConnected || PhotonNetwork.NetworkClientState != Photon.Realtime.ClientState.Disconnected)
        {
            Debug.Log("Already connected or connecting. State = "
         + PhotonNetwork.NetworkClientState);
            return;
        }*/

            if (warningText !=null)
        {
            warningText.text = "";
        }

        if (statusText != null)
            statusText.text = "Connecting to server...";

        if (createButton != null && joinButton != null)
        {
            createButton.interactable = false;
            joinButton.interactable = false;

        }


        PhotonNetwork.NickName = "Player" + Random.Range(1, 100);
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server");

        if (statusText != null)
            statusText.text = "Connected! Joined Lobby";
        PhotonNetwork.JoinLobby();
        //base.OnConnectedToMaster();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Disconnected: " + cause);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        if(statusText != null)
        {
            statusText.text = "Ready...";
        }

        if (createButton != null && joinButton != null)
        {
            createButton.interactable = true;
            joinButton.interactable = true;

        }
        //PhotonNetwork.JoinRandomRoom();
        //Debug.Log("Player joined. ActorNumber = " + PhotonNetwork.LocalPlayer.ActorNumber);
        //base.OnJoinedLobby();
    }

    /*public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No Rooms Exists, Creating New Room");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers =4 });
        //base.OnJoinRandomFailed(returnCode, message);
    }*/

    public void OnCreateRoomClick()
    {
        if (roomInput == null) return;
        if (string.IsNullOrEmpty(roomInput.text))
        {

            warningText.text = "Enter Room Code!";
            return;
        }

        if(statusText != null)
        {
            statusText.text = "Creating the room...";
        }
        PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions { MaxPlayers = 4 });
    }

    public void OnJoinRoomClick()
    {
        if (roomInput == null) return;
        if (string.IsNullOrEmpty(roomInput.text))
        {
            if (warningText != null)
            {
                warningText.text = "Enter Room Code!";
            }

            
            return;
        }

        if(statusText!= null)
        {
            statusText.text = "Joining the Room...";
        }
        PhotonNetwork.JoinRoom(roomInput.text);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        if(statusText=null)
        {
            statusText.text = "Joined the Room!";
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Multiplayer");
        }
        /*PhotonNetwork.Instantiate("AkaiEEspiritu", 
            new Vector3(Random.Range(40, 50), 310, Random.Range(400, 420)), 
            Quaternion.identity);*/
        //base.OnJoinedRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        statusText.text = " Join Failed!";
        //base.OnJoinRoomFailed(returnCode, message);
    }
    public void OnLeaveRoomClick()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        statusText.text = "Left Room";
        //base.OnLeftRoom();

        PhotonNetwork.LoadLevel("Lobby");

    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        base.OnDisable();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Lobby")
            statusText= GameObject.Find("Status - Text").GetComponent<TextMeshProUGUI>();
        else if (scene.name == "Multiplayer")
            statusText = GameObject.Find("Status - Text").GetComponent<TextMeshProUGUI>();
    }
}
