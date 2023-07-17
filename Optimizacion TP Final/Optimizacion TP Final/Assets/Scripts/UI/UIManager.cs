using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviourUI
{
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
}
