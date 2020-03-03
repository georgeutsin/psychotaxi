using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class AdRewardButton : MonoBehaviour
{
    float sliderBGLeft;
    public Slider bgSlider;
    string myPlacementId = "rewardedVideo";
    public Button myButton;

    void Start()
    {
        // Map the ShowRewardedVideo function to the button’s click listener:
        if (myButton) myButton.onClick.AddListener(ShowRewardedVideo);
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

    // Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        Advertisement.Show(myPlacementId);
    }

    public void StartButtonTimer()
    {
        myButton.interactable = true;
        sliderBGLeft = 1f;
    }
}