﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameOverlay : MonoBehaviour
{
    public GameStateScriptableObject gameState;
    public Slider gasSlider;
    public Text score;
    public Text coins;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gasSlider.value = gameState.timeLeft / gameState.maxTime;
        score.text = string.Format("Score: {0}", (int) (player.transform.position.x * 100));
        coins.text = string.Format("{0}", gameState.coinCount);
    }
}
