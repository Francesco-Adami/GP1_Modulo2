using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private int playerHp;

    [Header("GameManager Debugging")]
    public bool isGameActive;

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        instance = this;
    }

    #region PLAYER TAKING DAMAGE
    public void TakeDamage(int damage)
    {
        playerHp -= damage;
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
    #endregion

}
