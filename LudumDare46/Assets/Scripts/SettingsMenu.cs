using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;

    public void SetVolume(float volume)
    {

        audioMixer.SetFloat("Volume", slider.value);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}