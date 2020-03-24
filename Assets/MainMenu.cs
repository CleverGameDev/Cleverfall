using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        int currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(currentLevel);
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
