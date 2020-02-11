using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameStateScriptableObject gameState;
    public LevelDifficultyScriptableObject difficulty;
    public TextMeshProUGUI coinText;
    public ShopRow[] shopRows;
    public ShopModelRow modelRow;
    // Start is called before the first frame update
    void Start()
    {
        SetCoinText();
    }

    public void RefreshAllRows()
    {
        foreach (ShopRow row in shopRows)
        {
            row.SetRow();
        }
        modelRow.SetRow();
        SetCoinText();
    }

    void SetCoinText()
    {
        coinText.text = "$" + GetCurrentCoins();
    }

    public int GetCurLevelFromState(string upgradeType)
    {
        return StateManager.GetUpgradeLevel("Cur" + upgradeType);
    }

    public int GetUnlockedLevelFromState(string upgradeType)
    {
        return StateManager.GetUpgradeLevel("Unlocked" + upgradeType);
    }

    public int SetCurLevelToState(string upgradeType, int level)
    {
        StateManager.SetUpgradeLevel("Cur" + upgradeType, level);
        PropagateState();
        return level;
    }

    public int SetUnlockedLevelToState(string upgradeType, int level)
    {
        StateManager.SetUpgradeLevel("Unlocked" + upgradeType, level);
        return level;
    }

    public int GetSelectedModel()
    {
        return StateManager.GetUpgradeLevel("SelectedModel") - 1;
    }
    public int SetSelectedModel(int level)
    {
        level = StateManager.SetUpgradeLevel("SelectedModel", level + 1);
        PropagateState();
        return level;
    }

    public int UpgradeCost(string upgradeType, int level)
    {
        return UpgradeCostUtil.GetCost(upgradeType, level, difficulty);
    }

    public int GetCurrentCoins()
    {
        return StateManager.GetCoins();
    }

    void SubtractCoins(int amount)
    {
        StateManager.RemoveCoins(amount);
    }

    public void BuyUpgrade(string upgradeType, int level)
    {
        int cost = UpgradeCost(upgradeType, level);
        if (GetCurrentCoins() >= cost)
        {
            SetUnlockedLevelToState(upgradeType, level);
            SubtractCoins(cost);
            SetCoinText();
        }
        SetCoinText();
        RefreshAllRows();
        PropagateState();
    }

    public void PropagateState()
    {
        gameState.UpdateMultipliers();
    }
    public void BackPressed()
    {
        SetModelAndMesh();   
    }

    public void SetModelAndMesh()
    {
        modelRow.ResetModelIdx();
        modelRow.SetRow();
        modelRow.SetMesh(GetSelectedModel());
    }
}
