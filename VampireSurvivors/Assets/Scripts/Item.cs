using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour {
    public ItemData data;
    public int level;
    public Weapon weapon;

    private Image icon;
    private Text textLevel;

    private void Awake() {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void LateUpdate() {
        textLevel.text = "Lv." + (level + 1);
    }
}
