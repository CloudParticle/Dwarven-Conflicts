using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Castle : MonoBehaviour {
    public int ownerId;
    public Text winnerText;

    void Start () {
        //winnerText.text = "";
    }

    public void initCastle(Vector3 pos, int owner) {
        ownerId = owner;
    }

    void OnTriggerEnter2D (Collider2D other) {
        print("Entered me!");
        print(other.gameObject.tag);
        if (other.gameObject.tag == "Flag") {
            if (other.gameObject.GetComponent<Flag>().owner != ownerId) {
                print("Player " + ownerId + " won!");
            }
        }
    }
}
