using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
[RequireComponent(typeof(ScoreControl))]
public class Player : MonoBehaviour {
    //Variables
	private float jumpHeight = 2.6f;
	private float timeToJumpApex = 0.3f;
	private float accelerationTimeAirborne = 0.2f;
	private float accelerationTimeGrounded = 0.1f;
	private float moveSpeed = 5f;
    private float spawnTime = 4f;

    //Game variables
	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

    //Sets on initPlayer
    public int playerId = 0;
    private Vector3 startPosition;
    private bool isAlive = true;

    //Dynamite
    public GameObject dynamite;
    public float fireRate = 1f;
    private float nextFire = 0f;

    //Platform
    private int gridSize = 2;
    private float platformHeight = 0.13f;

    public GameObject platform;
    public GameObject platformContainer;
    private GameObject platformWrapper;

	private PlayerController controller;
    private ScoreControl score;

    void Awake () {
        controller = GetComponent<PlayerController>();
        score = GetComponent<ScoreControl>();
        platform = Resources.Load("Platform") as GameObject;
    }

    //Use as constructor who recieve playerId.
    public void initPlayer(Vector3 startPos, int id) {
        startPosition = startPos;    
        playerId = id;
		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        platformWrapper = (GameObject)Instantiate(platformContainer, Vector3.zero, transform.rotation);
        updateContainerPos();
    }

    void updateContainerPos() {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print("y: " + Mathf.Round(transform.position.y));
        print("localY: " + Vector3.Distance(transform.position, mouse));

        platformWrapper.transform.position = new Vector3(
            Mathf.Round(transform.position.x / gridSize) * gridSize + gridSize,
            Mathf.Round(Mathf.Clamp(mouse.y, (transform.position.y - gridSize), (transform.position.y +  gridSize)) / gridSize) * gridSize, 
            0f
        );
    }

    public int getPlayerId () {
        return playerId;
    }

    public void setAlive (bool status) {
        isAlive = status;
    }

	void Update() {
        if (isAlive) {
            inputListeners();
            updateContainerPos();
        } else {
            resetPlayer();
        }
    }

    void resetPlayer () {
        spawnTime -= Time.deltaTime;

        if (!isAlive && spawnTime < 0) {
            spawnTime = 4f; //Reset spawn time.
            print(gameObject.tag + " respawned.");
            transform.position = startPosition;
            setAlive(true);
        } else {
            //State: spawning
            transform.position = startPosition;
        }
    }

    void inputListeners () {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (controller.collisions.above || controller.collisions.below) {
            velocity.y = 0;
        }

        if (Input.GetButton("Jump") && controller.collisions.below) {
            velocity.y = jumpVelocity;
        }

        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(dynamite, gameObject.transform.position, gameObject.transform.rotation);
        }

        if (Input.GetButton("Fire2") && Time.time > nextFire) {
            //TODO Instantiate new platform that can only make platform inside its own container.
            nextFire = Time.time + fireRate;

            //if (score.subtractScore(playerId) > 0) {
            
                Instantiate(platform, new Vector3(
                    platformWrapper.transform.position.x,
                    platformWrapper.transform.position.y - (1 + platformHeight),
                    platformWrapper.transform.position.z
                ), transform.rotation);
            //}
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}