using UnityEngine;
using System.Collections;

public class DynamiteThrow : MonoBehaviour {
    private Rigidbody2D rb;
    public GameObject explosion;
    public Renderer explosionRender;

    private bool isExploding = false;
    private float speed = 300.0f;
    private float timeLeft = 5.0f;

    void Start () {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(transform.right * speed);
    }
	
	// Update is called once per frame
    void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0 && !isExploding) {
            isExploding = true;
            explode();
            print("EXPLOSION!");
        }
    }

    void explode () {
        BoxCollider2D collider      = explosion.GetComponent<BoxCollider2D>();
        GameObject explodingCollider = (GameObject) GameObject.Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
         
        Destroy(gameObject);
    }
}
