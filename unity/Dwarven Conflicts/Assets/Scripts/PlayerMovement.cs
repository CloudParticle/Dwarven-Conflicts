using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private float moveSpeed = 3f;
    private Animator anim;
    int jumpHash = Animator.StringToHash("jump");
    int walkHash = Animator.StringToHash("walk");
    private AnimatorStateInfo currentBaseState;         // a reference to the current state of the animator, used for base layer



    // Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0); // set our currentState variable to the current state of the Base Layer (0) of animation

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        bool j = Input.GetButtonDown("Jump");

        movement(x, y, j);

        print(currentBaseState.fullPathHash);
        anim.SetFloat(walkHash, x);
}

    void movement (float x, float y, bool jump) {
        float forwardMovement = x * moveSpeed * Time.deltaTime;        
        transform.Translate(forwardMovement, 0, 0);

        if (jump) {
            anim.SetTrigger(jumpHash);
        }
    }
}