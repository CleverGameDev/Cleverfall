using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HumanPlayer : MonoBehaviour {
    [SerializeField]
    public PlayerInput playerInput;

    [SerializeField]
    public GameObject playerAvatarPrefab;
    private GameObject playerAvatar;

    private string[] characters = {
        "joy",
        "sadness",
        "anger",
        "disgust",
        "fear",
    };
    private int selectedCharacterIdx = 0;
    bool characterConfirmed = false;

    // Combat stats
    private int kills = 0;
    private int deaths = 0;

    // Start is called before the first frame update
    void Start() {
        Debug.Log("Player joined: " + playerInput.playerIndex);
        // The Human Player persists across scenes
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update() {

    }

    ////////////////////////////
    // Combat (core Gameplay)
    ////////////////////////////

    // What do we need to track during the combat for a player?
    // - Avatar
    // - Combat stats: kills, deaths 
    // - GUI related to the player: health, lives remaining

    public void AddKill() {
        Debug.Log("Player" + playerInput.playerIndex + " got a kill!");
        kills += 1;
    }
    public void AddDeath() {
        Debug.Log("Player" + playerInput.playerIndex + " died");
        deaths += 1;
    }

    public int GetKills() {
        return kills;
    }
    public int GetDeaths() {
        return deaths;
    }

    // setup
    public void SetupCombat() {
        spawnAvatar();

        // Reset combat stats
        kills = 0;
        deaths = 0;

        // Stop listening for menu events
        playerInput.SwitchCurrentActionMap("Combat");
    }

    void spawnAvatar() {
        playerAvatar = Instantiate(playerAvatarPrefab);
        HumanPlayer hp = this.GetComponent<HumanPlayer>();
        playerAvatar.GetComponent<PlayerAvatar>().SetHumanPlayer(hp);

        // Set bg color
        SpriteRenderer ren = playerAvatar.GetComponent<SpriteRenderer>();
        ren.material.SetColor("_Color", getPlayerColor());
    }

    // TODO: Move this to the avatar creation
    private Color getPlayerColor() {
        switch (GetCharacter()) {
            case "joy":
                return Color.yellow;
            case "sadness":
                return Color.blue;
            case "anger":
                return Color.red;
            case "disgust":
                return Color.green;
            case "fear":
                return Color.magenta;
            default:
                return Color.black;
        }
    }

    public void CleanupCombat() {
        // Stop listening for Combat events
        playerInput.currentActionMap.Disable();
        Destroy(playerAvatar);
    }


    // player input event listeners
    private void OnMove(InputValue value) {
        if (playerAvatar == null) {
            // player is dead
            return;
        }

        playerAvatar.GetComponent<PlayerAvatar>()._onMove(value);
    }

    private void OnJump() {
        if (playerAvatar == null) {
            // player is dead. allow respawn by pressing jump
            spawnAvatar();
        }

        playerAvatar.GetComponent<PlayerAvatar>()._onJump();
    }

    private void OnMenu() {
        GameObject.Find("PauseMenu").GetComponent<PauseMenu>().TogglePause();
    }

    //////////////////
    // CharacterSelect
    //////////////////
    public void SetupCharacterSelect() {
        // Stop listening for menu events
        playerInput.SwitchCurrentActionMap("CharacterSelect");
        playerInput.currentActionMap.Enable();
        characterConfirmed = false;
    }

    public void CleanupCharacterSelect() {
        // Stop listening for Combat events
        playerInput.currentActionMap.Disable();
    }

    // player input event listeners
    public void OnNextCharacter() {
        if (characterConfirmed) {
            return;
        }

        selectedCharacterIdx = (selectedCharacterIdx + 1) % characters.Length;
    }
    public void OnPreviousCharacter() {
        if (characterConfirmed) {
            return;
        }

        selectedCharacterIdx = (characters.Length + selectedCharacterIdx - 1) % characters.Length;
    }

    public void OnAccept() {
        characterConfirmed = true;

        // TODO:
        // if (allPlayersConfirmed) {
        //     // Navigate to next screen: level select
        // }
    }

    public void OnCancel() {
        characterConfirmed = false;

        // TODO:
        // if (!characterConfirmed) {
        // Navigate to previous screen: 
        // }

    }

    // public void OnStart() {
    //     GameObject csgo = GameObject.Find("CharacterSelect");
    //     if (csgo == null) {
    //         return; // TODO: this can occur if the page is still loading
    //     }
    //     csgo.GetComponent<CharacterSelect>().ContinueIfAllReady();
    // }

    // other helpers
    public string GetCharacter() {
        return characters[selectedCharacterIdx];
    }
    public bool GetCharacterConfirmed() {
        return characterConfirmed;
    }
}
