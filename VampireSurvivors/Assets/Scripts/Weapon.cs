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

	public void LevelUp(float damage, int count) {
		this.damage = damage;
		this.count += count;

		if (id == 0) {
			Batch();
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
			Transform bullet;
			
			// 이미 가지고 있는 weapon이 있다면, 있는 것을 먼저 쓰고, 없다면 pooling에서 생성하기
			if (i < transform.childCount) {
				bullet = transform.GetChild(i);
			} else {
				bullet = GameManager.instance.pool.Get(prefabId).transform;
				bullet.parent = transform;
			}
			
			// 무기 추가 생성 시 위치 초기화
			bullet.localPosition = Vector3.zero;
			bullet.localRotation = Quaternion.identity;

			Vector3 rotVec = Vector3.forward * 360 * i / count;
			bullet.Rotate(rotVec);
			bullet.Translate(bullet.up * 1.3f, Space.World);
			bullet.GetComponent<Bullet>().Init(damage, -1); // -1 은 무한관통.
		}
	}
}

