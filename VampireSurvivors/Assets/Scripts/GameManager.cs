using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    [Header("Game Object")]
    public PoolManager pool;
    public Player player;
    [Header("Game Control")]
    public float gameTime;
    public float maxGameTime = 20f;
    [Header("Player Info")] 
    public int health;
    public int maxHealth = 100;
    public int level;
    public int killPoint;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 300, 450 };

    private void Awake() {
        instance = this;
    }

    private void Start() {
        health = maxHealth;
    }

    private void Update() {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime) {
            gameTime = maxGameTime;
        }
    }

    public void GetExp() {
        exp++;

        if (exp == nextExp[level]) {
            level++;
            exp = 0;
            
        }
    }
}
