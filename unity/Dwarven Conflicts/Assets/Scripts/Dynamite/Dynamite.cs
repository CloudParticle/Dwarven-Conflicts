using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class Dynamite : MonoBehaviour {
    private Rigidbody2D rb;
    public GameObject explosion;

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
        SphereCollider collider      = explosion.GetComponent<SphereCollider>();
        GameObject explodingCollider = (GameObject) GameObject.Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
         
        Destroy(gameObject);
    }
}
