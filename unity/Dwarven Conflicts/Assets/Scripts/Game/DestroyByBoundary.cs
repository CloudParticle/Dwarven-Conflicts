using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	// Destroys objects that leaves the surrounding game area.
	void OnTriggerExit2D (Collider2D other) {
        if (other.tag == "Player") {
            other.gameObject.GetComponent<Player>().setAlive(false);
        } else {
            Destroy(other.gameObject);
        }

		Debug.Log("Out of bounds: "+ other.tag);
	}
}
