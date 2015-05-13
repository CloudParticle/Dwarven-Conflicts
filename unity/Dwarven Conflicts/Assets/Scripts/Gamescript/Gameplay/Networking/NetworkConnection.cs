using UnityEngine;
using System.Collections;

public class NetworkConnection : Photon.MonoBehaviour {
    private GameController gameController;

    void Awake () {
        gameController = gameObject.GetComponent<GameController>();
    }
    void Start () {
        PhotonNetwork.ConnectUsingSettings("0.2");
        PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
    }

    void OnPhotonPlayerConnected (PhotonPlayer player) {
        Debug.Log("OnPhotonPlayerConnected: " + player);
    }

    void OnJoinedLobby () {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed () {
        Debug.Log("Can't join random room!");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom () {
        gameController.initConnectedPlayer(PhotonNetwork.player.ID-1);
    }

    void OnGUI () {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}