using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour {
    [SerializeField] private LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            levelManager.EndLevel();
        }
    }
}