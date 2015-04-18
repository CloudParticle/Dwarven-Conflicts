using UnityEngine;
using System.Collections;

public class DynamiteThrow : MonoBehaviour {
    private Rigidbody2D rb;
    private float speed = 300.0f;
    private float timeLeft = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * speed);
    }
	
	// Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) {
            Destroy(gameObject);
            print("EXPLOSION!");
        }
    }
}
