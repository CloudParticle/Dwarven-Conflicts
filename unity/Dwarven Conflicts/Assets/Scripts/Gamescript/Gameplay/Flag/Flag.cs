using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {
    public bool captured = false;
    public Vector3 startPosition;
    public int owner;
    private Player currentPlayer;

    private BoxCollider2D collider;
    private Rigidbody2D rb;

    void Awake () {
        collider = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
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
        togglePhysics(true);

        gameObject.transform.position = new Vector3(
            currentPlayer.transform.position.x + 0.8f,
            currentPlayer.transform.position.y + 1f,
            currentPlayer.transform.position.z
        );
    }

    void togglePhysics (bool active) {
        collider.enabled = active;
        rb.isKinematic = !active;
    }

    public void resetFlag () {
        print("Player took back the flag!");
        captured = false;
        gameObject.transform.position = startPosition;
    }
}
