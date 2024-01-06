using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    [Header("Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp levelUpUI;
    public GameObject resultUI;
    [Header("Game Control")]
    public float gameTime;
    public float maxGameTime = 20f;
    public bool isLive;
    [Header("Player Info")] 
    public float health;
    public float maxHealth = 100;
    public int level;
    public int killPoint;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 300, 450 };

    private void Awake() {
        instance = this;
    }

    public void GameStart() {
        health = maxHealth;
        levelUpUI.Select(0);
        Resume();
    }

    public void GameOver() {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine() {
        isLive = false;
        
        yield return new WaitForSeconds(0.5f);
        
        resultUI.SetActive(true);
        Stop();
    }
    
    public void GameRetry() {
        SceneManager.LoadScene("MainScene");
    }

    private void Update() {
        if(!isLive) return;
        
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime) {
            gameTime = maxGameTime;
        }
    }

    public void GetExp() {
        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)]) {
            level++;
            exp = 0;
            levelUpUI.Show();
        }
    }

    public void Stop() {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume() {
        isLive = true;
        Time.timeScale = 1;
    }
}
