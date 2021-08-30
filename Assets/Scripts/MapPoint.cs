using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour {
    [SerializeField] private MapPoint up;
    [SerializeField] private MapPoint down;
    [SerializeField] private MapPoint left;
    [SerializeField] private MapPoint right;
    
    [SerializeField] private bool isLevel;
}