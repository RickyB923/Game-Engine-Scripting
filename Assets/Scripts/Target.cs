using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] PlayerController player;
    void OnDestroy()
    {
        player.targetsDestroyed++;
    }
}
