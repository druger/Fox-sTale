using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMine : MonoBehaviour {
    [SerializeField] private GameObject explosion;

    private PlayerHealthController _playerHealthController;

    private void Start() {
        _playerHealthController = FindObjectOfType<PlayerHealthController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _playerHealthController.DealDamage();
            Explode();
        }
    }

    public void Explode() {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
    }
}