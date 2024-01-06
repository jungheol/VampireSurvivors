using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    public bool isLeft;
    public SpriteRenderer spriteRenderer;

    private SpriteRenderer player;

    private Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    private Vector3 reverseRightPos = new Vector3(-0.15f, -0.15f, 0);
    private Quaternion leftRot = Quaternion.Euler(0,0,-35);
    private Quaternion reverseLeftRot = Quaternion.Euler(0,0,-135);
    private void Awake() {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate() {
        bool isReverse = player.flipX;

        if (isLeft) {
            // 근접 무기
            transform.localRotation = isReverse ? reverseLeftRot : leftRot;
            spriteRenderer.flipY = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 4 : 6;
        } else {
            // 원거리 무기
            transform.localPosition = isReverse ? reverseRightPos : rightPos;
            spriteRenderer.flipX = isReverse;
            spriteRenderer.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
