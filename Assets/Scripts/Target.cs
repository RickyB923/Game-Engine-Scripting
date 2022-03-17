using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] private float speed;
    [SerializeField] bool moves;
    [SerializeField] Transform position1;
    [SerializeField] Transform position2;
    private Vector3 currentTarget;
    private int moveDirection = -1;
    void Update()
    {
        if(moves)
        {
            if(moveDirection == 1)
            {
                currentTarget = position1.position;
            }
            else if(moveDirection == -1)
            {
                currentTarget = position2.position;
            }

            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

            if(Vector3.Distance(transform.position, currentTarget) == 0f)
            {
                moveDirection *= -1;
            }
        }
    }
    void OnDestroy()
    {
        player.targetsDestroyed++;
    }
}

// Tring to get targets to move in between two points
// currently targets no longer get destoryed by player
