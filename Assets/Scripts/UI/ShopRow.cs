using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopRow : MonoBehaviour
{
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI PlusButtonText;
    public Button PlusButton;
    public Button MinusButton;
    public UpgradeType Upgrade;

    public Shop shop;

    public enum UpgradeType
    {
        Acceleration,
        Body,
        Efficiency,
    }
    int unlockedLevel;
    int curLevel;
    string upgradeString;

    // Start is called before the first frame update
    void Start()
    {
        MinusButton.onClick.AddListener(MinusButtonClicked);
        PlusButton.onClick.AddListener(PlusButtonClicked);

        upgradeString = Upgrade.ToString();
        SetRow();
    }

    public void SetRow()
    {
        curLevel = shop.GetCurLevelFromState(upgradeString);
        unlockedLevel = shop.GetUnlockedLevelFromState(upgradeString);
        SetMinusButton();
        SetPlusButton();
        SetLevelText();
    }

    void SetMinusButton()
    {
        if (curLevel <= 1)
        {
            MinusButton.interactable = false;
            return;
        }

        MinusButton.interactable = true;
    }

    void SetPlusButton()
    {
        if (curLevel < unlockedLevel)
        {
            PlusButton.interactable = true;
            PlusButtonText.text = "+";
        }
        else
        {
            PlusButtonText.text = "$" + shop.UpgradeCost(upgradeString, unlockedLevel + 1);
            if (shop.GetCurrentCoins() < shop.UpgradeCost(upgradeString, unlockedLevel + 1))
            {
                PlusButton.interactable = false;
            }
            else
            {
                PlusButton.interactable = true;
            }
        }
    }

    void SetLevelText()
    {
        LevelText.text = "Level " + curLevel;
    }

    void MinusButtonClicked()
    {
        if (curLevel <= 1)
        {
            return;
        }

        shop.SetCurLevelToState(upgradeString, curLevel - 1);
        SetRow();
    }

    void PlusButtonClicked()
    {
        if (curLevel == unlockedLevel)
        {
            shop.BuyUpgrade(upgradeString, unlockedLevel + 1);
            unlockedLevel = shop.GetUnlockedLevelFromState(upgradeString);
        }

        shop.SetCurLevelToState(upgradeString, curLevel + 1);
        SetRow();
    }
}
