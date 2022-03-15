using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float playerJumpSpeed = 1f;
    [SerializeField] int playerJumpAmount = 2;
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

    void Start()
    {   
        // Initializes properties
        targetsDestroyed = 0;
        rb = this.GetComponent<Rigidbody>();
        attackRight.SetActive(false);
        attackLeft.SetActive(false);
    }

    void Update()
    {  
        var currentPosition = transform.position;
        // Adds gravity every frame
        rb.AddForce(Vector3.down * playerGravity, ForceMode.Acceleration);
        Move();
        // Gets last known position
        lastPosition = currentPosition;
    }

    // Checks for various movements 
    public void Move()
    {
        // Basic set of input checks that determine left/right player movement and jumping
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * playerSpeed);
            isFacingRight = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * playerSpeed);
            isFacingRight = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && playerJumpAmount != 0)
        {
            while(Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * playerJumpSpeed, ForceMode.Impulse);
            }
            
            playerJumpAmount--;
        }
        // Input check that determines attacking
        if (Input.GetKey(KeyCode.RightShift))
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
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce((Vector3.up + Vector3.left) * playerJumpSpeed, ForceMode.Impulse);
                }
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce((Vector3.up + Vector3.right) * playerJumpSpeed, ForceMode.Impulse);
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
}
