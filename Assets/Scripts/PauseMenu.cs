using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private GameObject pauseMenu;

    private bool _isPaused;

    public bool IsPaused => _isPaused;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) PauseUnpause();
    }

    public void ResumeGame() {
        PauseUnpause();
    }

    public void LevelSelect() {
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
        SetTimeScale(1);
    }

    private void PauseUnpause() {
        if (_isPaused) {
            SetTimeScale(1);
            _isPaused = false;
            pauseMenu.SetActive(false);
        } else {
            SetTimeScale(0);
            _isPaused = true;
            pauseMenu.SetActive(true);
        }
    }

    private void SetTimeScale(float time) {
        Time.timeScale = time;
    }
    
}