using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {

	public Transform[] spawnPoint;

	private int level;
	private float timer;

	private void Awake() {
		spawnPoint = GetComponentsInChildren<Transform>();
	}

	private void Update() {
		timer += Time.deltaTime;
		level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);
		
		if (timer > (level == 0 ? 0.5f : 0.2f)) {
			Spawn();
			timer = 0;
		}
	}

	private void Spawn() {
		GameObject Monster = GameManager.instance.pool.Get(level);
		Monster.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
	}
}
