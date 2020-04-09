using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("CombatManager:Awake()");
        // Spawn a avatar for each human player
        GameObject[] players = GameObject.FindGameObjectsWithTag("HumanPlayer");
        for (int i = 0; i < players.Length; i++)
        {
            HumanPlayer hp = players[i].GetComponent<HumanPlayer>();
            hp.SetupCombat();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        Debug.Log("CombatManager:OnDestroy()");
        // Cleanup player avatars
        GameObject[] players = GameObject.FindGameObjectsWithTag("HumanPlayer");
        for (int i = 0; i < players.Length; i++)
        {
            HumanPlayer hp = players[i].GetComponent<HumanPlayer>();
            hp.CleanupCombat();
        }
    }



}
