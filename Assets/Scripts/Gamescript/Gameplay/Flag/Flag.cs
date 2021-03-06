﻿using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {
    public bool captured = false;
    public Vector3 startPosition;
    public int owner;
    private Player currentPlayer;

    private BoxCollider2D flagCollider;
    private Rigidbody2D rb;

    public AudioClip flagCapture;
    public AudioClip flagReset;

    void Awake () {
        flagCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void initFlag (Vector3 startPos, int ownerId) {
        startPosition = startPos;
        owner = ownerId;

        print("Flag initialized.");        
    }

	void Update () {
        if (captured) {
            followPlayer();
        }
	}

    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == "Player") {
            currentPlayer = other.gameObject.GetComponent<Player>();

            if (currentPlayer.playerId == owner) {
                captured = false;                
                resetFlag();
            } else {
                captured = true;
                capturedFlag(other);
            }
        }
    }

    void followPlayer () {
        togglePhysics(true);

        gameObject.transform.position = new Vector3(
            currentPlayer.transform.position.x + 0.8f,
            currentPlayer.transform.position.y + 1f,
            currentPlayer.transform.position.z
        );
    }

    void togglePhysics (bool enable) {
        flagCollider.enabled = enable;
        rb.isKinematic = !enable;
    }

    void capturedFlag (Collision2D other) {
        AudioSource.PlayClipAtPoint(flagCapture, transform.position);
        print(other.gameObject.tag + " captured the flag!");
    }

    public void resetFlag () {
        AudioSource.PlayClipAtPoint(flagReset, transform.position);
        gameObject.transform.position = startPosition;
        print("Player took back the flag!");
    }
}
