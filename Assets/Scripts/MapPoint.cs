using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour {
    [SerializeField] private MapPoint up;
    [SerializeField] private MapPoint down;
    [SerializeField] private MapPoint left;
    [SerializeField] private MapPoint right;
    
    [SerializeField] private bool isLevel;
    [SerializeField] private bool isLocked;
    [SerializeField] private String levelToLoad;
    [SerializeField] private String levelToCheck;

    private void Start() {
        if (isLevel && levelToLoad != null && levelToCheck != null) {
            isLocked = true;
            if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1) {
                isLocked = false;
            }
        }

        if (levelToLoad == levelToCheck) isLocked = false;
    }

    public MapPoint Up => up;

    public MapPoint Down => down;

    public MapPoint Left => left;

    public MapPoint Right => right;

    public bool IsLevel => isLevel;
    
    public bool IsLocked => isLocked;

    public String LevelToLoad => levelToLoad;
}