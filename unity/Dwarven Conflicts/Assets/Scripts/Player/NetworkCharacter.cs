using UnityEngine;

public class NetworkCharacter : Photon.MonoBehaviour {
    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;

    //
    private Vector3 correctPlayerPos;

    void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(transform.position);
        } else {
            syncEndPosition = (Vector3)stream.ReceiveNext();
            syncStartPosition = transform.position;

            syncTime = 0f;
            syncDelay = Time.time - lastSynchronizationTime;
            lastSynchronizationTime = Time.time;
        }
    }

    void Update () {
        if (!photonView.isMine) {
            SyncedMovement();
        }
    }

    private void SyncedMovement () {
        syncTime += Time.deltaTime;
        this.transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
        //this.transform.position = Vector3.MoveTowards(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
        //this.transform.position = Vector3.MoveTowards(syncStartPosition, syncEndPosition, syncTime / syncDelay);
    }
}
