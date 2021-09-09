using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {
    [SerializeField] private PlayerHealthController playerHealthController;
    [SerializeField] private float speed;

    void Update() {
        transform.position = new Vector3(-speed * transform.localScale.x * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerHealthController.DealDamage();
        }
        Destroy(gameObject);
    }
}