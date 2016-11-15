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
        }

        if (hitPlayer) {
            //Player hit by explosion.
            hitPlayer = false;
        }
	}

    /**
     * Adds a force on player hit by the explosion.
     */
    void moveHitPlayer (GameObject objectToPushAway) {
        float velocityX = Random.Range(-4f, 3f),
              velocityY = Random.Range(3f, 6f);

        objectToPushAway.GetComponent<Rigidbody2D>().AddForce(
            new Vector2(100f * velocityX, 200f * velocityY)
        );
        print("Player been hit by an explosion!");
    }

    void removeSelf () {
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Platform") {
            Platform platform = other.gameObject.GetComponent<Platform>();
            platform.reduceLife();
            print("Exploded on: " + other.gameObject.name);
            removeSelf();
        }
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Dynamite") {
            hitPlayerObj = other.gameObject;
            hitPlayer = true;
            moveHitPlayer(hitPlayerObj);
        }
    }
}
