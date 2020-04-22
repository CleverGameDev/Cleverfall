using System;
using TMPro;
using UnityEngine;

public class CombatGUI : MonoBehaviour {
    public TextMeshProUGUI countdownText;
    public GameObject panel;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {


    }

    public void Show() {
        panel.SetActive(true);
    }

    public void Hide() {
        panel.SetActive(false);
    }

    public void SetTimeRemaining(float timeRemaining) {
        // Get a referecne to the timer text
        TimeSpan time = TimeSpan.FromSeconds(timeRemaining);
        countdownText.SetText(time.ToString(@"m\:ss"));
    }

}
