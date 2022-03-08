using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float playerJumpSpeed = 1f;
    [SerializeField] int playerJumpAmount = 2;
    [SerializeField] bool inAir;
    [SerializeField] bool isAttacking;

    private Rigidbody rigidbody;

    void Start()
    {   
        rigidbody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        isAttacking = false;
        Move();
    }

    public void Move()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * playerSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * playerSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && playerJumpAmount != 0)
        {
            rigidbody.AddForce(Vector3.up * playerJumpSpeed, ForceMode.Impulse);
            //transform.Translate(Vector3.up * Time.deltaTime * playerJumpSpeed);
            inAir = true;
            playerJumpAmount--;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            isAttacking = true;
        }
        
    }

    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("floor"))
        {
            inAir = false;
            playerJumpAmount = 2;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("target") && isAttacking == true)
        {
            Destroy(other.gameObject);
        }
    }
}
