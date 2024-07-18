using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public void PlayShootingClip(AudioClip shootingClip, float shootingVolume){
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayBlastClip(AudioClip hitClip, float hitVolume){
        PlayClip(hitClip, hitVolume);
    }

    public void PlayClip(AudioClip clip, float volume){
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }
}
