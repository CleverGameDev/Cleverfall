﻿using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class LevelSelect : MonoBehaviour
{

    private TextMeshProUGUI levelText;
    private string[] levels = new string[]{
        // These must match the exact scene names
        "SandyLevel",
        "DungeonLevel",
    };
    private int selected = 0;

    void Start() {
        setLevelText();
    }

    void Awake() {

    }

    public void PlayGame() {
        SceneManager.LoadScene(selectedLevelName());
    }

    private string selectedLevelName() {
        return levels[selected];
    }

    public void NextLevel() {
        selected = (selected + 1) % levels.Length;
        setLevelText();
    }

    public void PreviousLevel() {
        selected = (levels.Length + selected - 1) % levels.Length;
        setLevelText();
    }

    void setLevelText() {
        levelText = GameObject.Find("LevelName").GetComponent<TextMeshProUGUI>();
        levelText.text = selectedLevelName();
    }
}