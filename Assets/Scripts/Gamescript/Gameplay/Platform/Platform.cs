using UnityEngine;

public class Platform : MonoBehaviour {
    public int ownerId = 0;
    private int life = 1;

    public void initPlatform (int owner) {
        ownerId = owner;
    }

    void togglePhysics(bool enable) {
        gameObject.GetComponent<BoxCollider2D>().enabled = enable;

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = !enable;
        rb.freezeRotation = !enable;
    }

    public void reduceLife () {
        Debug.Log("Reduce life from " + life + " to " + (life - 1));
        if (life > 0) {
            life -= 1;
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        } else {
            togglePhysics(true);
            //Die();
        }
    }

    void Die () {
        Destroy(gameObject);
    }
}