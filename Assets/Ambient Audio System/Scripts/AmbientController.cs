using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientController : MonoBehaviour
{
    
    
    [Header("Ambient Audio Settings")]
    [Range(0, 1)]
    public float masterVolume = 1;
    
    private Transform player;
    private AmbientArea[] areas;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        areas = GetComponentsInChildren<AmbientArea>();
        StartCoroutine(UpdateAreas());
    }

    private IEnumerator UpdateAreas()
    {
        while (true)
        {
            foreach (var area in areas)
            {
                area.UpdateAmbientAreaAudio(player.position, masterVolume);
                yield return null;//perf: updates only one area per frame 
            }

        }
    }
}
