using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------- Audio Source --------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("-------- Audio Clip - Enviroment --------")]
    public AudioClip background;

    [Header("-------- Audio Clip - Player --------")]
    public AudioClip walk;
    public AudioClip run;
    public AudioClip jump;
    public AudioClip attack;
    public AudioClip death;

    [Header("-------- Audio Clip - UI --------")]
    public AudioClip clickButtom;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip)
        {
            SFXSource.PlayOneShot(clip);
        }
    }

}
