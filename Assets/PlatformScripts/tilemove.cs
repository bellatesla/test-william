using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tilemove : MonoBehaviour
{
    public float dirx, moveSpeed = 3f;
    bool moveRight = true;
    
    void Update()
    {
        if (transform.position.x > 2.69f)
            moveRight = false;
        if (transform.position.x < -2.69f)
            moveRight = true;

        if (moveRight)
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);


       
        
    }

    
    

}
