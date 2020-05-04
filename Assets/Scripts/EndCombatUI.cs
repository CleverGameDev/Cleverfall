using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndCombatUI : MonoBehaviour {

    public TextMeshProUGUI winner;
    public TextMeshProUGUI[] playersStats;

    public GameObject panel;

    public void Show() {
        panel.SetActive(true);
    }

    public void Hide() {
        panel.SetActive(false);
    }

    public void UpdateStats(HumanPlayer[] humanPlayers) {
        float bestScore = -1 * Mathf.Infinity;
        int winningPlayerIndex = -1;

        for (int i = 0; i < G.Instance.MAX_PLAYERS; i++) {
            if (humanPlayers[i] == null) {
                // if null, that means the player isn't
                playersStats[i].text = "";
                playersStats[i].gameObject.SetActive(false);
            } else {
                int kills = humanPlayers[i].GetKills();
                int deaths = humanPlayers[i].GetDeaths();
                int score = kills - deaths;
                if (score > bestScore) {
                    // FUTURE: Handle ties
                    winningPlayerIndex = humanPlayers[i].playerInput.playerIndex;
                    bestScore = score;
                }
                playersStats[i].text = "Player" + i + "\n" +
                   "\n" +
                   "Kills: " + kills + "\n" +
                   "Deaths: " + deaths;
            }
        }
        winner.text = "Player" + winningPlayerIndex + " wins!";
    }




}
