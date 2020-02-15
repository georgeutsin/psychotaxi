using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI DetailsText;
    public GameObject PlayAgainButton;
    public GameObject MainMenuButton;

    public void UpdateText(int score, int highscore, int coins, int totalCoins)
    {
        DetailsText.text = "Score: " + score + "\n" +
            "High score: " + highscore + "\n" +
            "\n" +
            "coins: " + coins + "\n" +
            "total coins: " + totalCoins;

    }

    public void ShowButtons()
    {
        PlayAgainButton.SetActive(true);
        MainMenuButton.SetActive(true);
    }

    public void HideButtons()
    {
        PlayAgainButton.SetActive(false);
        MainMenuButton.SetActive(false);
    }
}
