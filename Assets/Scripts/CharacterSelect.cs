using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelect : MonoBehaviour {
    [SerializeField, Tooltip("UI to choose player")]
    public PlayerChooser[] choosers;

    [SerializeField, Tooltip("Status shown at bottom of page")]
    public TextMeshProUGUI status;

    [SerializeField]
    public Button nextButton;

    [SerializeField]
    public Button backButton;

    const int MAX_PLAYERS = 4; // This is a property of the PlayerInputManager, as well

    Color32 green = new Color32(0, 255, 0, 255);
    Color32 white = new Color32(255, 255, 255, 255);
    Color32 gray = new Color32(150, 150, 150, 150);

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // Show character selection for each active player
        // TODO: Rather than computing this in the UpdateLoop, 
        //      change this to only update on player JOIN or LEAVE event
        HumanPlayer[] players = getHumanPlayers();
        for (int i = 0; i < MAX_PLAYERS; i++) {
            // Set the text
            TextMeshProUGUI c = choosers[i].GetComponent<TextMeshProUGUI>();
            if (i < players.Length) {
                // active players
                HumanPlayer hp = players[i];
                string character = hp.GetCharacter();
                c.text = hp.GetCharacter() + "\n\n(P" + (i + 1) + ")";
                c.color = hp.GetCharacterConfirmed() ? green : white;

            } else {
                // inactive (not yet joined) players
                c.text = "(P" + (i + 1) + ")";
                c.color = gray;
            }
        }

        // If all players are ready, then show PRESS START TO CONTINUE text
        // Otherwise, hide that text
        if (players.Length == 0) {
            status.gameObject.SetActive(true);
            status.text = "waiting for players...";
        } else {
            status.gameObject.SetActive(allReady());
            nextButton.gameObject.SetActive(allReady());
            if (allReady()) {
                // TODO: Figure out why it's sometimes not showing the proper highlighted state,
                // but *does* seem to be getting selected.
                // Without the below line, I'm unable to interact with the button.
                nextButton.Select();
            }
            status.text = "Press START/ENTER to continue";
        }
    }


    private void OnEnable() {
        foreach (var player in getHumanPlayers()) {
            player.SetupCharacterSelect();
        }
    }

    private void OnDisable() {
        foreach (var player in getHumanPlayers()) {
            player.CleanupCharacterSelect();
        }
    }

    private HumanPlayer[] getHumanPlayers() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("HumanPlayer");
        HumanPlayer[] hps = new HumanPlayer[players.Length];
        for (int i = 0; i < players.Length; i++) {
            hps[i] = players[i].GetComponent<HumanPlayer>();
        }
        return hps;
    }

    // public void ContinueIfAllReady() {
    //     HumanPlayer[] hps = getHumanPlayers();
    //     if (allReady()) {
    //         levelSelect.gameObject.SetActive(true);
    //         this.gameObject.SetActive(false);
    //     }
    // }

    private bool allReady() {
        // every character must be confirmed before we're all ready
        HumanPlayer[] hps = getHumanPlayers();
        for (int i = 0; i < hps.Length; i++) {
            if (!hps[i].GetCharacterConfirmed()) {
                return false;
            }
        }
        return true;
    }
}
