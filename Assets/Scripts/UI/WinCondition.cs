using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinCondition : MonoBehaviour
{
    public int nextScene = 2;
    public ScreenFader screenFader;

    private IEnumerator Level()
    {
        yield return screenFader.FadeToBlack();
        SceneManager.LoadScene(nextScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(Level());
        }
    }
}
