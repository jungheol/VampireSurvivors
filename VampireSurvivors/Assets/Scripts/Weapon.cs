using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public int id;
	public int prefabId;
	public float damage;
	public int count;
	public float speed;

	private void Start() {
		Init();
	}

	private void Update() {
		switch (id) {
			case 0:
				transform.Rotate(Vector3.back * speed * Time.deltaTime);
				break;
			case 1:
				break;
			default:
				break;
		}
	}

	public void Init() {
		switch (id) {
			case 0:
				speed = 150;
				Batch();		
				break;
			case 1:
				break;
			default:
				break;
		}
	}

	private void Batch() {
		for (int i = 0; i < count; i++) {
			Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
			bullet.parent = transform;

			Vector3 rotVec = Vector3.forward * 360 * i / count;
			bullet.Rotate(rotVec);
			bullet.Translate(bullet.up * 1.3f, Space.World);
			bullet.GetComponent<Bullet>().Init(damage, -1); // -1 은 무한관통.
		}
	}
}

