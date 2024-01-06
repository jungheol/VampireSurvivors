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
    private Collider2D coll;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private WaitForFixedUpdate wait;
    
    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate() {
        if(!GameManager.instance.isLive) return;
        
        if(!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
            
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate() {
        if(!GameManager.instance.isLive) return;
        
        if(!isLive) return;
        
        spriteRenderer.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable() {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
        coll.enabled = true;
        rigid.simulated = true;
        spriteRenderer.sortingOrder = 2;
        anim.SetBool("Dead", false);
    }

    public void Init(SpawnData data) {
        anim.runtimeAnimatorController = animControllers[data.mosterType];
        moveSpeed = data.monsterSpeed;
        maxHealth = data.monsterHealth;
        health = data.monsterHealth;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Bullet") || !isLive)
            return;

        health -= other.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());
        
        if (health > 0) {
            anim.SetTrigger("Hit");            
        } else {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriteRenderer.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.killPoint++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack() {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    private void Dead() {
        gameObject.SetActive(false);
    }
}
