using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField, Tooltip("UI to choose player")]
    public PlayerChooser[] choosers;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // check how many players (player inputs?) are present
        GameObject[] players = GameObject.FindGameObjectsWithTag("HumanPlayer");
        for (int i=0; i < 4; i++) {
            TextMeshProUGUI c = choosers[i].GetComponent<TextMeshProUGUI>();
            if (i < players.Length) {
                string character = players[i].GetComponent<HumanPlayer>().GetCharacter();
                c.text = character + "\nP" + (i+1);
            } else {
                c.text = "";
            }
        }

        // print 

        // create a select UX for each player
        
    }
}
