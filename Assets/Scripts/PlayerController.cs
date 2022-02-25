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
    void Start()
    {   
        
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
        else if (Input.GetKeyDown(KeyCode.Space) && playerJumpAmount >= 1)
        {
            transform.Translate(Vector3.up * Time.deltaTime * playerJumpSpeed);
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
        else if(other.gameObject.CompareTag("target") && isAttacking == true)
        {
            Destroy(other.gameObject);
        }
    }
}
