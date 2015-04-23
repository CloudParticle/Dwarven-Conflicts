using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {
    private bool captured = false;
    public Vector3 startPosition = new Vector3(0f, 0f, 0f);

    void Awake () {
        print("Flag initialized.");        
    }

	// Update is called once per frame
	void Update () {
        if (captured) {
            //TODO: Follow if player is correct. 
            //Else: Reset flag position.
        }
	}

    void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Player") {
            captured = true;
            print(other.tag + " captured the flag!");
        }
    }

    void resetFlag () {
        gameObject.transform.position = startPosition;
    }
}
