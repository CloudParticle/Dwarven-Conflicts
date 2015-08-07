using UnityEngine;
using System.Collections;

public class GameController: MonoBehaviour {
    //Script
    private GameObject gui;
    private Player player;
    private Flag flag;
    private Castle castle;

    //Start positions for objects. Should be more flexible in the future with different maps/players.
    Vector3[] playerPosition    = { new Vector3(-10f, 0f, 0f), new Vector3(11f, 0f, 0f) };
    Vector3[] flagPosition      = { new Vector3(-11.5f, 2f, 0f), new Vector3(12f, 2f, 0f) };
    Vector3[] castlePosition    = { new Vector3(-12f, 0f, 0f), new Vector3(12f, 0f, 0f) };
    Vector3[] basePosition      = { new Vector3(-10.5f, -4f, 0f), new Vector3(10.5f, -4f, 0f) };

    void Start () {
        gui = Instantiate(Resources.Load("GUI")) as GameObject;
        initConnectedPlayer(0);
        initConnectedPlayer(1);
    }

    void initConnectedPlayer(int playerId) {
        addPlayer(playerId);
        addFlag(playerId);
        addCastle(playerId);
        addBase(playerId);
    }

    void addPlayer (int playerId) {
        GameObject gameObj = Instantiate(Resources.Load("Player"), playerPosition[playerId], Quaternion.identity) as GameObject;
        Player player = gameObj.GetComponent<Player>();
        player.initPlayer(playerPosition[playerId], playerId);
    }

    void addFlag(int ownerId) {
        GameObject gameObj = Instantiate(Resources.Load("Flag"), flagPosition[ownerId], Quaternion.identity) as GameObject;
        Flag flag = gameObj.GetComponent<Flag>();
        flag.initFlag(flagPosition[ownerId], ownerId);
    }

    void addCastle(int ownerId) {
        GameObject gameObj = Instantiate(Resources.Load("Castle"), castlePosition[ownerId], Quaternion.identity) as GameObject;
        Castle castle = gameObj.GetComponent<Castle>();
        castle.initCastle(castlePosition[ownerId], ownerId);
    }

    void addBase(int ownerId) {
        GameObject gameObj = Instantiate(Resources.Load("StartBase"), basePosition[ownerId], Quaternion.identity) as GameObject;
    }
}
