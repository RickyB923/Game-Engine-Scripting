using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    void OnTriggerEnter(Collider other) // Destroys the target upon collision
    {
        if(other.gameObject.CompareTag("target"))
        {
            Destroy(other.gameObject);
        }
    }
}
