using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {
    [SerializeField] private GameObject player;

    private Checkpoint[] _checkpoints;
    private Vector3 _spawnPosition;

    public Vector3 SpawnPosition {
        get => _spawnPosition;
        set => _spawnPosition = value;
    }

    void Start() {
        _checkpoints = GetComponentsInChildren<Checkpoint>();
        _spawnPosition = player.transform.position;
    }

    public void DeactivateCheckpoints() {
        foreach (var checkpoint in _checkpoints) {
            checkpoint.Deactivate();
        }
    }
}