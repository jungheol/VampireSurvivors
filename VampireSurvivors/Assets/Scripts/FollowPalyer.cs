using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPalyer : MonoBehaviour {
    private RectTransform rect;

    private void Awake() {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate() {
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
