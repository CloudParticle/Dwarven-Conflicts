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

    public int playerNr = 0;

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

    //Flag
    private GameObject flag;

    void Awake () {
        controller = GetComponent<PlayerController>();
        logScore = GetComponent<ScoreControl>();
        dynamiteSpawn = GetComponent<Transform>();
        flag = Resources.Load("Flag", typeof(GameObject)) as GameObject;
    }

	void Start() {
		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        tester = (GameObject)Instantiate(platformContainer);
        Instantiate(flag, new Vector3(0f, 0f, 0f), Quaternion.identity);

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

		if (Input.GetButton("Jump") && controller.collisions.below) {
			velocity.y = jumpVelocity;
		}

        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(dynamite, dynamiteSpawn.position, dynamiteSpawn.rotation);
        }

        if (Input.GetButton("Fire2") && Time.time > nextFire) {
            //TODO Instantiate new platform that can only make platform inside its own container.
            nextFire = Time.time + fireRate;

            if (logScore.subtractScore(playerNr) > 0) {
                Vector3 pos = new Vector3(
                    Mathf.Round(transform.position.x / gridSize) * gridSize,
                    Mathf.Round(transform.position.y - 1),
                    Mathf.Round(transform.position.z / gridSize) * gridSize
                );

                Instantiate(platform, pos, transform.rotation);
            }   
        }

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);

        updateContainerPos();
	}
}