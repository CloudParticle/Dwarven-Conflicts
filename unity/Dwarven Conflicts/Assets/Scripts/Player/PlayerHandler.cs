using UnityEngine;
using System.Collections;

public class PlayerHandler : Photon.MonoBehaviour {
    public Player player;
    public PlayerController controller;

	void Awake () {
        player = GetComponent<Player>();
        controller = GetComponent<PlayerController>();
	}

    void Start () {
        if (photonView.isMine) {
            player.enabled = true;
            controller.enabled = true;
        }
    }
}
