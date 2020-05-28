using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    private CombatManager cm;

    void Awake() {
        cm = GameObject.Find("CombatManager").GetComponent<CombatManager>();
    }

    public void Resume() {
        cleanup();
    }

    public void Restart() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);

        cleanup();
    }

    public void QuitToMainMenu() {
        SceneManager.LoadScene("Menu");

        cleanup();
    }

    // The combat manager is responsible for toggling gameplay pause behaviors, 
    // and show/hiding this UI
    //
    // We call this last because the CombatManager destroys this gameObject when done
    private void cleanup() {
        cm.Pause(false);
    }
}
