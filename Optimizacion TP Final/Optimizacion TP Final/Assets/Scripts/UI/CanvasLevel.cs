using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class CanvasLevel : MonoBehaviourUI
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] private Canvas endPanel;
    [SerializeField] public TMP_Text textFinal;
    [SerializeField] private GameObject panelLevel;
    [SerializeField] private RawImage[] currentLife;

    [SerializeField] private TMP_Text _bricks;

    private int lastIndex;
    public override void Awake()
    {
        endPanel.enabled = false;
        lastIndex = currentLife.Length - 1;


        for (int i = 0; i < currentLife.Length; i++)
        {
            currentLife[i].enabled = true;
        }
    }
    
   
    //llamar cada vez que pierde vida
    public void LostLife()
    {
        if (lastIndex >= 0)
        {
            currentLife[lastIndex].enabled = false;
            lastIndex--;
        }
    }

    public void UpdateBricks(int current, int total)
    {
        _bricks.text = $"Bricks: ({current}/{total})";
    }

    //llamar cuando se gana
    public void Win()
    {
        textFinal.text = "Congrats";
        endPanel.enabled = true;
    }

    //llamar cuando se pierde
    public void Defeat()
    {
        textFinal.text = "Try Again";
        endPanel.enabled = true;
    }

    //se llama desde el boton
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
