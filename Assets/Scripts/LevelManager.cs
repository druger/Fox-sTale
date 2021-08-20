using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    [SerializeField] private UIController uiController;
    [SerializeField] private GameObject player;
    [SerializeField] private CheckpointController _checkpointController;
    [SerializeField] private float waitToRespawn = 1f;

    private PlayerHealthController _playerHealthController;

    void Start() {
        _playerHealthController = player.GetComponent<PlayerHealthController>();
    }

    public void RespawnPlayer() {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn() {
        // TODO reinstance player object instead
        player.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        player.SetActive(true);
        player.transform.position = _checkpointController.SpawnPosition;
        _playerHealthController.CurrentHealth = _playerHealthController.MAXHealth;
        uiController.UpdateHealth();
    }
}