using UnityEngine;
using System.Collections;

public class GameController: MonoBehaviour {
    //Script
    private Player player;

    //GameObject
    private GameObject playerObj;

	void Start () {
        addPlayer(new Vector3(-10f, 0f, 0f), 0);
        addPlayer(new Vector3(11f, 0f, 0f), 1);
	}

    void addPlayer (Vector3 position, int playerId) {
        GameObject obj = Resources.Load("Player", typeof(GameObject)) as GameObject;
        playerObj = Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;
        player = playerObj.GetComponent<Player>();
        player.initPlayer(playerId);
    }
	
	void Update () {
	
	}
}
