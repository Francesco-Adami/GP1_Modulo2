using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private int playerHp;
    [SerializeField] private int playerMoney;

    [Header("GameManager Debugging")]
    public bool isGameActive;
    [SerializeField] private HudUI hudUI;

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        instance = this;
    }

    private void Start()
    {
        hudUI = FindAnyObjectByType<HudUI>(FindObjectsInactive.Include);

        hudUI.SetSliderValue(playerHp);
        hudUI.UpdatePlayerHealth(playerHp);
        hudUI.UpdateMoneyText(playerMoney);

    }

    #region PLAYER TAKING DAMAGE
    public void TakeDamage(int damage)
    {
        playerHp -= damage;
        hudUI.UpdatePlayerHealth(playerHp >= 0 ? playerHp : 0);
        if (playerHp <= 0)
        {
            isGameActive = false;
            StartCoroutine(GameOverRoutine());
        }
    }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(1);
        GameOver();
    }

    public void GameOver()
    {
        UIManager.instance.ShowUI(UIManager.GameUI.Lose);
    }

    internal void WinGame()
    {
        UIManager.instance.ShowUI(UIManager.GameUI.Win);
    }
    #endregion

    #region PLAYER MONEY
    public void AddMoney(int amount)
    {
        playerMoney += amount;
        FindAnyObjectByType<HudUI>(FindObjectsInactive.Include).UpdateMoneyText(playerMoney);
    }
    public void RemoveMoney(int amount)
    {
        playerMoney -= amount;
        FindAnyObjectByType<HudUI>(FindObjectsInactive.Include).UpdateMoneyText(playerMoney);
    }
    public int GetMoney()
    {
        return playerMoney;
    }
    #endregion
}
