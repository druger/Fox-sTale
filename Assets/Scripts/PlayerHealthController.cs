using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour {
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private UIController uiController;

    public int MAXHealth = 6;

    private SpriteRenderer _spriteRenderer;
    private PlayerController _playerController;

    private int _currentHealth;
    private float _invinsibleCounter;
    private float _invinsibleLength = 1;
    
    public int CurrentHealth {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerController = GetComponent<PlayerController>();
        _currentHealth = MAXHealth;
    }

    private void Update() {
        if (_invinsibleCounter > 0) {
            _invinsibleCounter -= Time.deltaTime;
            if (_invinsibleCounter <= 0) ChangePlayerTransparency(1f);
        }
    }

    public void DealDamage() {
        if (_invinsibleCounter <= 0) {
            _currentHealth--;
            if (_currentHealth <= 0) {
                _currentHealth = 0;
                _levelManager.RespawnPlayer();
            } else {
                _invinsibleCounter = _invinsibleLength;
                ChangePlayerTransparency(.5f);
                _playerController.KnockBack();
            }

            uiController.UpdateHealth();
        }
    }

    private void ChangePlayerTransparency(float alfa) {
        var color = _spriteRenderer.color;
        color = new Color(color.r, color.g, color.b, alfa);
        _spriteRenderer.color = color;
    }
}