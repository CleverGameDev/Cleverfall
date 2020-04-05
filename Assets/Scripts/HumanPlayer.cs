using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class HumanPlayer : MonoBehaviour
{
    [SerializeField]
    public PlayerInput playerInput;

    [SerializeField]
    public GameObject playerAvatarPrefab;
    private GameObject playerAvatar;

    private string[] characters = {
        "larry", 
        "curly", 
        "moe"
    };
    private int selectedCharacterIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player joined: " + playerInput.playerIndex);
        // The Human Player persists across scenes
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //////////////////
    // Gameplay
    //////////////////
    // setup
    public void SetupCombat() {
        // Stop listening for menu events
        playerAvatar = Instantiate(playerAvatarPrefab);
        playerInput.SwitchCurrentActionMap("Combat");
    }

    public void CleanupCombat() {
        // Stop listening for Combat events
        playerInput.SwitchCurrentActionMap("MainMenu");
        Destroy(playerAvatar); // TODO: unity doesnt like the Destroy here
    }


    // input event listeners
    private void OnMove(InputValue value) {
        PlayerAvatar pa = playerAvatar.GetComponent<PlayerAvatar>();
        pa._onMove(value);
    }

    private void OnJump() {
        PlayerAvatar pa = playerAvatar.GetComponent<PlayerAvatar>();
        pa._onJump();
    }

    private void OnMenu() {
        PlayerAvatar pa = playerAvatar.GetComponent<PlayerAvatar>();
        pa._onMenu();
    }

    //////////////////
    // Menu
    //////////////////
    // TODO: Only allow making these changes on the CharacterSelect screen
    public void OnNextCharacter() {
        selectedCharacterIdx = (selectedCharacterIdx + 1 ) % characters.Length;
    }
    public void OnPreviousCharacter() {
        selectedCharacterIdx = (characters.Length + selectedCharacterIdx - 1 ) % characters.Length;
    }

    public string GetCharacter() {
        return characters[selectedCharacterIdx];
    }
}
