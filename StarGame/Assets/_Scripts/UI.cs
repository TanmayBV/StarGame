using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    PlayerInputs playerInputs;

    public TextMeshProUGUI ScoreText;

    private void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
    }

    private void Update()
    {
        SetScore(playerInputs.Score);
    }

    void SetScore(int score)
    {
        ScoreText.text = "Score: " + score;
    }
}
