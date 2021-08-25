using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private UIController uiController;
    [SerializeField] private GameObject player;
    [SerializeField] private CheckpointController _checkpointController;
    [SerializeField] private float waitToRespawn = 1f;

    private PlayerHealthController _playerHealthController;

    private int _gemsCollected;

    public int GemsCollected {
        get => _gemsCollected;
        set => _gemsCollected = value;
    }

    public PlayerHealthController PlayerHealthController => _playerHealthController;

    public UIController UIController => uiController;

    private void Awake() {
        _playerHealthController = player.GetComponent<PlayerHealthController>();
    }

    public void RespawnPlayer() {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn() {
        // TODO reinstance player object instead
        player.SetActive(false);
        audioManager.PlaySFX(8);
        yield return new WaitForSeconds(waitToRespawn);
        player.SetActive(true);
        player.transform.position = _checkpointController.SpawnPosition;
        _playerHealthController.CurrentHealth = _playerHealthController.maxHealth;
        uiController.RespawnHealth();
    }
}