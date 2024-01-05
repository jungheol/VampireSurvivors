using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public PoolManager pool;
    public Player player;

    public float gameTime;
    public float maxGameTime = 20f;

    private void Awake() {
        instance = this;
    }

    private void Update() {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime) {
            gameTime = maxGameTime;
        }
    }
}
