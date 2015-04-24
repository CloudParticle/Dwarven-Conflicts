using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {
    private bool captured = false;
    public Vector3 startPosition;
    private int owner;
    private Player currentPlayer;

    private Rigidbody2D storedRb;

    void Awake () {
        storedRb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void initFlag (Vector3 startPos, int ownerId) {
        startPosition = startPos;
        owner = ownerId;

        print("Flag initialized.");        
    }

	void Update () {
        if (captured) {
            followPlayer();
        }
	}

    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == "Player") {
            currentPlayer = other.gameObject.GetComponent<Player>();

            if (currentPlayer.getPlayerId() == owner) {
                captured = false;                
                resetFlag();
            } else {
                print(other.gameObject.tag + " captured the flag!");         
                captured = true;
            }
        }
    }

    void followPlayer () {
        gameObject.transform.position = new Vector3(
            currentPlayer.transform.position.x + 0.55f,
            currentPlayer.transform.position.y + 1.5f,
            currentPlayer.transform.position.z
        );
    }

    void resetFlag () {
        print("Player took back the flag!");         
        gameObject.transform.position = startPosition;
    }
}
