using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameStateScriptableObject gameState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPressed()
    {
        EventManager.TriggerEvent("NewGame");
        gameState.isPaused = false;
    }

    public void ShopPressed()
    {
        EventManager.TriggerEvent("ShopPressed");
    }
}
