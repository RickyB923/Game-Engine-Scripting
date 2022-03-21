using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetWatcher : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Target[] targets;
    [SerializeField] public string nextLevel;

    void Update() // Loads next scene if all targets are destroyed
    {
        if(targets.Length == player.targetsDestroyed)
        {
            StartCoroutine(LoadNextScene(3));
        }
    }

    IEnumerator LoadNextScene(float seconds) // Loads next scene
    {
        Debug.Log("Complete!");
        if(nextLevel != "")
        {
            yield return new WaitForSeconds(seconds);
            SceneManager.LoadScene(nextLevel);
        }
    }
}
