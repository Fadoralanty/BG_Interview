using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Action<bool> OnPauseToggle;
    private bool isGamePaused;
    public bool isGameOver;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        isGameOver = false;
        isGamePaused = false;
    }

    private void Start()
    {
        AudioManager.instance.play("bg");
    }

    private void PauseGame(bool pause)
    {
        if (pause)
        {
            isGamePaused = true;
            OnPauseToggle?.Invoke(true);
            Time.timeScale = 0f;
        }
        else
        {
            isGamePaused = false;
            OnPauseToggle?.Invoke(false);

            Time.timeScale = 1f;
        }
    }
    
}
