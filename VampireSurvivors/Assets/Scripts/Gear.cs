using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gear : MonoBehaviour {
    public ItemData.ItemType itemType;
    public float rate;

    public void Init(ItemData data) {
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        itemType = data.itemType;
        rate = data.damageRate[0];
        ApplyGear();
    }

    public void LevelUp(float rate) {
        this.rate = rate;
        ApplyGear();
    }

    private void ApplyGear() {
        switch (itemType) {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    private void RateUp() {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons) {
            switch (weapon.id) {
                case 0:
                    float speed = 150 * Character.WeaponSpeed;
                    weapon.speed = speed + (speed * rate);
                    break;
                default:
                    speed = 0.5f * Character.WeaponRate;
                    weapon.speed = speed * (1f - rate);
                    break;
            }
        }
    }

    private void SpeedUp() {
        float speed = 3 * Character.Speed;
        GameManager.instance.player.moveSpeed = speed + speed * rate;
    }
}
