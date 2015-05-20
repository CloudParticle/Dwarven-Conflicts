using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
public class Player : Photon.MonoBehaviour {
    //Variables
	private float jumpHeight = 2.6f;
	private float timeToJumpApex = 0.3f;
	private float accelerationTimeAirborne = 0.2f;
	private float accelerationTimeGrounded = 0.1f;
	private float moveSpeed = 5f;

    //Photon
    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;

    //Game variables
	float gravity;
	float jumpVelocity;
	public Vector3 velocity;
	float velocityXSmoothing;

    //Set on initPlayer
    public int playerId = 0;
    private Vector3 startPosition;
    private bool isAlive = true;

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
    }

    void Start () {
        if (photonView.isMine) {
            controller.enabled = true;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        }
    }

    public void initPlayer(Vector3 startPos, int id) {
        startPosition = startPos;    
        playerId = id;

        platformWrapper = Instantiate(platformContainer, Vector3.zero, transform.rotation) as GameObject;
        
        StartCoroutine(resetPlayer());
        updateContainerPos();
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

    public int getPlayerId () {
        return playerId;
    }

    public void setAlive (bool status) {
        isAlive = status;
    }

    void Update () {
        if (photonView.isMine) {
            if (isAlive) {
                inputListeners();
            } else {
                StartCoroutine(resetPlayer());
            }

            updateContainerPos();            
        }
    }
    
    IEnumerator resetPlayer () {
        yield return new WaitForSeconds(3);

        if (!isAlive) {
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
            PhotonNetwork.Instantiate(dynamite.name, spawnPoint.transform.position, gameObject.transform.rotation, 0);
        }

        if (Input.GetButton("Fire2") && Time.time > nextFire) {
            //TODO Instantiate new platform that can only make platform inside its own container.
            nextFire = Time.time + fireRate;

            //if (score.subtractScore(playerId) > 0) {
                PhotonNetwork.Instantiate(platform.name, new Vector3(
                    platformWrapper.transform.position.x,
                    platformWrapper.transform.position.y - (1 + platformHeight),
                    platformWrapper.transform.position.z
                ), transform.rotation, 0);
            //}
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
}