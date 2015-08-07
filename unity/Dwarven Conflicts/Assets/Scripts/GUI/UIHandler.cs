using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHandler : MonoBehaviour {
    private GameObject gui;
    public Text[] logCount;


	// Use this for initialization
	void Start () {
        gui = Instantiate(Resources.Load("GUI") as GameObject);
    }

    public void setLogCountPlayer (int playerId, int logValue) {
        logCount[playerId].text = logValue.ToString();
    }

    public void setWinningPlayer (int playerId) {
        print("Player " + playerId + "won!");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
