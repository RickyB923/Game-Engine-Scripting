using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float playerJumpSpeed = 15f;
    [SerializeField] int playerJumpAmount = 2;
    [SerializeField] float playerJumpDelay = 0.3f;
    [SerializeField] float playerWallJumpSpeed = 2f;
    [SerializeField] float playerWallSpeed = 1f;
    [SerializeField] float playerDropSpeed = 3f;
    [SerializeField] float playerGravity = 15f;
    [SerializeField] float attackSpeed = 0.2f;
    [SerializeField] bool inAir;
    [SerializeField] bool onWall;
    [SerializeField] bool isAttacking;
    [SerializeField] bool isFacingRight;
    [SerializeField] GameObject attackRight;
    [SerializeField] GameObject attackLeft;    
    public int targetsDestroyed;
    public Vector3 lastPosition;
    private Rigidbody rb;
    private float leftInput;
    private float rightInput;
    private float downInput;
    private float jumpInput;
    private float attackInput;
    private bool canJump;

    void Start() // Initializes properties
    {   
        targetsDestroyed = 0;
        canJump = true;
        rb = this.GetComponent<Rigidbody>();
        attackRight.SetActive(false);
        attackLeft.SetActive(false);
    }

    void Update() // Gets last known position
    {  
        var currentPosition = transform.position;
        GetMoveInput();
        lastPosition = currentPosition;
    }
    void FixedUpdate() // Adds gravity and moves player rigidbody
    {
        rb.AddForce(Vector3.down * playerGravity, ForceMode.Acceleration);
        Move();
    }

    void GetMoveInput() //Checks for player input
    {
        leftInput = Input.GetAxis("Horizontal");
        rightInput = Input.GetAxis("Horizontal");
        downInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxis("Jump");
        attackInput = Input.GetAxis("Attack");
    }
    public void Move() // Moves player rigidbody if the corresponding input is met
    {
        // Input check that determines left, right, down and jump movement
        if(leftInput == -1)
        {
            transform.Translate(Vector3.left * Time.fixedDeltaTime * playerSpeed);
            isFacingRight = false;
        }
        if (rightInput == 1)
        {
            transform.Translate(Vector3.right * Time.fixedDeltaTime * playerSpeed);
            isFacingRight = true;
        }
        if (downInput == 1)
        {
            rb.AddForce(Vector3.down * playerDropSpeed, ForceMode.Impulse); 
        }
        if (jumpInput == 1 && playerJumpAmount != 0)
        {
            if(canJump)
            {
                rb.AddForce(Vector3.up * playerJumpSpeed, ForceMode.Impulse);
                canJump = false;
                StartCoroutine(JumpDelay(playerJumpDelay));
                playerJumpAmount--;
            }
        }
        // Input check that determines attacking
        if (attackInput == 1)
        {
            if(!isFacingRight)
            {
                StartCoroutine(AttackRight(attackSpeed));
            }
            else
            {
                StartCoroutine(AttackLeft(attackSpeed));
            }
        }
        // Input check that determines wall jumping
        if(onWall && inAir)
        {
            if(isFacingRight)
            {
                if(jumpInput == 1 && canJump)
                {
                    rb.AddForce(Vector3.up * playerWallJumpSpeed, ForceMode.Impulse);
                    rb.AddForce((Vector3.left * playerWallSpeed) * 0.3f, ForceMode.Impulse);
                }
            }
            else
            {
                if(jumpInput == 1 && canJump)
                {
                    rb.AddForce(Vector3.up * playerWallJumpSpeed, ForceMode.Impulse);
                    rb.AddForce((Vector3.right * playerWallSpeed) * 0.3f, ForceMode.Impulse);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision other) // Checks for collision with floors and walls
    {
        if(other.gameObject.CompareTag("floor"))
        {
            inAir = false;
            playerJumpAmount = 2;
        }
        if(other.gameObject.CompareTag("wall"))
        {
            onWall = true;
        }
    }
    private void OnCollisionExit(Collision other) // Resets bools upon leaving collision
    {
        if(other.gameObject.CompareTag("floor"))
        {
            inAir = true;
        }
        if(other.gameObject.CompareTag("wall"))
        {
            onWall = false;
        }
    }
    IEnumerator AttackRight(float seconds) // Activates right attack
    {
        isAttacking = true;
        attackRight.SetActive(true);
        yield return new WaitForSeconds(seconds);
        isAttacking = false;
        attackRight.SetActive(false);
    }
    IEnumerator AttackLeft(float seconds) // Activates left attack
    {
        isAttacking = true;
        attackLeft.SetActive(true);
        yield return new WaitForSeconds(seconds);
        isAttacking = false;
        attackLeft.SetActive(false);
    }
    IEnumerator JumpDelay(float seconds) // Delays when the player can jump
    {
        yield return new WaitForSeconds(seconds);
        canJump = true;
    }
}
