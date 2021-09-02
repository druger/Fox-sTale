using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField] private GameObject continueBtn;

    private void Start() {
        if (PlayerPrefs.HasKey("Level1_unlocked")) {
            continueBtn.SetActive(true);
        } else {
            continueBtn.SetActive(false);
        }
    }

    public void StartGame() {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(2);
    }

    public void ContinueGame() {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {
        Application.Quit();
    }
}