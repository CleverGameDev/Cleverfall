using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(AudioSource))]
public class CombatManager : MonoBehaviour {
    float roundLength = 45; // seconds
    float currCountdownValue;

    public CombatGUI combatGUI;
    public EndCombatUI endCombatUI;

    public PauseMenu pauseMenuPrefab;
    private PauseMenu pauseMenu;

    public AudioClip backgroundMusic;

    private AudioSource _audio;

    HumanPlayer[] humanPlayers;

    void Awake() {
        Debug.Log("CombatManager:Awake()");
        // Spawn a avatar for each human player
        GameObject[] players = GameObject.FindGameObjectsWithTag("HumanPlayer");
        humanPlayers = new HumanPlayer[G.Instance.MAX_PLAYERS];
        for (int i = 0; i < players.Length; i++) {
            HumanPlayer hp = players[i].GetComponent<HumanPlayer>();
            humanPlayers[i] = hp;
            hp.SetupCombat(this);
        }

        _audio = this.GetComponent<AudioSource>();
        _audio.clip = backgroundMusic;

        startCombat();
    }

    // startCombat sets up victory conditions for combat
    void startCombat() {
        combatGUI.Show();
        endCombatUI.Hide();

        // One end condition is a timer, and the player with the most kills wins
        StartCoroutine(startCountdown(roundLength));

        // TODO: Add support for another game type, where "players start with n lives"
        // if exactly <=1 player has >0 lives then round over!
        // ... if only 1 survivor: they win
        // ... if 0 survivors: draw

        _audio.Play();
    }

    public IEnumerator startCountdown(float countdownValue) {
        currCountdownValue = countdownValue;
        while (true) {
            combatGUI.SetTimeRemaining(currCountdownValue);
            if (currCountdownValue <= 0) {
                break;
            }

            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        endCombat();
    }

    void endCombat() {
        Debug.Log("CombatManager:EndCombat()");

        combatGUI.Hide();
        endCombatUI.UpdateStats(humanPlayers);
        endCombatUI.Show();

        _audio.Stop();
    }

    void OnDestroy() {
        Debug.Log("CombatManager:OnDestroy()");
        // Cleanup player avatars
        GameObject[] players = GameObject.FindGameObjectsWithTag("HumanPlayer");
        for (int i = 0; i < players.Length; i++) {
            HumanPlayer hp = players[i].GetComponent<HumanPlayer>();
            hp.CleanupCombat();
        }
    }

    ////////////////////
    // Pause
    ////////////////////
    private bool isPaused = false;
    // TogglePause sets isPaused to the opposite of its current value
    public void TogglePause() {
        Pause(!isPaused);
    }

    // Pause sets isPaused value explicitly 
    public void Pause(bool b) {
        isPaused = b;
        Time.timeScale = isPaused ? 0f : 1f;
        if (b) {
            pauseMenu = Instantiate(pauseMenuPrefab);
        } else {
            Destroy(pauseMenu.gameObject);
        }
    }

    // IsPaused 
    public bool IsPaused() {
        return isPaused;
    }


}
