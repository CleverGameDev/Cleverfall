using System;
using TMPro;
using UnityEngine;

public class CombatGUI : MonoBehaviour {
    public TextMeshProUGUI countdownText;
    public GameObject topPanel;
    public GameObject bottomPanel;
    public PlayerStatus playerStatusPrefab;
    private PlayerStatus[] statuses;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {


    }

    public void Show() {
        topPanel.SetActive(true);
        bottomPanel.SetActive(true);
    }

    public void Hide() {
        topPanel.SetActive(false);
        bottomPanel.SetActive(false);
    }

    public void SetTimeRemaining(float timeRemaining) {
        // Get a referecne to the timer text
        TimeSpan time = TimeSpan.FromSeconds(timeRemaining);
        countdownText.SetText(time.ToString(@"m\:ss"));
    }

    public void Initialize(HumanPlayer[] hps) {
        statuses = new PlayerStatus[hps.Length];
        for (int i = 0; i < hps.Length; i++) {
            PlayerStatus ps = Instantiate(playerStatusPrefab, bottomPanel.gameObject.transform);
            ps.HumanPlayer = hps[i];
            statuses[i] = ps;
        }
    }
}
