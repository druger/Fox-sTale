using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour {
    [SerializeField] private MapPoint up;
    [SerializeField] private MapPoint down;
    [SerializeField] private MapPoint left;
    [SerializeField] private MapPoint right;
    
    [SerializeField] private bool isLevel;
    [SerializeField] private int levelToLoad;

    public MapPoint Up => up;

    public MapPoint Down => down;

    public MapPoint Left => left;

    public MapPoint Right => right;

    public bool IsLevel => isLevel;
    public int LevelToLoad => levelToLoad;
}