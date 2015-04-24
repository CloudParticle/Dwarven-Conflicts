using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    private bool hitPlayer = false;
    private float timeLeft = 0.5f;
    private GameObject hitPlayerObj;

	void FixedUpdate () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) {
            Destroy(gameObject);
        }

        if (hitPlayer) {
            hitPlayer = false;
            moveHitPlayer();
        }
	}

    void moveHitPlayer () {
        float velocityX = Random.Range(-12f, 12f),
              velocityY = Random.Range(3f, 5f);

        hitPlayerObj.GetComponent<Rigidbody2D>().AddForce(
            new Vector2(velocityX, 200f * velocityY)
        );
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "Platform") {
            Platform platform = other.gameObject.GetComponent<Platform>();
            platform.reduceLife();
            print("Exploded on: " + other.gameObject.name);
        } if (other.gameObject.tag == "Player") {
            hitPlayerObj = other.gameObject;
            hitPlayer = true;
            print(other.gameObject.tag + " been hit by an explosion! FUUUCK!");
        }
    }
}
