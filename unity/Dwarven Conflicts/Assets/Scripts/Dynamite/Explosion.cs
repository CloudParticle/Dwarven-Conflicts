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

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "Platform") {
            Platform platform = other.gameObject.GetComponent<Platform>();
            platform.reduceLife();
            print("Exploded on: " + other.gameObject.name);
        } if (other.gameObject.tag == "Player") {
            print(other.gameObject.tag + " been hit by an explosion! FUUUCK!");
        }
    }
}
