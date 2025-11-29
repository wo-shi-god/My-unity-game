using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private void Awake()
    {
        instance = this;
    }

    public AudioSource[] soundEffect;
    public void PlaySFX(int sfxToPlay)
    {
        soundEffect[sfxToPlay].Stop();
        soundEffect[sfxToPlay].Play();
    }
    public void PlaySFXitched(int sfxToPlay)
    {
        soundEffect[sfxToPlay].pitch = Random.Range(0.8f, 1.2f);
        PlaySFX(sfxToPlay);
    }
}
