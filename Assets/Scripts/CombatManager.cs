using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(AudioSource))]
public class CombatManager : MonoBehaviour {
    float roundLength = 45; // seconds
    float currCountdownValue;

    public CombatGUI combatGUI;
    public EndCombatUI endCombatUIPrefab;
    private EndCombatUI endCombatUI;

    public PauseMenu pauseMenuPrefab;
    private PauseMenu pauseMenu;

    public AudioClip backgroundMusic;

    private AudioSource _audio;
    private bool combatIsOver = false;

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
        Debug.Log("CombatManager:startCombat()");
        combatIsOver = false;
        combatGUI.Show();
        Pause(false);

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
        Debug.Log("CombatManager:endCombat()");
        combatIsOver = true;

        Pause(true); // stop combat from running and further game interaction

        combatGUI.Hide();

        endCombatUI = Instantiate(endCombatUIPrefab);
        endCombatUI.UpdateStats(humanPlayers);

        _audio.Stop();
    }

    void OnDestroy() {
        Debug.Log("CombatManager:OnDestroy()");
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
    public bool IsPaused() {
        return isPaused;
    }

    // Pause or unpause the game
    public void Pause(bool b) {
        isPaused = b;
        Time.timeScale = isPaused ? 0f : 1f;

        // Toggle player input on/off, so you can't e.g. send a "jump" event when
        // you're looking at the PauseMenu or EndOfCombat UI
        GameObject[] players = GameObject.FindGameObjectsWithTag("HumanPlayer");
        for (int i = 0; i < players.Length; i++) {
            HumanPlayer hp = players[i].GetComponent<HumanPlayer>();
            hp.EnablePlayerInput(!isPaused);
        }

        if (!combatIsOver) {
            // While we're in the combat, show/hide the PauseMenu
            if (b) {
                pauseMenu = Instantiate(pauseMenuPrefab);
            } else {
                if (pauseMenu != null) {
                    Destroy(pauseMenu.gameObject);
                }
            }
        }
    }


}
