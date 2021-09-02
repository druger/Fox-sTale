using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO вынести использование аудио по соответствующим скриптам
public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource[] soundEffects;
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource levelEndMusic;

    public void PlaySFX(int soundToPlay) {
        var sound = soundEffects[soundToPlay];
        sound.Stop();
        sound.pitch = Random.Range(.9f, 1.1f);
        sound.Play();
    }

    public void PlayLevelEndMusic() {
        bgMusic.Stop();
        levelEndMusic.Play();
    }
}