using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {

	public Transform[] spawnPoint;
	public SpawnData[] spawnDatas;
	private int level;
	private float timer;

	private void Awake() {
		spawnPoint = GetComponentsInChildren<Transform>();
	}

	private void Update() {
		if(!GameManager.instance.isLive) return;
		
		timer += Time.deltaTime;
		level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnDatas.Length - 1);
		
		if (timer > spawnDatas[level].spawnTime) {
			Spawn();
			timer = 0;
		}
	}

	private void Spawn() {
		GameObject monster = GameManager.instance.pool.Get(0);
		monster.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
		monster.GetComponent<Enemy>().Init(spawnDatas[level]);
	}
}

[Serializable]
public class SpawnData {
	public float spawnTime;
	public int mosterType;
	public int monsterHealth;
	public float monsterSpeed;
}
