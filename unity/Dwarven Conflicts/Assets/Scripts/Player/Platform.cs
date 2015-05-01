using UnityEngine;
using System.Collections;

public class Platform : Photon.MonoBehaviour {
    private int owner;
    private int life = 1;

    void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info) {
        Vector3 syncPosition = Vector3.zero;
        if (stream.isWriting) {
            print("writing");
            stream.Serialize(ref syncPosition);
        } else {
            print("else");
            stream.Serialize(ref syncPosition);
        }
    }

    public void initPlatform(Vector3 position, int owner) {

    }

    public int reduceLife () {
        if (life > 0) {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
            life--;
        } else {
            PhotonNetwork.Destroy(gameObject);
        }
        print(gameObject.name + " has " + (life + 1) + " hits left.");
        return life;
    }
}
