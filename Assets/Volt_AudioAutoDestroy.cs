using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volt_AudioAutoDestroy : MonoBehaviour
{
    AudioSource audio;
    float timer;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    void LateUpdate()
    {
        if (timer < 1f)
        {
            timer += Time.deltaTime;
            return;
        }

        if (!audio.isPlaying)
        {
            Volt_SoundManager.S.sounds.Remove(audio);
            Managers.Resource.Release<AudioClip>(audio.clip);
            Destroy(gameObject);
        }
    }
}
