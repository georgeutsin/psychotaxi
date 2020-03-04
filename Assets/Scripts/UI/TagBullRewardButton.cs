using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class TagBullRewardButton : MonoBehaviour
{
    float sliderBGLeft;
    public Slider bgSlider;
    public GameObject webview;
    public GameObject loadingScreen;
    public GameOverMenu GameOverMenu;

    public Button myButton;
    void Start()
    {
        if (myButton) myButton.onClick.AddListener(ShowTagBullActivity);
    }
    void Update()
    {
        UpdateButtonBackground();
    }

    void UpdateButtonBackground()
    {
        if (sliderBGLeft > 0f)
        {
            sliderBGLeft -= Time.deltaTime / 10f;
        }
        bgSlider.value = sliderBGLeft;

        if (sliderBGLeft <= 0f)
        {
            myButton.interactable = false;
        }
    }

    void ShowTagBullActivity()
    {
        AnalyticsEvent.ScreenVisit("TagBullStart");
        Time.timeScale = 0f;
        StartCoroutine(webview.GetComponent<TagBullWebView>().Create());
    }

    public void TagBullSuccess()
    {
        AnalyticsEvent.ScreenVisit("TagBullSuccess");
        webview.SetActive(false);
        loadingScreen.SetActive(false);

        GameOverMenu.RewardSuccess();
    }

    public void TagBullCancel()
    {
        AnalyticsEvent.ScreenVisit("TagBullCancel");
        webview.SetActive(false);
        loadingScreen.SetActive(false);

        GameOverMenu.RewardCancel();
    }

    public void StartButtonTimer()
    {
        sliderBGLeft = 1f;
        myButton.interactable = true;
    }
}
