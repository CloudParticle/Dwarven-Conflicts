using UnityEngine;
using System.Collections;

public class GameController: MonoBehaviour {
    //Script
    private Player player;
    private Flag flag;
    private Castle castle;


    //GameObject
    private GameObject playerObj;
    private GameObject flagObj;
    private GameObject castleObj;


	void Start () {
        //Player
        //addPlayer(new Vector3(-10f, 0f, 0f), 0);
        //addPlayer(new Vector3(12f, 0f, 0f), 1);

        //Flag
        addFlag(new Vector3(-11f, -2f, 0f), 0);
        addFlag(new Vector3(13f, -2f, 0f), 1);

        //Castle
        addCastle(new Vector3(-12f, 0f, 0f), 0);
    }

    void addPlayer (Vector3 startPosition, int playerId) {
        GameObject obj = Resources.Load("Player", typeof(GameObject)) as GameObject;
        playerObj = Instantiate(obj, startPosition, Quaternion.identity) as GameObject;
        player = playerObj.GetComponent<Player>();
        player.initPlayer(startPosition, playerId);
    }

    void addFlag(Vector3 startPosition, int ownerId) {
        GameObject obj = Resources.Load("Flag", typeof(GameObject)) as GameObject;
        flagObj = Instantiate(obj, startPosition, Quaternion.identity) as GameObject;
        flag = flagObj.GetComponent<Flag>();
        flag.initFlag(startPosition, ownerId);
    }

    void addCastle(Vector3 startPosition, int ownerId) {
        GameObject obj = Resources.Load("Castle", typeof(GameObject)) as GameObject;
        castleObj = Instantiate(obj, startPosition, Quaternion.identity) as GameObject;
        castle = castleObj.GetComponent<Castle>();
        castle.initCastle(startPosition, ownerId);
    }
}
