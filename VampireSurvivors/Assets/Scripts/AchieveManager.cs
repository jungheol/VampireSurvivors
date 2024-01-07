using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour {

    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject noticeUI;

    enum Achieve {
        Unlock0, 
        Unlock1
    }
    private Achieve[] achives;
    private WaitForSecondsRealtime wait;

    private void Awake() {
        achives = (Achieve[])Enum.GetValues(typeof(Achieve));
        wait = new WaitForSecondsRealtime(5);
        
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

            for (int i = 0; i < noticeUI.transform.childCount; i++) {
                bool isActive = i == (int)achieve;
                noticeUI.transform.GetChild(i).gameObject.SetActive(isActive);
            }
            
            StartCoroutine(NoticeRoutine());
        }
    }

    IEnumerator NoticeRoutine() {
        noticeUI.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);

        yield return wait;
        
        noticeUI.SetActive(false);
    }
}
