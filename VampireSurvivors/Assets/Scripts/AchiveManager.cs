using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour {

    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;

    enum Achive {
        Unlock0, 
        Unlock1
    }
    private Achive[] achives;

    private void Awake() {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        
        if (!PlayerPrefs.HasKey("MyData")) Init();
    }

    private void Init() {
        PlayerPrefs.SetInt("MyData", 1);
       
        foreach (Achive achive in achives) {
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
}
