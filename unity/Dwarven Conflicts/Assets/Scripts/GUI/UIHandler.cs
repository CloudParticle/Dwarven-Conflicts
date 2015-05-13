using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandler : MonoBehaviour {
    public Canvas canvas;

	// Use this for initialization
	void Start () {
	    
	}

    public void setWinningPlayer (int playerId) {
        print("Player " + playerId + "won!");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
