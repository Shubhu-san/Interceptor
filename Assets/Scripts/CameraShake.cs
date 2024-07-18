using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // [SerializeField] float shakeDuration = 1f;
    // [SerializeField] float shakeMagnitude = 0.5f;
    Coroutine shakeCoroutine;

    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    public void ShakeCam(float camShakeMagnitude, float camShakeDuration){
        shakeCoroutine = StartCoroutine(Play(camShakeMagnitude, camShakeDuration));
    }
    
    IEnumerator Play(float shakeMag, float shakeDuration){
        float timeElapsed = 0;
        while (timeElapsed < shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMag;
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
        StopCoroutine(shakeCoroutine);
    }

}
