using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject playbutton;
    [SerializeField] GameObject settingsbutton;
    [SerializeField] GameObject creditbutton;
    [SerializeField] GameObject controlbutton;

    [SerializeField] GameObject settingpanel;
    [SerializeField] GameObject creditpanel;
    [SerializeField] GameObject controlpanel;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buttonClick;
    [SerializeField] AudioSource menuMusic;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        QualitySettings.vSyncCount = 0;
        audioSource.Play();
    }
    public void playButton()
    {
        SceneManager.LoadScene(1);
    }
    public void SetFps(int index)
    {
        if (index == 0)
        {
            Application.targetFrameRate = 60;
        }
        else if (index == 1)
        {
            Application.targetFrameRate = 144;
        }
        else if (index == 2)
        {
            Application.targetFrameRate = 165;
        }
        else if (index == 3)
        {
            Application.targetFrameRate = -1;
        }
    }
    float sensX =700f, sensY=700f;
    public void SensX(float x)
    {
        sensX = x * 1000f;
        sensY = x * 1000f;
        VoiceInfo.instance.sensX = sensX;
        VoiceInfo.instance.sensY = sensY;

        print(x);
    }
    public void settingsButton()
    {
        audioSource.clip = buttonClick;
        audioSource.Play();
        settingpanel.SetActive(true);
        playbutton.SetActive(false);
        settingsbutton.SetActive(false);
        creditbutton.SetActive(false);
        controlbutton.SetActive(false);
    }
    public void creditButton()
    {
        audioSource.clip = buttonClick;
        audioSource.Play();
        creditpanel.SetActive(true);
        settingsbutton.SetActive(false);
        playbutton.SetActive(false);
        creditbutton.SetActive(false);
        controlbutton.SetActive(false);
    }
    public void controlButton()
    {
        audioSource.clip = buttonClick;
        audioSource.Play();
        controlpanel.SetActive(true);
        settingsbutton.SetActive(false);
        playbutton.SetActive(false);
        creditbutton.SetActive(false);
        controlbutton.SetActive(false);
    }
    public void backToMenu()
    {
        audioSource.clip = buttonClick;
        audioSource.Play();
        settingpanel.SetActive(false);
        creditpanel.SetActive(false);
        controlpanel.SetActive(false);
        settingsbutton.SetActive(true);
        playbutton.SetActive(true);
        creditbutton.SetActive(true);
        controlbutton.SetActive(true);
    }
    public void setVolume(float volume)
    {
        menuMusic.volume = volume;
        audioSource.volume = volume;
        VoiceInfo.instance.voiceLevel = volume;
    }
}
