using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour {
    [SerializeField] private LSUIController uiController;
    [SerializeField] private LevelSelectPlayer player;
    [SerializeField] private AudioManager audioManager;

    private MapPoint[] _mapPoints;

    private void Start() {
        _mapPoints = FindObjectsOfType<MapPoint>();
        CheckCurrentLevel();
    }

    private void CheckCurrentLevel() {
        if (PlayerPrefs.HasKey("CurrentLevel")) {
            foreach (var point in _mapPoints) {
                if (point.LevelToLoad == PlayerPrefs.GetString("CurrentLevel")) {
                    player.transform.position = point.transform.position;
                    player.CurrentPoint = point;
                }
            }
        }
    }

    public void LoadLevel(String level) {
        StartCoroutine(LoadLevelCoroutine(level));
    }

    private IEnumerator LoadLevelCoroutine(String level) {
        audioManager.PlaySFX(4);
        uiController.FadeToBlack();
        yield return new WaitForSeconds(1f / uiController.FadeSpeed + .25f);
        SceneManager.LoadScene(level);
    }
}