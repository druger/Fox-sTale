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
    [SerializeField] private Text gemTxt;

    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed;

    private bool _shouldFadeToBlack;
    private bool _shouldFadeFromBlack;

    private PlayerHealthController _playerHealthController;
    private int _currentHeartIndex;

    public float FadeSpeed => fadeSpeed;

    private void Start() {
        _playerHealthController = player.GetComponent<PlayerHealthController>();
        SetupCurrentHeartIndex();
        UpdateGemCount(0);
    }

    private void Update() {
        if (_shouldFadeToBlack) {
            var oldColor = fadeScreen.color;
            fadeScreen.color = new Color(
                oldColor.r, oldColor.g, oldColor.b,
                Mathf.MoveTowards(oldColor.a, 1f, fadeSpeed * Time.deltaTime)
            );
            if (fadeScreen.color.a == 1f) {
                _shouldFadeToBlack = false;
            }
        }

        if (_shouldFadeFromBlack) {
            var oldColor = fadeScreen.color;
            fadeScreen.color = new Color(
                oldColor.r, oldColor.g, oldColor.b,
                Mathf.MoveTowards(oldColor.a, 0f, fadeSpeed * Time.deltaTime)
            );
            if (fadeScreen.color.a == 0f) {
                _shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack() {
        _shouldFadeToBlack = true;
        _shouldFadeFromBlack = false;
    }

    public void FadeFromBlack() {
        _shouldFadeFromBlack = true;
        _shouldFadeToBlack = false;
    }

    public void ReduceHealth() {
        if (_playerHealthController.CurrentHealth % 2 == 0) {
            SetEmptyHeart();
            _currentHeartIndex--;
        } else {
            SetHalfHeart();
        }

        if (_currentHeartIndex < 0) SetupCurrentHeartIndex();
    }

    public void IncreaseHealth() {
        if (_playerHealthController.CurrentHealth % 2 == 0) {
            SetFullHeart();
        } else {
            _currentHeartIndex++;
            SetHalfHeart();
        }

        if (_currentHeartIndex > hearts.Count - 1) SetupCurrentHeartIndex();
    }

    public void RespawnHealth() {
        foreach (var heart in hearts) {
            heart.sprite = heartFull;
        }
    }

    public void UpdateGemCount(int count) {
        gemTxt.text = count.ToString();
    }

    private void SetHalfHeart() {
        hearts[_currentHeartIndex].sprite = heartHalf;
    }

    private void SetEmptyHeart() {
        hearts[_currentHeartIndex].sprite = heartEmpty;
    }

    private void SetFullHeart() {
        hearts[_currentHeartIndex].sprite = heartFull;
    }

    private void SetupCurrentHeartIndex() {
        _currentHeartIndex = hearts.Count - 1;
    }
}