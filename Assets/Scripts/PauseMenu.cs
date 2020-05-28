using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenuUI;
    private CombatManager cm;

    void Awake() {
        cm = GameObject.Find("CombatManager").GetComponent<CombatManager>();
    }

    public void Show(bool b) {
        pauseMenuUI.SetActive(b);
    }

    public void Resume() {
        cm.Pause(false);
    }

    // Restart
    public void Restart() {
        cm.Pause(false);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    // Quit
    public void QuitToMainMenu() {
        cm.Pause(false);
        SceneManager.LoadScene("Menu");
    }
}
