using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinCondition : MonoBehaviour
{
    public GameObject winScreen;

    private bool wined = false;
    public int nextScene = 2;
    public ScreenFader screenFader;

    void Start()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
    }

    void Update()
    {
        /*if (wined)
        {
            CheckWinCondition();
        }*/
    }

    private void CheckWinCondition()
    {
        /*if ()
        {
            TriggerWin();
        }*/
    }

    private void TriggerWin()
    {
        if (winScreen != null && wined)
        {
            wined = false;
            winScreen.SetActive(true);
            StartCoroutine(LoadFirstSceneAfterDelay());
        }
    }

    private IEnumerator Level()
    {
        yield return screenFader.FadeToBlack();
        SceneManager.LoadScene(nextScene);
    }

    private IEnumerator LoadFirstSceneAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(Level());
        }
    }
}
