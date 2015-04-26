using UnityEngine;
using System.Collections;

public class Castle : MonoBehaviour {
    private Vector3 position;
    private int ownerId;

    public void initCastle(Vector3 pos, int owner) {
        position = pos;
        ownerId = owner;
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Flag") {
            if (other.gameObject.GetComponent<Flag>().owner != ownerId) {
                print("Player " + ownerId + " won!");
            }
        }
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == "Flag") {
            if (other.gameObject.GetComponent<Flag>().owner != ownerId) {
                print("The player " + ownerId + " won!");
            }
        }
    }
}
