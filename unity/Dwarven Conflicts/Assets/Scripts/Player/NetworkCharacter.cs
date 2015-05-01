using UnityEngine;

public class NetworkCharacter : Photon.MonoBehaviour {
    private Vector3 correctPlayerPos;

    // Update is called once per frame
    void FixedUpdate () {
        if (!photonView.isMine) {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
        }
    }

    void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
        } else {
            // Network player, receive data
            this.correctPlayerPos = (Vector3)stream.ReceiveNext();
        }
    }
}
