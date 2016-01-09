using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
public class Player : MonoBehaviour {
    //Variables
	private float jumpHeight = 2.8f;
	private float timeToJumpApex = 0.3f;
	private float accelerationTimeAirborne = 0.2f;
	private float accelerationTimeGrounded = 0.1f;
	private float moveSpeed = 5f;
    private float spawnTime = 4f;

    //Game variables
	float gravity;
	float jumpVelocity;
	public Vector3 velocity;
	float velocityXSmoothing;

    GameObject gameController;

    //Set on initPlayer
    public int playerId = 0;
    private Vector3 startPosition;
    private bool isAlive = true;
    private int logCount = 15;

    //Dynamite
    public Transform spawnPoint;
    public GameObject dynamite;
    public float fireRate = 1f;
    private float nextFire = 0f;

    //Platform
    private int gridSize = 2;
    private float platformHeight = 0.13f;

    private GameObject platform;
    private GameObject platformContainer;
    private GameObject platformWrapper;


	private PlayerController controller;
    private ScoreControl score;

    void Awake () {
        controller = GetComponent<PlayerController>();
        score = GetComponent<ScoreControl>();
        platform = Resources.Load("Platform") as GameObject;
        platformContainer = Resources.Load("PlatformArea") as GameObject;
        gameController = GameObject.FindGameObjectWithTag("Game");
    }

    void Start () {
        controller.enabled = true;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    public void initPlayer(Vector3 startPos, int id) {
        startPosition = startPos;    
        playerId = id;

        platformWrapper = Instantiate(platformContainer, Vector3.zero, transform.rotation) as GameObject;
        gameObject.layer = 20 + playerId;   //Player layer. Platforms spawn on this layer.
        resetPlayer();
        updateContainerPos();
    }

    int getOtherPlayerLayer () {
        int ignoreLayer = 21;
        if (playerId == 1) ignoreLayer = 20;
        return ignoreLayer;
    }

    void updateContainerPos() {
        Vector3 mouse = GetWorldPositionOnPlane(Input.mousePosition, 0f);

        platformWrapper.transform.position = new Vector3(
            Mathf.Round(Mathf.Clamp(mouse.x, (transform.position.x - gridSize), (transform.position.x + gridSize)) / gridSize) * gridSize,
            Mathf.Round(Mathf.Clamp(mouse.y, (transform.position.y - gridSize), (transform.position.y +  gridSize)) / gridSize) * gridSize, 
            0f
        );
    }

    public Vector3 GetWorldPositionOnPlane (Vector3 screenPosition, float z) {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    public void setAlive (bool status) {
        isAlive = status;
    }

    void Update () {
        if (isAlive) {
            inputListeners();
        } else {
            resetPlayer();
        }

        updateContainerPos();
    }
    
    void resetPlayer () {
        spawnTime -= Time.deltaTime;

        if (!isAlive && spawnTime < 0) {
            spawnTime = 4f; //Reset spawn time.
            print(gameObject.tag + " " + playerId + " respawned.");
            transform.position = startPosition;
            setAlive(true);
        } else {
            transform.position = startPosition;
        }
    }

    void inputListeners () {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (controller.collisions.above || controller.collisions.below) {
            velocity.y = 0;
        }

        if (Input.GetButtonDown("Jump") && controller.collisions.below) {
            velocity.y = jumpVelocity;
        }

        if (Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Instantiate(dynamite, spawnPoint.transform.position, gameObject.transform.rotation);
        }

        if (Input.GetButton("Fire2") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            instantiatePlatform();
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(
            velocity.x, 
            targetVelocityX, 
            ref velocityXSmoothing, 
            (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne
        );
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void instantiatePlatform () {
        if (reduceLogCount()) {
            GameObject tempPlatform = Instantiate(platform, new Vector3(
                platformWrapper.transform.position.x,
                platformWrapper.transform.position.y - (1 + platformHeight),
                platformWrapper.transform.position.z
            ), transform.rotation) as GameObject;

            tempPlatform.layer = 13;
            tempPlatform.GetComponent<Platform>().initPlatform(playerId);
        } else {
            print("Out of logs.");
        }
    }

    bool reduceLogCount () {
        if (logCount > 0) {
            logCount--;
        }

        gameObject.GetComponent<ScoreControl>().setScore(logCount, playerId);
        return logCount > 0 ? true : false;
    }
}