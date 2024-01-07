using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour {

    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;

    enum Achieve {
        Unlock0, 
        Unlock1
    }
    private Achieve[] achives;

    private void Awake() {
        achives = (Achieve[])Enum.GetValues(typeof(Achieve));
        
        if (!PlayerPrefs.HasKey("MyData")) Init();
    }

    private void Init() {
        PlayerPrefs.SetInt("MyData", 1);
       
        foreach (Achieve achive in achives) {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    void Start() {
        UnlockCharacter();
    }

    private void UnlockCharacter() {
        for (int i = 0; i < lockCharacter.Length; i++) {
            string achiveName = achives[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[i].SetActive(!isUnlock);
            unlockCharacter[i].SetActive(isUnlock);
        }
    }

    private void LateUpdate() {
        foreach (Achieve achieve in achives) {
            CheckAchieve(achieve);
        }
    }

    private void CheckAchieve(Achieve achieve) {
        bool isAchieve = false;

        switch (achieve) {
            case Achieve.Unlock0:
                // 몬스터 킬 수
                isAchieve = GameManager.instance.killPoint >= 10;
                break;
            case Achieve.Unlock1:
                // 생존 성공
                isAchieve = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }

        if (isAchieve && PlayerPrefs.GetInt(achieve.ToString()) == 0) {
            PlayerPrefs.SetInt(achieve.ToString(), 1);
        }
    }
}
