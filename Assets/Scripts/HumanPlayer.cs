using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanPlayer : MonoBehaviour
{
    [SerializeField, Tooltip("Player Input")]
    public PlayerInput playerInput;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
