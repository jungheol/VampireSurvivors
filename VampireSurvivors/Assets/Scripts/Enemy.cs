using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float moveSpeed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animControllers;
    private bool isLive;

    private Rigidbody2D target;
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    
    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if(!isLive) return;
            
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate() {
        if(!isLive) return;
        
        spriteRenderer.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable() {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data) {
        anim.runtimeAnimatorController = animControllers[data.mosterType];
        moveSpeed = data.monsterSpeed;
        maxHealth = data.monsterHealth;
        health = data.monsterHealth;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Bullet"))
            return;

        health -= other.GetComponent<Bullet>().damage;

        if (health > 0) {
            
        } else {
            Dead();        
        }
    }

    private void Dead() {
        gameObject.SetActive(false);
    }
}
