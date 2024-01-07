using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelUp : MonoBehaviour {

    private RectTransform rect;
    private Item[] items;
    private void Awake() {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show() {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
    }

    public void Hide() {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void Select(int index) {
        items[index].OnClick();
    }

    private void Next() {
        // 모든 아이템 비활성화
        foreach (Item item in items) {
            item.gameObject.SetActive(false);
        }
        // 그 중에서 랜덤하게 3개의 아이템만 활성화
        int[] ran = new int[3];
        while (true) {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);
            
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2]) {
                break;
            }
        }

        for (int i = 0; i < ran.Length; i++) {
            Item ranItem = items[ran[i]];
            // 만렙 아이템을 소비아이템으로 대체
            if (ranItem.level == ranItem.data.damageRate.Length) {
                items[4].gameObject.SetActive(true);
            } else {
                ranItem.gameObject.SetActive(true);
            }
            
            
        }
    }
}
