using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitBox : MonoBehaviour {
    [SerializeField] private BossTankController tankController;
    [SerializeField] private PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && playerController.transform.position.y > transform.position.y) {
            tankController.TakeHit();
            playerController.Bounce();
            gameObject.SetActive(false);
        }
    }
}