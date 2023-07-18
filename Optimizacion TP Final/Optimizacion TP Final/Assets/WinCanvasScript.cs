using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinCanvasScript : MonoBehaviourGameplay
{
    public Canvas canvas;

    // Update is called once per frame
    public override void ManagedUpdate()
    {
        if (canvas.isActiveAndEnabled && Input.GetKey(KeyCode.Space)) 
        {
            SceneManager.LoadScene(0);
        }
    }
}
