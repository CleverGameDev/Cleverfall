﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // TODO: Allow for more flexible user input methods
        if(Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    // Sets the pause state to the opposite of the current state
    public void TogglePause() {
        pause(!GameIsPaused);
    }

    private void pause(bool isPaused) {
        pauseMenuUI.SetActive(isPaused);
        GameIsPaused = isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    // Restart
    public void Restart() {
        pause(false);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);

    }

    // Quit
    public void QuitToMainMenu() {
        pause(false);
        int previousScene = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(previousScene);
    }
}