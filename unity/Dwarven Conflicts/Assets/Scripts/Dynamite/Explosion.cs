using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    private float timeLeft = 3.0f;
	
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) {
            Destroy(gameObject);
        }
	}

    void OnTriggerStay2D (Collider2D other) {
        if (other.gameObject.tag == "Platform") {
            print("Exploding on: " + other.gameObject.name);
            Destroy(other.gameObject);
        } if (other.gameObject.tag == "Player") {
            print(other.gameObject.name + " been hit by an explosion! FUUUCK!");
        }
    }
}
