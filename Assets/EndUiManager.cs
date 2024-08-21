using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUiManager : MonoBehaviour
{
    public GameObject endGameUi;
    public GameObject leftStar;
    public GameObject rightStar;
    public GameObject middleStar;
    public weapon Weapon; // manually assign in editor
    public int threeStarMinimumBullets = 1;
    public int twoStarMaximumBullets = 5;

    int starCount;

    void Start()
    {
        endGameUi.SetActive(false);
    }

    
    public void ActivateUi()
    {
        endGameUi.SetActive(true);
        
        //start are on by default 3 star
        if (Weapon.totalShotsFired == threeStarMinimumBullets)
        {
            // do nothing 3 stars
            starCount = 3;
        }

        if (Weapon.totalShotsFired > threeStarMinimumBullets && Weapon.totalShotsFired < twoStarMaximumBullets)
        {
            // two stars
            rightStar.SetActive(false);
            starCount = 2;
        }

        if (Weapon.totalShotsFired >= twoStarMaximumBullets)
        {
            // one star
            starCount = 1;
            rightStar.SetActive(false);
            middleStar.SetActive(false);
        }
                
        
        //level manager update level
        LevelSelectManagement levelManager = FindObjectOfType<LevelSelectManagement>();
        levelManager.UpdateLevel(starCount);

    }
   
}
