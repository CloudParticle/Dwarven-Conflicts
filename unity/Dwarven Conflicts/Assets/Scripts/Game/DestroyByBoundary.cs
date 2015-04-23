﻿using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	// Destroys everything that leaves the trigger area.
	void OnTriggerExit2D(Collider2D other) 
	{
		Debug.Log("Out of bounds: "+ other.tag);
		Destroy(other.gameObject);
	}
}
