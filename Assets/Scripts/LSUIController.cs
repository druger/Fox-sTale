using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIController : MonoBehaviour {
    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed;

    private bool _shouldFadeToBlack;
    private bool _shouldFadeFromBlack;
    
    public float FadeSpeed => fadeSpeed;

    private void Start() {
        FadeFromBlack();
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
}