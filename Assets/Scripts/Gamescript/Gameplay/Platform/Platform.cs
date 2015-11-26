using UnityEngine;

public class Platform : MonoBehaviour {
    public int ownerId = 0;
    private int life = 1;

    public void initPlatform (int owner) {
        ownerId = owner;
    }

    void OnTriggerEnter2D (Collider2D other) {
        //
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == "Player") {
            Player player = other.gameObject.GetComponent<Player>();
            if (player.playerId != ownerId) {
                print("//IGNORE!");
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>());
            }
        }
    }

    public void reduceLife () {
        Debug.Log("Reduce life from " + life + " to " + (life - 1));
        if (life > 0) {
            life -= 1;
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        } else {
            Die();
        }
    }

    void Die () {
        Destroy(gameObject);
    }
}