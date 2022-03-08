using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWatcher : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Target[] targets;
    void Update()
    {
        if(targets.Length == player.targetsDestroyed)
        {
            Debug.Log("You Win!");
        }
    }
}
