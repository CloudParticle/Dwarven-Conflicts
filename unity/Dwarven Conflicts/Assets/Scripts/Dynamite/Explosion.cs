using UnityEngine;
using System.Collections;

public class Explosion : Photon.MonoBehaviour {
    private bool hitPlayer = false;
    private float timeLeft = 0.5f;
    private GameObject hitPlayerObj;

	void FixedUpdate () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) {
            PhotonNetwork.Destroy(gameObject);
        }

        if (hitPlayer) {
            hitPlayer = false;
            moveHitPlayer();
        }
	}

    void moveHitPlayer () {
        float velocityX = Random.Range(-3f, 3f),
              velocityY = Random.Range(3f, 6f);

        hitPlayerObj.GetComponent<Rigidbody2D>().AddForce(
            new Vector2(100f * velocityX, 200f * velocityY)
        );
        print("Player been hit by an explosion! FUUUCK!");
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
