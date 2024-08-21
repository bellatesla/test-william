
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientArea : MonoBehaviour
{    
    public AudioClip audioClip;
    
    [Range(0,1)]
    public float volume = .5f;
    
    private AudioSource audioSource;     
    private BoxCollider area;
    private readonly float followSpeed = .5f;
    
    // gizmos color
    [SerializeField]private Color boxColor = new Color(0, 1, 0, .5f);
    

    private void Awake()
    {
        area = GetComponent<BoxCollider>();
        area.isTrigger = true;

        // creates an new Audio Source with a desired default settings
        string viewName = "Ambient Audio Source:" + audioClip.name;
        GameObject go = new GameObject(viewName);
        go.transform.position = area.transform.position;
        

        audioSource = go.AddComponent<AudioSource>();
        audioSource.transform.SetParent(this.transform);
        audioSource.dopplerLevel = 0;
        audioSource.spread = 0;
        audioSource.minDistance = 1;
        audioSource.maxDistance = 8;
        audioSource.loop = true;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.spatialBlend = 1; // 3D = 1
        audioSource.clip = audioClip;    

    }

    private void Start()
    {        
        audioSource.Play();
    }

    internal void UpdateAmbientAreaAudio(Vector3 playerPos, float masterVolume)
    {
        audioSource.volume = volume * masterVolume;

        // smooth audio position to player
        Vector3 target = area.ClosestPoint(playerPos);
        Vector3 current = audioSource.transform.position;
        audioSource.transform.position = Vector3.Lerp(current, target, followSpeed);
    }

    private void OnDrawGizmos()
    {
        if (area == null)
        {
            area = GetComponent<BoxCollider>();
            return;
        }

        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale); 
        
        Gizmos.color = boxColor;
        // force opacity to 50%
        boxColor.a = .5f;
        
        Gizmos.DrawCube(area.center,area.size);
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(area.center, area.size);

        // Restore the Gizmos matrix and color
        Gizmos.matrix = oldMatrix;
        Gizmos.color = Color.white;
    }

}
