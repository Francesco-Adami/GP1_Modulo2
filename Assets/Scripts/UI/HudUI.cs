using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Slider playerHealth;

    private void OnEnable()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.isGameActive = true;
        }
    }

    private void OnDisable()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.isGameActive = false;
        }
    }

    internal void UpdateMoneyText(int playerMoney)
    {
        moneyText.text = "$ " + playerMoney.ToString();
    }

    internal void SetSliderValue(int maxHealth)
    {
        playerHealth.maxValue = maxHealth;
    }

    internal void UpdatePlayerHealth(int playerHealth)
    {
        this.playerHealth.value = playerHealth;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            UIManager.instance.ShowUI(UIManager.GameUI.Pause);
        }
    }
}
