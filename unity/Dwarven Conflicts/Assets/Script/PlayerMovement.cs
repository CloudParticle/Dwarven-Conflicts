using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private float moveSpeed = 3f;

    // Use this for initialization
	void Start () {
       //
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        movement(h, v);
    }

    void movement (float h, float v) {
        float forwardMovement = h * moveSpeed * Time.deltaTime;        
        transform.Translate(forwardMovement, 0, 0);
    }
}