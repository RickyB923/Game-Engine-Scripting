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

    void Start()
    {
        transform.position = position1.position;
    }
    void Update()
    {
        if(moves)
        {
            if(transform.position != position2.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, position2.position, Time.deltaTime * speed);
            }
            else if(transform.position == position2.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, position1.position, Time.deltaTime * speed);
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
