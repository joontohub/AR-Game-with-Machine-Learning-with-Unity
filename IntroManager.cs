using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public float waitTime = 4f; // video play time

    void Start()
    {
        StartCoroutine(WaitForIntro());
    }

    IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(1); // 1: build index of the next scene
    }

}
