using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bullet : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public static int DestroyedPlatforms;
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    void Update()
    {
       
    }

     public void OnTriggerEnter2D(Collider2D other)
     {
        if (other == null)
        {
            return;
        }

        if (other.gameObject.CompareTag("platform"))
        {

            other.GetComponent<Platform>().RemoveHp(damage);

            //DestroyedPlatforms++;
           // Debug.Log("Number of destroyed platforms: " + DestroyedPlatforms);
        }
        //if (DestroyedPlatforms >= 1)
        //{    
        //    SceneManager.LoadScene("Level 2");    
        //}
        
        
        
     }

    

     
}
