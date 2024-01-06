using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour {
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

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

    public void OnClick() {
        switch (data.itemType) {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (level == 0) {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                } else {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damageRate[level];
                    nextCount += data.counts[level];
                    
                    weapon.LevelUp(nextDamage, nextCount);
                }
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (level == 0) {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                } else {
                    float nextRate = data.damageRate[level];
                    gear.LevelUp(nextRate);
                }
                break;
            case ItemData.ItemType.Heal:
                break;
        }

        level++;

        if (level == data.damageRate.Length) {
            GetComponent<Button>().interactable = false;
        }
    }
}
