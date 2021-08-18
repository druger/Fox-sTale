using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [SerializeField] private List<Image> hearts;

    [SerializeField] private Sprite heartEmpty;
    [SerializeField] private Sprite heartHalf;

    private int _currentHeartIndex;

    private void Start() {
        _currentHeartIndex = hearts.Count - 1;
    }

    public void UpdateHealth(int health) {
        if (health % 2 == 0) {
            hearts[_currentHeartIndex].sprite = heartEmpty;
            _currentHeartIndex--;
        } else {
            hearts[_currentHeartIndex].sprite = heartHalf;
        }
    }
}