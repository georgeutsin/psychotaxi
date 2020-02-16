using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI DetailsText;
    public TextMeshProUGUI BonusCoinsText;
    public GameObject PlayAgainButton;
    public GameObject MainMenuButton;
    public GameObject DoubleCoinsButton;
    public GameObject webview;
    public GameObject loadingScreen;
    public Slider bgSlider;

    int bonusCoins;
    float sliderBGLeft;

    void Update()
    {
        UpdateDoubleCoinsButtonBackground();
    }

    void UpdateDoubleCoinsButtonBackground()
    {
        if (sliderBGLeft > 0f)
        {
            sliderBGLeft -= Time.deltaTime / 10f;
        }
        bgSlider.value = sliderBGLeft;

        if (sliderBGLeft <= 0f)
        {
            DoubleCoinsButton.GetComponent<Button>().interactable = false;
        }
    }

    public void UpdateText(int score, int highscore, int coins, int totalCoins)
    {
        DetailsText.text = "Score: " + score + "\n" +
            "High score: " + highscore + "\n" +
            "\n" +
            "coins: " + coins + "\n" +
            "total coins: " + totalCoins;
        bonusCoins = coins;
        BonusCoinsText.text = "";
    }

    public void ShowButtons(bool shouldShowDoubleButton)
    {
        PlayAgainButton.SetActive(true);
        MainMenuButton.SetActive(true);
        if (shouldShowDoubleButton)
        {
            DoubleCoinsButton.GetComponent<Button>().interactable = true;
            DoubleCoinsButton.SetActive(true);
            sliderBGLeft = 1f;
        }
    }

    public void HideButtons()
    {
        PlayAgainButton.SetActive(false);
        MainMenuButton.SetActive(false);
        DoubleCoinsButton.SetActive(false);   
    }

    public void DoubleCoinsPressed()
    {
        Time.timeScale = 0f;
        StartCoroutine(webview.GetComponent<TagBullWebView>().Create());
    }

    public void TagBullSuccess()
    {
        webview.SetActive(false);
        loadingScreen.SetActive(false);
        DoubleCoinsButton.SetActive(false);
        Time.timeScale = 1.0f;

        StateManager.AddCoins(bonusCoins);
        BonusCoinsText.text = "bonus coins: " + bonusCoins + "\n" +
            "new total coins: " + StateManager.GetCoins();
    }

    public void TagBullCancel()
    {
        webview.SetActive(false);
        loadingScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
