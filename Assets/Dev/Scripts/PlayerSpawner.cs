using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    private void Start()
    {
        if (!PhotonNetwork.IsConnected) return;

        Vector3 spawnPos = new Vector3(Random.Range(40, 50), 310, Random.Range(400, 420));

        PhotonNetwork.Instantiate("AkaiEEspiritu",spawnPos,Quaternion.identity);
    }
}
