using UnityEngine;

public class NetworkCharacter : Photon.MonoBehaviour {
    private Vector3 correctPlayerPos;
    
    void FixedUpdate () {
        if (photonView.isMine) {
            //
        } else {
            //transform.position = Vector3.Lerp(transform.position, correctPlayerPos, 0.1f);
        }
    }

    void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
        } else {
            // Network player, receive data
            correctPlayerPos = (Vector3)stream.ReceiveNext();
        }
    }
}
