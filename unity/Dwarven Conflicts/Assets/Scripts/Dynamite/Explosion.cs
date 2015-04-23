using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    private bool hasExploded = false;
    private float timeLeft = 0.5f;
	
	void FixedUpdate () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) {
            Destroy(gameObject);
        }
	}

    void OnTriggerStay2D (Collider2D other) {
        if (other.gameObject.tag == "Platform") {
            print("Exploding on: " + other.gameObject.name);
            Destroy(other.gameObject);
        } if (other.gameObject.tag == "Player" && !hasExploded) {
            print(other.gameObject.name + " been hit by an explosion! FUUUCK!");
            hasExploded = true;
        }
    }
}
