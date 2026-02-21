using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    
    public void Start()
    {
        //PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server");

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

        PhotonNetwork.JoinRandomRoom();
        //base.OnJoinedLobby();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No Rooms Exists, Creating New Room");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers =4 });
        //base.OnJoinRandomFailed(returnCode, message);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");

        PhotonNetwork.Instantiate("Player", 
            new Vector3(Random.Range(-10, 10), 2, Random.Range(-10, 10)), 
            Quaternion.identity);
        //base.OnJoinedRoom();
    }
}
