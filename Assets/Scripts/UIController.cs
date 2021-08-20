using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private List<Image> hearts;

    [SerializeField] private Sprite heartEmpty;
    [SerializeField] private Sprite heartHalf;
    [SerializeField] private Sprite heartFull;

    private PlayerHealthController _playerHealthController;
    private int _currentHeartIndex;

    private void Start() {
        _playerHealthController = player.GetComponent<PlayerHealthController>();
        SetupCurrentHeartIndex();
    }

    public void UpdateHealth() {
        if (_playerHealthController.CurrentHealth == _playerHealthController.MAXHealth) FillAllHearts();
        else ReduceHealth();
    }

    private void ReduceHealth() {
        if (_playerHealthController.CurrentHealth % 2 == 0) {
            hearts[_currentHeartIndex].sprite = heartEmpty;
            _currentHeartIndex--;
        } else {
            hearts[_currentHeartIndex].sprite = heartHalf;
        }

        if (_currentHeartIndex < 0) SetupCurrentHeartIndex();
    }

    private void FillAllHearts() {
        foreach (var heart in hearts) {
            heart.sprite = heartFull;
        }
    }

    private void SetupCurrentHeartIndex() {
        _currentHeartIndex = hearts.Count - 1;
    }
}