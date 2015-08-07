using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour {
    public Player player;
    public PlayerController controller;

	void Awake () {
        player = GetComponent<Player>();
        controller = GetComponent<PlayerController>();
	}

    void Start () {
        if (player.playerId == 0) {
            player.enabled = true;
            controller.enabled = true;
        }       
    }
}
