using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	// Destroy everything that leaves the trigger
	void OnTriggerExit2D(Collider2D other) 
	{
		Debug.Log("On trigger exit: "+other.tag);
		Destroy(other.gameObject);
	}
}
