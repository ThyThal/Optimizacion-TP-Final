using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviourUI
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Canvas loseCanvas;
    [SerializeField] private Canvas winCanvas;

    [SerializeField] private TMP_Text _lives;
    [SerializeField] private TMP_Text _bricks;

    public void UpdateLives(int amount)
    {
        _lives.text = $"Lives: {amount}";
    }

    public void UpdateBricks(int current, int total)
    {
       _bricks.text = $"Bricks: ({current}/{total})";
    }

    public void ShowWin()
    {
        winCanvas.enabled = true;
    }

    public void ShowLose()
    {
        loseCanvas.enabled = true;
    }

    public override void ManagedUpdate()
    {
        if (!levelManager.FinishedGame) return;

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
