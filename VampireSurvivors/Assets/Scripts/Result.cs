using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour {
    
    public GameObject[] titles;

    public void Lose() {
        titles[0].SetActive(true);
    }

    public void Victory() {
        titles[1].SetActive(true);
    }
}
