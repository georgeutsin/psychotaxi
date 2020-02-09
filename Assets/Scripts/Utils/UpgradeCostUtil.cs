using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCostUtil
{
    public static int numberOfLevels = 10;
    public static int maxAccelerationMultiplier = 50;
    public static int maxBodyMultiplier = 60;
    public static int maxEfficiencyMultiplier = 70;
    public static int GetCost(string upgrade, int level, LevelDifficultyScriptableObject difficulty)
    {
        int cost = 0;
        switch(upgrade)
        {
            case "Racer":
                cost = 5000;
                break;
            case "Truck":
                cost = 5000;
                break;
            case "Acceleration":
                cost =(int) (10 * CurveUtil.MultiplierSample(
                    difficulty.accelerationUpgradeMuliplierCurve,
                    (float)level / numberOfLevels,
                    maxAccelerationMultiplier));
                break;
            case "Body":
                cost = (int) (11 * CurveUtil.MultiplierSample(
                    difficulty.bodyUpgradeMuliplierCurve,
                    (float)level / numberOfLevels,
                    maxBodyMultiplier));
                break;
            case "Efficiency":
                cost = (int) (12 * CurveUtil.MultiplierSample(
                    difficulty.efficiencyUpgradeMuliplierCurve,
                    (float)level / numberOfLevels,
                    maxEfficiencyMultiplier));
                break;
            default:
                break;
        }

        return cost;
    }
}
