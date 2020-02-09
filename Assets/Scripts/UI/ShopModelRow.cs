using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopModelRow : MonoBehaviour
{
    public TextMeshProUGUI ModelText;
    public TextMeshProUGUI DescText;
    public TextMeshProUGUI BuyButtonText;
    public Button RightButton;
    public Button LeftButton;
    public Button BuyButton;

    public Shop shop;

    public string[] ModelTypes =
    {
        "Standard",
        "Racer",
        "Truck",
    };
    public GameObject player;
    public GameObject[] meshes;
    int unlockedLevel;
    int modelIdx;
    int selectedModelIdx;
    int modelLevel;



    // Start is called before the first frame update
    void Start()
    {
        LeftButton.onClick.AddListener(LeftButtonClicked);
        RightButton.onClick.AddListener(RightButtonClicked);
        BuyButton.onClick.AddListener(BuyButtonClicked);
        ResetModelIdx();
        SetRow();
    }

    public void ResetModelIdx()
    {
        modelIdx = shop.GetSelectedModel();
    }

    public void SetRow()
    {
        selectedModelIdx = shop.GetSelectedModel();
        modelLevel = shop.GetUnlockedLevelFromState(ModelTypes[modelIdx]);
        if (modelIdx == 0)
        {
            modelLevel = 2;
        }
        SetBuyButton();
        SetText();
    }

    void SetBuyButton()
    {
        if (modelIdx == selectedModelIdx)
        {
            BuyButton.interactable = false;
            BuyButtonText.text = "Selected";
            return;
        }

        if (modelLevel == 2)
        {
            BuyButton.interactable = true;
            BuyButtonText.text = "Select";
            return;
        }

        BuyButtonText.text = "$" + shop.UpgradeCost(ModelTypes[modelIdx], 1);
        if (shop.GetCurrentCoins() < shop.UpgradeCost(ModelTypes[modelIdx], 1))
        {
            BuyButton.interactable = false;
        }
        else
        {
            BuyButton.interactable = true;
        }
    }

    void SetText()
    {
        ModelText.text = "Model: " + ModelTypes[modelIdx];
        string descString = "";
        switch (modelIdx)
        {
            case 2:
                descString = "1x acceleration, 3x body";
                break;
            case 1:
                descString = "3x acceleration, 1x body";
                break;
            case 0:
            default:
                descString = "1x acceleration, 1x body";
                break;

        }
        DescText.text = descString;
    }

    void LeftButtonClicked()
    {
        modelIdx = (ModelTypes.Length + modelIdx - 1) % ModelTypes.Length;
        SetMesh(modelIdx);
        SetRow();
    }

    void RightButtonClicked()
    {
        modelIdx = (ModelTypes.Length + modelIdx + 1) % ModelTypes.Length;
        SetMesh(modelIdx);
        SetRow();
    }

    void BuyButtonClicked()
    {
        if (modelLevel == 2)
        {
            shop.SetSelectedModel(modelIdx);
            SetRow();
            return;
        }

        shop.BuyUpgrade(ModelTypes[modelIdx], 2);
        SetRow();
    }

    public void SetMesh(int idx)
    {
        player.GetComponent<MeshFilter>().mesh = meshes[idx].GetComponent<MeshFilter>().sharedMesh;
    }
}
