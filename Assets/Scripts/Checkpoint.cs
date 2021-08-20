using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;

    private SpriteRenderer _renderer;
    private CheckpointController _checkpointController;

    void Start() {
        _renderer = GetComponent<SpriteRenderer>();
        _checkpointController = GetComponentInParent<CheckpointController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _checkpointController.DeactivateCheckpoints();
            _renderer.sprite = on;
            _checkpointController.SpawnPosition = transform.position;
        }
    }

    public void Deactivate() {
        _renderer.sprite = off;
    }
}