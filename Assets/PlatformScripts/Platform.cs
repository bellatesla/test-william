using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int hp = 1;
    // Start is called before the first frame update
    public void RemoveHp(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            // destroy platform
            GameConditionManager manager = FindObjectOfType<GameConditionManager>();
            manager.PlatformDestroyed();
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
