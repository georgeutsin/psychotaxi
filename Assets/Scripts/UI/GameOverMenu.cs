using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI DetailsText;

    public void UpdateText(int score, int highscore, int coins, int totalCoins)
    {
        DetailsText.text = "Score: " + score + "\n" +
            "High score: " + highscore + "\n" +
            "\n" +
            "coins: " + coins + "\n" +
            "total coins: " + totalCoins;

    }
}
