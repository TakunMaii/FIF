using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Timeline.Actions;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Ins;
    private AudioSource bgm;
    private void Awake()
    {
        Ins = this;
    }

    public void Init()
    {
        bgm = gameObject.AddComponent<AudioSource>();
    }

    public void PlayBGM(string name, float volume = 0.3f, bool isLoop = true)
    {
        AudioClip clip = Resources.Load<AudioClip>("BGM/" + name);
        bgm.clip = clip;
        bgm.loop = isLoop;
        bgm.volume = volume;
        bgm.Play();
    }

    public void PlaySounds(string name, Vector3 position)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + name);
        AudioSource.PlayClipAtPoint(clip, position);
    }
}
