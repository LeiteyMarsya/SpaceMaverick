using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Aisyah Aida

public class ChangeScene : MonoBehaviour
{
   public void GoToGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
