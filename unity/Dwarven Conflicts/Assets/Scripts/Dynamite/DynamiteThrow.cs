using UnityEngine;
using System.Collections;

public class DynamiteThrow : MonoBehaviour {
    private Rigidbody2D rb;
    public float speed = 18.0f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
