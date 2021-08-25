using System;
using UnityEngine;

public class Pickup : MonoBehaviour {
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UIController uiController;
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private bool isGem;
    [SerializeField] private bool isHeal;

    private PlayerHealthController _playerHealthController;
    private bool _isCollected;

    private void Start() {
        _playerHealthController = levelManager.PlayerHealthController;
        uiController = levelManager.UIController;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !_isCollected) {
            if (isGem) {
                levelManager.GemsCollected++;
                uiController.UpdateGemCount(levelManager.GemsCollected);
                _isCollected = true;
                Destroy(gameObject);
                CreatePickupEffect();
                audioManager.PlaySFX(6);
            }

            if (isHeal) {
                if (_playerHealthController.CurrentHealth != _playerHealthController.maxHealth) {
                    _playerHealthController.HealPlayer();
                    _isCollected = true;
                    Destroy(gameObject);
                    CreatePickupEffect();
                    audioManager.PlaySFX(7);
                }
            }
        }
    }

    private void CreatePickupEffect() {
        Instantiate(pickupEffect, transform.position, transform.rotation);
    }
}