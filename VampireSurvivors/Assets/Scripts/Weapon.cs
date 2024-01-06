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

	private float timer;
	private Player player;

	private void Awake() {
		player = GameManager.instance.player;
	}

	private void Update() {
		switch (id) {
			case 0:
				transform.Rotate(Vector3.back * speed * Time.deltaTime);
				break;
			case 1:
				timer += Time.deltaTime;

				if (timer > speed) {
					timer = 0f;
					Fire();
				}
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

	public void Init(ItemData data) {
		// 기본 셋팅
		name = "Weapon " + data.itemId;
		transform.parent = player.transform;
		transform.localPosition = Vector3.zero;
		
		// 각종 값 세팅
		id = data.itemId;
		damage = data.baseDamage;
		count = data.baseCount;

		for (int i = 0; i < GameManager.instance.pool.prefabs.Length; i++) {
			if (data.projectile == GameManager.instance.pool.prefabs[i]) {
				prefabId = i;
				break;
			}
		}
		
		switch (id) {
			case 0:
				speed = 150;
				Batch();		
				break;
			case 1:
				speed = 0.3f;
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
			bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 은 무한관통.
		}
	}

	private void Fire() {
		if (!player.scanner.nearestTarget)
			return;

		Vector3 targetPos = player.scanner.nearestTarget.position;
		Vector3 dir = targetPos - transform.position;
		dir = dir.normalized;
		
		Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
		bullet.position = transform.position;
		bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
		bullet.GetComponent<Bullet>().Init(damage, count, dir);
	}
}

