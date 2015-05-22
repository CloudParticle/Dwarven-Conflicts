using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Flag))]
[RequireComponent(typeof(Player))]
public class DestroyByBoundary : Photon.MonoBehaviour {

	// Destroys objects that leaves the surrounding game area.
	void OnTriggerExit2D (Collider2D other) {
        switch (other.tag) {       
            case "Player":
                resetPlayer(other.gameObject);
                break;            
            case "Flag":
                other.gameObject.GetComponent<Flag>().resetFlag();
                break;
            default:
                if (PhotonNetwork.isMasterClient) {
                    PhotonNetwork.Destroy(other.gameObject);
                }
                break;
        }
		Debug.Log("Out of bounds: "+ other.tag);
	}

    void resetPlayer (GameObject other) {
        Player player = other.GetComponent<Player>();
        GameObject[] flags = GameObject.FindGameObjectsWithTag("Flag");

        foreach (GameObject flag in flags) {
            Flag playerFlag = flag.GetComponent<Flag>();
            if (playerFlag.captured && playerFlag.owner != player.playerId) {
                playerFlag.resetFlag();
            }
        }
        player.setAlive(false);
    }
}
