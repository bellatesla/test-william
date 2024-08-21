using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    public Transform player; 
    public AudioClip audioClip;

    //since we move the audio source we need this as a seperate child object
    private AudioSource audioSource;
    private const float fadeDuration = 3f;//seconds

    private Vector3 lastPosition;
    [SerializeField]private float followSpeed = 3;
    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        audioSource.clip = audioClip;
    }

    private void Start()
    {
        //find player
    }

    private void ActivateSound()
    {
        StopAllCoroutines();
        audioSource.Play();
        StartCoroutine(FadeVolumeTo(1));
    }    

    private void DeactivateSound()
    {
        StopAllCoroutines();
        StartCoroutine(FadeVolumeTo(0));
    }

    /// <summary>
    /// Fade the volume from/to 0 to 1.
    /// </summary>
    /// <param name="targetVolume"0 to 1</param>
    /// <returns></returns>
    IEnumerator FadeVolumeTo(float targetVolume) 
    {
        targetVolume = Mathf.Clamp01(targetVolume);

        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeDuration);
            yield return null;
        }

        // Ensure the final volume is set to the target volume
        audioSource.volume = targetVolume;

        if(audioSource.volume == 0) audioSource.Stop();
    }

    private void UpdateAudioSourcePosition()
    {
        audioSource.transform.position = Vector3.Lerp(lastPosition, player.position, followSpeed * Time.deltaTime);
        lastPosition = audioSource.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player.transform != other.transform) return;

        ActivateSound();
    }

    private void OnTriggerExit(Collider other)
    {
        if (player.transform != other.transform) return;
        
        DeactivateSound();
    }

    private void OnTriggerStay(Collider other)
    {
        if (player.transform != other.transform) return;
        
        UpdateAudioSourcePosition();
    }
    
}
