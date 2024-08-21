using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelSelectManagement : MonoBehaviour
{
    public GameData gameData;
    // public List<LevelData> levelsData = new List<LevelData>();
    public List<StarController> starControllers = new List<StarController>();
    
    //const string subDir = "Player Saves";

    public static string SAVEPATH
    {
        get { return Application.persistentDataPath + "/" ; }

    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

   
    void Start()
    {
        // find all star controllers
        starControllers = FindObjectsOfType<StarController>().ToList();

        // load data from file
        LoadFromFile();
        // apply data to level buttons
        ApplyData();
    }

    

    [ContextMenu("Save Game Data")]
    void SaveToFile()
    {
        BinaryFormatter bf = new BinaryFormatter(); //Create new or overwrite
        string path = SAVEPATH + "/" + "level save" + ".dat";
        FileStream file = File.Create(path); //Create a new save point file -- overwrites

        bf.Serialize(file, gameData); //save /serilize data

        file.Close(); //close it
        Debug.Log("saved game data");
    }





    [ContextMenu("Load Game Data")]
    void LoadFromFile()
    {
        string path = SAVEPATH + "/" + "level save" + ".dat"; //req .dat
        if (File.Exists(path))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);

            //************* LOAD ALL THE PLAYER DATA
            gameData = (GameData)bf.Deserialize(file);

            file.Close();

            Debug.Log("Loaded game data");

        }
        else
        {
            Debug.Log("Game data not found at: " + SAVEPATH + " Loading defauts") ;
        }

        
           
    }


    public void UpdateLevel(int starCount)
    {
        GameConditionManager gameMan = FindObjectOfType<GameConditionManager>();

        gameData.levelsData[gameMan.currentLevel - 1].starRating = starCount;
        gameData.levelsData[gameMan.currentLevel - 1].hasCompleted = true;


        //save to hard drive next
        SaveToFile();
    }



    //only available in load menu
    void ApplyData()
    {
        for (int i = 0; i < gameData.levelsData.Count; i++)
        {
            int starAmount = gameData.levelsData[i].starRating;
            starControllers[i].SetStarAmount(starAmount);

                       
            starControllers[i].levelUnlocked = gameData.levelsData[i].hasCompleted;
        }
    }


   
}
