using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetWatcher : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Target[] targets;
    [SerializeField] public string nextLevel;

    void Update()
    {
        if(targets.Length == player.targetsDestroyed)
        {
            StartCoroutine(LoadNextScene(3));
        }
    }

    IEnumerator LoadNextScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(nextLevel);
    }
}
