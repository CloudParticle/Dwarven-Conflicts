using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
[RequireComponent(typeof(ScoreControl))]
public class Player : MonoBehaviour {
    //Variables
	private float jumpHeight = 2.5f;
	private float timeToJumpApex = 0.3f;
	float accelerationTimeAirborne = 0.2f;
	float accelerationTimeGrounded = 0.1f;
	float moveSpeed = 6f;

    //Game variables
	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

    //Dynamite
    public GameObject dynamite;
    public Transform dynamiteSpawn;
    public float fireRate = 1f;
    private float nextFire = 0f;

	PlayerController controller;
    ScoreControl logScore;

	void Start() {
		controller = GetComponent<PlayerController> ();
        logScore = GetComponent<ScoreControl>();
        dynamiteSpawn = GetComponent<Transform>();

		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}

	void Update() {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		if (Input.GetKeyDown (KeyCode.Space) && controller.collisions.below) {
			velocity.y = jumpVelocity;
		}

        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(dynamite, dynamiteSpawn.position, dynamiteSpawn.rotation);
            logScore.subtractScore(0);
        }

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}
}
