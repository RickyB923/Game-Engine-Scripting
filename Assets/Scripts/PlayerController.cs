using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float playerJumpSpeed = 1f;
    [SerializeField] int playerJumpAmount = 2;
    [SerializeField] float playerJumpDelay = 0.3f;
    [SerializeField] int playerGravity;
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

    void Start()
    {   
        // Initializes properties
        targetsDestroyed = 0;
        canJump = true;
        rb = this.GetComponent<Rigidbody>();
        attackRight.SetActive(false);
        attackLeft.SetActive(false);
    }

    void Update()
    {  
        // Gets last known position
        var currentPosition = transform.position;
        GetMoveInput();
        lastPosition = currentPosition;
    }
    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * playerGravity, ForceMode.Acceleration);
        Move();
    }

    void GetMoveInput()
    {
        leftInput = Input.GetAxis("Horizontal");
        rightInput = Input.GetAxis("Horizontal");
        downInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxis("Jump");
        attackInput = Input.GetAxis("Attack");
    }
    // Checks for various movements 
    public void Move()
    {
        // Basic set of input checks that determine left/right player movement and jumping
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
            rb.AddForce(Vector3.down * 2, ForceMode.Impulse); 
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
                    rb.AddForce((Vector3.up + Vector3.left) * (playerJumpSpeed * 0.5f), ForceMode.Impulse);
                }
            }
            else
            {
                if(jumpInput == 1 && canJump)
                {
                    rb.AddForce((Vector3.up + Vector3.right) * (playerJumpSpeed * 0.5f), ForceMode.Impulse);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision other)
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

    private void OnCollisionExit(Collision other)
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

    // Activates right attack
    IEnumerator AttackRight(float seconds)
    {
        isAttacking = true;
        attackRight.SetActive(true);
        yield return new WaitForSeconds(seconds);
        isAttacking = false;
        attackRight.SetActive(false);
    }

    // Activates left attack
    IEnumerator AttackLeft(float seconds)
    {
        isAttacking = true;
        attackLeft.SetActive(true);
        yield return new WaitForSeconds(seconds);
        isAttacking = false;
        attackLeft.SetActive(false);
    }

    IEnumerator JumpDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canJump = true;
    }
}
