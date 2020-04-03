using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string[] levels = new string[]{
        // These must match the exact scene names
        "SandyLevel",
        "DungeonLevel",
    };

    public void PlayGame() {
        string randomLevel = levels[Random.Range(0, levels.Length)]; 
        SceneManager.LoadScene(randomLevel);
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
