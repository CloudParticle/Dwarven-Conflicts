using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
    public int ownerId;
    private int life = 1;

    private int syncLife;

    public void initPlatform (Vector3 position, int owner) {
        ownerId = owner;
    }

    [RPC]
    public void reduceLife () {
        Debug.Log("Reduce life from " + life + " to " + (life - 1));
        if (life > 0) {
            life -= 1;
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        } else {
            Die();
        }
    }

    void Die () {
        if (PhotonNetwork.isMasterClient) {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}