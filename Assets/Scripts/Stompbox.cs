using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stompbox : MonoBehaviour {
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject deathEffect;

    private PlayerController _playerController;

    private void Start() {
        _playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            var transformOther = other.transform;
            transformOther.parent.gameObject.SetActive(false);
            Instantiate(deathEffect, transformOther.position, transformOther.rotation);
            _playerController.Bounce();
            audioManager.PlaySFX(3);
        }
    }
}