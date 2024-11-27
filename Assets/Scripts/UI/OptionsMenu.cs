using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer AudioMixer;
    [SerializeField] private ScreenFader screenFader;

    public void FullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void ChangeVolume(float volume)
    {
        AudioMixer.SetFloat("Volumen", Mathf.Log10(volume)*20);
    }

    public void Play()
    {
        Time.timeScale = 1f;
        screenFader.FadeToBlack();
        SceneManager.LoadScene(1);
    }

    public void Credits(int cred)
    {
        Time.timeScale = 1f;
        screenFader.FadeToBlack();
        SceneManager.LoadScene(cred);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
