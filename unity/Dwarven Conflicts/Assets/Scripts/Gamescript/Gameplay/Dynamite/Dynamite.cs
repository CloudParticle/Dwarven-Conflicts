using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class Dynamite : Photon.MonoBehaviour {
    private Rigidbody2D rb;
    public GameObject explosion;

    private bool isExploding = false;
    private float speed = 300.0f;
    private float timeLeft = 5.0f;

    void Start () {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(transform.right * speed);
    }
	
	// Update is called once per frame
    void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0 && !isExploding) {
            explode();
            print("EXPLOSION!");
        }
    }

    void explode () {
        isExploding = true;
        if (PhotonNetwork.isMasterClient) {
            PhotonNetwork.Instantiate(explosion.name, gameObject.transform.position, gameObject.transform.rotation, 0);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
