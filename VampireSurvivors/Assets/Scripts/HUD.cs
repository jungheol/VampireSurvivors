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
                break;
            case InfoType.Level:
                break;
            case InfoType.KillPoint:
                break;
            case InfoType.Time:
                break;
            case InfoType.Health:
                break;
        }
    }
}
