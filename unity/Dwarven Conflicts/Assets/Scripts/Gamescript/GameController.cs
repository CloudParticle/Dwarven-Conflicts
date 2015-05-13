using UnityEngine;
using System.Collections;

public class GameController: MonoBehaviour {
    //Script
    private Player player;
    private Flag flag;
    private Castle castle;

    //Start positions for objects. Should be more flexible in the future with different maps/players.
    Vector3[] playerPosition    = { new Vector3(-10f, 0f, 0f), new Vector3(11f, 0f, 0f) };
    Vector3[] flagPosition      = { new Vector3(-11.5f, 2f, 0f), new Vector3(12f, 2f, 0f) };
    Vector3[] castlePosition    = { new Vector3(-13f, 0f, 0f), new Vector3(13f, 0f, 0f) };


    public void initConnectedPlayer(int playerId) {
        addPlayer(playerId);
        addFlag(playerId);
        addCastle(playerId);
    }

    void addPlayer (int playerId) {
        GameObject gameObj = PhotonNetwork.Instantiate(Resources.Load("Player").name, playerPosition[playerId], Quaternion.identity, 0);
        Player player = gameObj.GetComponent<Player>();
        player.initPlayer(playerPosition[playerId], playerId);
    }

    void addFlag(int ownerId) {
        GameObject gameObj = PhotonNetwork.Instantiate(Resources.Load("Flag").name, flagPosition[ownerId], Quaternion.identity, 0);
        Flag flag = gameObj.GetComponent<Flag>();
        flag.initFlag(flagPosition[ownerId], ownerId);
    }

    void addCastle(int ownerId) {
        GameObject gameObj = PhotonNetwork.Instantiate(Resources.Load("Castle").name, castlePosition[ownerId], Quaternion.identity, 0);
        Castle castle = gameObj.GetComponent<Castle>();
        castle.initCastle(castlePosition[ownerId], ownerId);
    }
}
