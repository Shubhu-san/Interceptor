using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 20;
    [SerializeField] ParticleSystem blastEffect;
    CameraShake cameraShake;

    [Header("Camera Shake")]
    [SerializeField] bool shakeCam;
    
    [Header("Death Camera")]
    [SerializeField] float deathCamShakeMagnitude = 0.5f;
    [SerializeField] float deathCamShakeDuration = 1f;

    [Header("Hit Camera")]
    [SerializeField] float hitCamShakeMagnitude = 0.2f;
    [SerializeField] float hitCamShakeDuration = 0.2f;

    AudioPlayer audioPlayer;
    [Header("DeathSFX")]
    [SerializeField] AudioClip hitClip;
    [SerializeField] [Range(0f, 1f)]float hitVolume;

    [Header("HitSFX")]
    [SerializeField] bool isPlayer;
    [SerializeField] AudioClip playerIsHitClip;
    [SerializeField] [Range(0f, 1f)]float playerIsHitVolume;
    Coroutine hitCoroutine;
    [SerializeField] float hitSFXDuration;

    [Header("Scoring")]
    [SerializeField] int scoreValue;
    ScoreKeeper scoreKeeper;


    void Awake(){
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Update(){
        if (isPlayer)
        {
            GetPlayerHP();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
            if (isPlayer)
            {
                hitCoroutine = StartCoroutine(PlayClipCoroutine());
            }
            PlayCamShake(hitCamShakeMagnitude, hitCamShakeDuration);
            PlayBlastEffect();
        }
    }

    IEnumerator PlayClipCoroutine(){
        audioPlayer.PlayShootingClip(playerIsHitClip, playerIsHitVolume);
        yield return new WaitForSeconds(hitSFXDuration);
        if (hitCoroutine != null)
        {
            StopCoroutine(hitCoroutine);
            hitCoroutine = null;
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0){
            Die();
            audioPlayer.PlayBlastClip(hitClip, hitVolume);
            PlayCamShake(deathCamShakeMagnitude, deathCamShakeDuration);
        }
    }

    void Die(){
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(scoreValue);
        }
        Destroy(gameObject);
    }

    void PlayBlastEffect(){
        if (blastEffect != null)
        {
            ParticleSystem instance = Instantiate(blastEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void PlayCamShake(float shakeMagnitude, float shakeDuration){
        if (shakeCam && cameraShake != null)
        {
            cameraShake.ShakeCam(shakeMagnitude, shakeDuration);
        } 
    }

    public int GetPlayerHP(){
        return health;
    }

}
