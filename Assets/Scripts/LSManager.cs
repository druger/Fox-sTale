using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour {
    [SerializeField] private LSUIController uiController;

    public void LoadLevel(int level) {
        StartCoroutine(LoadLevelCoroutine(level));
    }

    private IEnumerator LoadLevelCoroutine(int level) {
        uiController.FadeToBlack();
        yield return new WaitForSeconds(1f / uiController.FadeSpeed + .25f);
        SceneManager.LoadScene(level);
    }
}