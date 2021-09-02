using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private UIController uiController;
    [SerializeField] private GameObject player;
    [SerializeField] private CheckpointController checkpointController;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private float waitToRespawn = 1f;

    private PlayerHealthController _playerHealthController;
    private PlayerController _playerController;

    private int _gemsCollected;

    public int GemsCollected {
        get => _gemsCollected;
        set => _gemsCollected = value;
    }

    public PlayerHealthController PlayerHealthController => _playerHealthController;

    public UIController UIController => uiController;

    private void Awake() {
        _playerHealthController = player.GetComponent<PlayerHealthController>();
        _playerController = player.GetComponent<PlayerController>();
    }

    public void RespawnPlayer() {
        StartCoroutine(Respawn());
    }

    public void EndLevel() {
        StartCoroutine(EndLevelCoroutine());
    }

    private IEnumerator EndLevelCoroutine() {
        audioManager.PlayLevelEndMusic();
        _playerController.StopInput = true;
        cameraController.StopFollow = true;
        yield return new WaitForSeconds(1.5f);
        uiController.FadeToBlack();
        yield return new WaitForSeconds(1f / (uiController.FadeSpeed + 3f));
        OpenLevelSelect();
    }

    private void OpenLevelSelect() {
        var scene = SceneManager.GetActiveScene();
        PlayerPrefs.SetInt(scene.name + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel", scene.name);
        SceneManager.LoadScene(1);
    }

    private IEnumerator Respawn() {
        // TODO reinstance player object instead
        player.SetActive(false);
        audioManager.PlaySFX(8);
        
        yield return new WaitForSeconds(waitToRespawn - 1f / uiController.FadeSpeed);
        uiController.FadeToBlack();
        yield return new WaitForSeconds(1f / uiController.FadeSpeed + .2f);
        uiController.FadeFromBlack();

        player.SetActive(true);
        player.transform.position = checkpointController.SpawnPosition;
        _playerHealthController.CurrentHealth = _playerHealthController.maxHealth;
        uiController.RespawnHealth();
    }
}