using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour {
    private const int MAXHealth = 6;

    private SpriteRenderer _spriteRenderer;
    private PlayerController _playerController;

    private int currentHealth;
    private float _invinsibleCounter;
    private float _invinsibleLength = 1;

    [SerializeField] private UIController uiController;

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerController = GetComponent<PlayerController>();
        currentHealth = MAXHealth;
    }

    private void Update() {
        if (_invinsibleCounter > 0) {
            _invinsibleCounter -= Time.deltaTime;
            if (_invinsibleCounter <= 0) ChangePlayerTransparency(1f);
        }
    }

    public void DealDamage() {
        if (_invinsibleCounter <= 0) {
            currentHealth--;
            if (currentHealth <= 0) {
                Destroy(gameObject);
            } else {
                _invinsibleCounter = _invinsibleLength;
                ChangePlayerTransparency(.5f);
                _playerController.KnockBack();
            }

            uiController.UpdateHealth(currentHealth);
        }
    }

    private void ChangePlayerTransparency(float alfa) {
        var color = _spriteRenderer.color;
        color = new Color(color.r, color.g, color.b, alfa);
        _spriteRenderer.color = color;
    }
}