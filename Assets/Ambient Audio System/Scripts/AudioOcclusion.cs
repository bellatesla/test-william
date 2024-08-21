using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioOcclusion : MonoBehaviour
{
    public LayerMask occlusionLayer;  // Set this to the layer of walls or objects that occlude sound
    public float maxDistance = 100f;
    public float occludedVolumeMultiplier = 0.5f; // Volume when occluded

    public Vector3 leftEarOffset = new Vector3(-0.1f, 0f, 0f); // Adjust for left ear
    public Vector3 rightEarOffset = new Vector3(0.1f, 0f, 0f); // Adjust for right ear

    private AudioSource audioSource;
    private Transform listener;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        listener = Camera.main.transform; // Assuming the main camera has the AudioListener
    }

    void Update()
    {
        AdjustOcclusionAndPan();
    }

    void AdjustOcclusionAndPan()
    {
        float leftEarVolume = CheckEarOcclusion(leftEarOffset);
        float rightEarVolume = CheckEarOcclusion(rightEarOffset);

        // Calculate the final volume based on occlusion
        float finalVolume = Mathf.Min(leftEarVolume, rightEarVolume);
        audioSource.volume = Mathf.Lerp(audioSource.volume, finalVolume, Time.deltaTime * 5f);

        // Adjust the stereo pan based on ear volumes
        float stereoPan = CalculateStereoPan(leftEarVolume, rightEarVolume);
        audioSource.panStereo = Mathf.Lerp(audioSource.panStereo, stereoPan, Time.deltaTime * 5f);
    }

    float CheckEarOcclusion(Vector3 earOffset)
    {
        Vector3 earPosition = listener.position + listener.TransformDirection(earOffset);
        Vector3 direction = earPosition - transform.position;
        float distance = direction.magnitude;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance, occlusionLayer))
        {
            // If the ray hits an object in the occlusion layer, return occluded volume
            return occludedVolumeMultiplier;
        }
        else
        {
            // If no occlusion, return normal volume
            return 1f;
        }
    }

    float CalculateStereoPan(float leftEarVolume, float rightEarVolume)
    {
        // Pan the audio source based on the relative volume difference between the ears
        float pan = (rightEarVolume - leftEarVolume) * 0.5f;
        return Mathf.Clamp(pan, -1f, 1f); // Clamp to stereo pan range [-1, 1]
    }
}
