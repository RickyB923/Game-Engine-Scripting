using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float playerJumpSpeed = 1f;
    [SerializeField] int playerJumpAmount = 2;
    [SerializeField] float attackSpeed = 0.2f;
    [SerializeField] bool inAir;
    [SerializeField] bool onWall;
    [SerializeField] bool isAttacking;
    public int targetsDestroyed;
    private Rigidbody rb;
    [SerializeField] bool isFacingRight;
    [SerializeField] GameObject attackRight;
    [SerializeField] GameObject attackLeft;

    void Start()
    {   
        targetsDestroyed = 0;
        rb = this.GetComponent<Rigidbody>();
        attackRight.SetActive(false);
        attackLeft.SetActive(false);
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
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
            rb.AddForce(Vector3.up * playerJumpSpeed, ForceMode.Impulse);
            inAir = true;
            playerJumpAmount--;
        }
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
        if(other.gameObject.CompareTag("wall"))
        {
            onWall = false;
        }
    }

    IEnumerator AttackRight(float seconds)
    {
        isAttacking = true;
        attackRight.SetActive(true);
        yield return new WaitForSeconds(seconds);
        isAttacking = false;
        attackRight.SetActive(false);
    }

    IEnumerator AttackLeft(float seconds)
    {
        isAttacking = true;
        attackLeft.SetActive(true);
        yield return new WaitForSeconds(seconds);
        isAttacking = false;
        attackLeft.SetActive(false);
    }
}
