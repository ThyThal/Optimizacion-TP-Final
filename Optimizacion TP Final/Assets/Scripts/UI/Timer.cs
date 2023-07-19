using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviourUI
{
    [SerializeField] private TMP_Text _textTimer;

    public override void ManagedUpdate()
    {
        _textTimer.text = $"{GameManager.Instance.LevelManager.GetFormattedTime()}";
    }
}
