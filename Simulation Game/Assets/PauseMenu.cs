using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Slider = UnityEngine.UI.Slider;


public class PauseMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private float _volume;
    [SerializeField] private GameObject container;

    private void Start()
    {
        GameManager.instance.OnPauseToggle += OnGamePauseToggleListener;
        _volumeSlider.value = _volume;
        //_volumeSlider.onValueChanged.AddListener(SetVolume);
        container.SetActive(false);
    }

    private void OnGamePauseToggleListener(bool toggle)
    {
        container.SetActive(toggle);
    }
    
    public void SetVolume(float volume)
    {
        if(volume==0){ volume+=0.00001f;}
        _volume = volume;
        AudioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20); 
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        GameManager.instance.PauseGame(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnDisable()
    {
        GameManager.instance.OnPauseToggle -= OnGamePauseToggleListener;
    }
}
