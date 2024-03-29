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
    public Result resultUI;
    public GameObject enemyCleaner;
    public Transform joyUI;
    
    [Header("Game Control")]
    public float gameTime;
    public float maxGameTime = 20f;
    public bool isLive;
    
    [Header("Player Info")] 
    public int playerId;
    public float health;
    public float maxHealth = 100;
    public int level;
    public int killPoint;
    public int exp;
    public int[] nextExp = { 3, 10, 30, 100, 150, 210, 280, 360, 450, 600 };

    private void Awake() {
        instance = this;
        Application.targetFrameRate = 60;
    }

    public void GameStart(int id) {
        playerId = id;
        health = maxHealth;
        
        player.gameObject.SetActive(true);
        levelUpUI.Select(playerId % 2);
        Resume();
        
        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AdsManager.instance.LoadAd();
    }

    public void GameOver() {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine() {
        isLive = false;
        
        yield return new WaitForSeconds(0.5f);
        
        resultUI.gameObject.SetActive(true);
        resultUI.Lose();
        Stop();
        
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }
    
    public void GameVictory() {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine() {
        isLive = false;
        enemyCleaner.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);
        
        resultUI.gameObject.SetActive(true);
        resultUI.Victory();
        Stop();
        
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }
    
    public void GameRetry() {
        SceneManager.LoadScene("MainScene");
    }
    
    public void GameQuit() {
        Application.Quit();
    }

    private void Update() {
        if(!isLive) return;
        
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime) {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp() {
        if (!isLive) return;
        
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
        joyUI.localScale = Vector3.zero;
    }

    public void Resume() {
        isLive = true;
        Time.timeScale = 1;
        joyUI.localScale = Vector3.one;
    }
}
