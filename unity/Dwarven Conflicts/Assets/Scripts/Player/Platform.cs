using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
    private int life = 1;

    public int reduceLife () {
        if (life > 0) {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
            life--;
        } else {
            Destroy(gameObject);
        }
        print(gameObject.name + " has " + (life + 1) + " hits left.");
        return life;
    }
}
