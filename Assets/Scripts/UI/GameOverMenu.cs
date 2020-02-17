using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Analytics;
using UnityEngine.Advertisements;

public class GameOverMenu : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    string gameId = "3472791";
#elif UNITY_ANDROID
    string gameId = "3472790";
#endif
    string myPlacementId = "rewardedVideo";
    bool isDebug = false;

    public TextMeshProUGUI DetailsText;
    public TextMeshProUGUI BonusCoinsText;
    public GameObject PlayAgainButton;
    public GameObject MainMenuButton;
    public GameObject TagBullRewardButton;
    public GameObject AdRewardButton;
    int bonusCoins;

    void Start()
    {
        Advertisement.Initialize(gameId, isDebug);
        Advertisement.AddListener(this);

        AdRewardButton.SetActive(false);
        TagBullRewardButton.SetActive(false);
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

    public void ShowButtons(bool tagbullActivityAvailable, bool unityAdsEnabled)
    {
        PlayAgainButton.SetActive(true);
        MainMenuButton.SetActive(true);
        if (tagbullActivityAvailable)
        {
            TagBullRewardButton.SetActive(true);
            TagBullRewardButton.GetComponent<TagBullRewardButton>().StartButtonTimer();
            return;
        }
        if (unityAdsEnabled && Advertisement.IsReady(myPlacementId))
        {
            AdRewardButton.SetActive(true);
            AdRewardButton.GetComponent<AdRewardButton>().StartButtonTimer();
            return;
        }
    }

    public void HideButtons()
    {
        PlayAgainButton.SetActive(false);
        MainMenuButton.SetActive(false);
        TagBullRewardButton.SetActive(false);
        AdRewardButton.SetActive(false);
    }

    public void RewardSuccess()
    {
        TagBullRewardButton.SetActive(false);
        AdRewardButton.SetActive(false);

        Time.timeScale = 1.0f;

        StateManager.AddCoins(bonusCoins);
        BonusCoinsText.text = "bonus coins: " + bonusCoins + "\n" +
            "new total coins: " + StateManager.GetCoins();
    }

    public void RewardCancel()
    {
        Time.timeScale = 1.0f;
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
             AnalyticsEvent.ScreenVisit("AdSuccess");
            RewardSuccess();
        }
        else if (showResult == ShowResult.Skipped)
        {
            RewardCancel();
        }
        else if (showResult == ShowResult.Failed)
        {
            RewardCancel();
            AdRewardButton.SetActive(Advertisement.IsReady(myPlacementId));
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}
