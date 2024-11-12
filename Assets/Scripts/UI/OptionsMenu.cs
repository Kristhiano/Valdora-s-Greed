using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer AudioMixer;
    
    public void FullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void ChangeVolume(float volume)
    {
        AudioMixer.SetFloat("Volumen", Mathf.Log10(volume)*20);
    }
}
