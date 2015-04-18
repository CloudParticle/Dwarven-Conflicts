using UnityEngine;
using System.Collections;

public class DynamiteThrow : MonoBehaviour {
    private Rigidbody2D rb;
    private float speed = 300.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.right * speed);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
