using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private Animator anim;              // Reference to the animator component.
    private float moveSpeed = 3f;

    // Use this for initialization
	void Start () {
       //
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate () {
        // Cache the inputs.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        movement(h, v);
    }

    void movement (float h, float v) {
        transform.Translate(h * moveSpeed * Time.deltaTime, v * moveSpeed * Time.deltaTime, 0);
    }
}