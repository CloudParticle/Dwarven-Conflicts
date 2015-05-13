using UnityEngine;
using System.Collections;

public class NetworkConnection : Photon.MonoBehaviour {
    // Use this for initialization
    void Start () {
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
    }

    void OnPhotonPlayerConnected (PhotonPlayer player) {
        Debug.Log("OnPhotonPlayerConnected: " + player);

        // when new players join, we send "who's it" to let them know
        // only one player will do this: the "master"
    }

    void OnJoinedLobby () {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed () {
        Debug.Log("Can't join random room!");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom () {
        GameObject play = Resources.Load("Player") as GameObject;
        GameObject dwarf = PhotonNetwork.Instantiate(play.name, new Vector3(-10f, 0f, 0f), Quaternion.identity, 0);
        Player player = dwarf.GetComponent<Player>();
        if (PhotonNetwork.player.ID == 1) {
            player.initPlayer(new Vector3(-10f, 0f, 0f), PhotonNetwork.player.ID);
        } else {
            player.initPlayer(new Vector3(11f, 0f, 0f), PhotonNetwork.player.ID);
        }
    }

    void OnGUI () {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}