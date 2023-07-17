using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuScript : MonoBehaviourUI
{
    public GameObject Credits;

    public override void ManagedUpdate() 
    {
        if (Input.GetKey(KeyCode.C))
        {
            Credits.SetActive(true);
        }
        else 
        {
            Credits.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Escape)) 
        {
            ExitGame();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            LoadSceneLevel();
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    public void LoadSceneLevel()
    {
        SceneManager.LoadScene("Gameplay");
    }

}
