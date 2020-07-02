using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class EndCombatUI : MonoBehaviour {

    public TextMeshProUGUI winner;
    public TextMeshProUGUI[] playersStats;

    public GameObject panel;

    public void UpdateStats(HumanPlayer[] humanPlayers) {
        float bestScore = -1 * Mathf.Infinity;
        int winningPlayerIndex = -1;

        for (int i = 0; i < G.Instance.MAX_PLAYERS; i++) {
            playersStats[i].text = "";
            playersStats[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < humanPlayers.Length; i++) {
            playersStats[i].gameObject.SetActive(true);

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
        winner.text = "Player" + winningPlayerIndex + " wins!";
    }

    public void QuitToMainMenu() {
        SceneManager.LoadScene("Menu");
    }
}
