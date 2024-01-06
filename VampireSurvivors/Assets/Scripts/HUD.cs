using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour {
    public enum InfoType {
        Exp,
        Level,
        KillPoint,
        Time,
        Health
    }

    public InfoType infoType;
    private Text myText;
    private Slider mySlider;

    private void Awake() {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate() {
        switch (infoType) {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                myText.text = "Lv." + GameManager.instance.level;
                break;
            case InfoType.KillPoint:
                myText.text = GameManager.instance.killPoint.ToString();
                break;
            case InfoType.Time:
                break;
            case InfoType.Health:
                break;
        }
    }
}
