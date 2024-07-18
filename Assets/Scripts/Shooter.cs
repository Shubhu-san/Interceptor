using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 12f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float fireRate = 0.2f;
    Coroutine firingCoroutine;
    [HideInInspector] public bool isFiring;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float fireRateVariance;
    [SerializeField] float minFireRate;

    AudioPlayer audioPlayer;

     [Header("PlayerFireSFX")]
    [SerializeField] AudioClip playerShootingClip;
    [SerializeField] [Range(0f, 1f)]float playerShootingVolume = 1;

    [Header("EnemyFireSFX")]
    [SerializeField] AudioClip enemyShootingClip;
    [SerializeField] [Range(0f, 1f)]float enemyShootingVolume = 1;

    void Awake(){
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        } else if(!isFiring && firingCoroutine != null) {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously(){
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 180));

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed ;
            }
            Destroy(instance, projectileLifetime);
            if (useAI)
            {
                audioPlayer.PlayShootingClip(enemyShootingClip, enemyShootingVolume);
                yield return new WaitForSeconds(GetRandomFireRate());
            } else {
                audioPlayer.PlayShootingClip(playerShootingClip, playerShootingVolume);
                yield return new WaitForSeconds(fireRate);
            }
        }

    }

    public float GetRandomFireRate(){
     float randomFireRate = Random.Range(fireRate - fireRateVariance, fireRate + fireRateVariance);
     return Mathf.Clamp(randomFireRate, minFireRate, float.MaxValue);
   }
}
