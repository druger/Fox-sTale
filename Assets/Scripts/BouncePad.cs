using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour {
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float bounceForce = 25f;

    private Animator _animator;

    void Start() {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _animator.SetTrigger("bounce");
            playerController.Rb.velocity = new Vector2(playerController.Rb.velocity.x, bounceForce);
        }
    }
}