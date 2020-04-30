using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndCombatUI : MonoBehaviour {

    public TextMeshProUGUI winner;
    public TextMeshProUGUI[] playersStats;

    public GameObject panel;

    const int MAX_PLAYERS = 4;

    public void Show() {
        panel.SetActive(true);
    }

    public void Hide() {
        panel.SetActive(false);
    }

    public void UpdateStats(HumanPlayer[] humanPlayers) {
        float bestScore = -1 * Mathf.Infinity;
        int winningPlayerIndex = -1;

        for (int i = 0; i < humanPlayers.Length; i++) {
            if (humanPlayers[i] == null) {
                playersStats[i].text = "";
                // TODO: Do we want this? It will resize the layout
                playersStats[i].gameObject.SetActive(false);
            } else {
                int kills = humanPlayers[i].GetKills();
                int deaths = humanPlayers[i].GetDeaths();
                if ((kills - deaths) > bestScore) {
                    winningPlayerIndex = humanPlayers[i].playerInput.playerIndex;
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
