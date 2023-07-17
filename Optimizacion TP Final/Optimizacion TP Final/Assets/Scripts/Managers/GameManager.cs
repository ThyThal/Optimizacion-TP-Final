using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourGameplay
{
    // Game Manager Instance
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private LevelManager _levelManager;

    public LevelManager LevelManager
    {
        get { return _levelManager; }
        set { _levelManager = value; }
    }

    public void SetLevelManager(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }
}