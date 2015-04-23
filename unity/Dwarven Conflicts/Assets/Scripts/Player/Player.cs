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

    //Platform
    private int gridSize = 2;
    public GameObject platform;
    public GameObject platformContainer;

    private GameObject tester;

	PlayerController controller;
    ScoreControl logScore;

	void Start() {
		controller = GetComponent<PlayerController> ();
        logScore = GetComponent<ScoreControl>();
        dynamiteSpawn = GetComponent<Transform>();

		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        tester = (GameObject)Instantiate(platformContainer);
        updateContainerPos();
    }

    void updateContainerPos() {
        tester.transform.position = new Vector3(
            Mathf.Round(transform.position.x / gridSize) * gridSize,
            Mathf.Round(transform.position.y / gridSize) * gridSize,
            Mathf.Round(transform.position.z / gridSize) * gridSize
        );
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
        }

        if (Input.GetButton("Fire2") && Time.time > nextFire) {
            //TODO Instantiate new platform that can only make platform inside its own container.
            nextFire = Time.time + fireRate;
            
            if (logScore.subtractScore(0) > 0) {
                Vector3 pos = new Vector3(
                    Mathf.Round(dynamiteSpawn.position.x / gridSize) * gridSize,
                    Mathf.Round(dynamiteSpawn.position.y / gridSize) * gridSize,
                    Mathf.Round(dynamiteSpawn.position.z / gridSize) * gridSize
                );

                Instantiate(platform, pos, dynamiteSpawn.rotation);
            }   
        }

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);

        updateContainerPos();
	}
}