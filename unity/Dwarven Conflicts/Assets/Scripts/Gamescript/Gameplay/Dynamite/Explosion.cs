using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
    private bool hitPlayer = false;

    private float timeLeft = 0.5f;
    private GameObject hitPlayerObj;

	void FixedUpdate () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) {
            //Destory explosion (self) after set time.
            Destroy(gameObject);
        }

        if (hitPlayer) {
            //Player hit by explosion.
            hitPlayer = false;
            moveHitPlayer();
        }
	}

    /**
     * Adds a force on player hit by the explosion.
     */
    void moveHitPlayer () {
        float velocityX = Random.Range(-4f, 3f),
              velocityY = Random.Range(3f, 6f);

        hitPlayerObj.GetComponent<Rigidbody2D>().AddForce(
            new Vector2(100f * velocityX, 200f * velocityY)
        );
        print("Player been hit by an explosion!");
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "Platform") {
            Platform platform = other.gameObject.GetComponent<Platform>();
            platform.reduceLife();
            print("Exploded on: " + other.gameObject.name);
        } if (other.gameObject.tag == "Player" || other.gameObject.tag == "Dynamite") {
            hitPlayerObj = other.gameObject;
            hitPlayer = true;
        }
    }
}
