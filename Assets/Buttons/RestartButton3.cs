using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton3 : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Level 3");
    }
}
