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
    private bool shouldMove;

    void Start()
    {
        transform.position = position1.position;
    }
    void Update()
    {
        if(moves)
        {
            if(Vector3.Distance(transform.position, position1.position) == 0)
            {
                Move(position2.position, position1.position);
            } 
        }
    }
    void OnDestroy()
    {
        player.targetsDestroyed++;
    }

    void Move(Vector3 pos1, Vector3 pos2)
    {       
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(pos1, pos2, step);
    }
}

// Tring to get targets to move in between two points
// currently targets no longer get destoryed by player
