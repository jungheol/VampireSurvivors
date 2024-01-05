using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {

	public Transform[] spawnPoint;

	private float timer;

	private void Awake() {
		spawnPoint = GetComponentsInChildren<Transform>();
	}

	private void Update() {
		timer += Time.deltaTime;

		if (timer > 0.5f) {
			Spawn();
			timer = 0;
		}
	}

	private void Spawn() {
		GameObject Monster = GameManager.instance.pool.Get(Random.Range(0, 2));
		Monster.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
	}
}
