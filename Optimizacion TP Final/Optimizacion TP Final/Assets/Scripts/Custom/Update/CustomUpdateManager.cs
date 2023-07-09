using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomUpdateManager : MonoBehaviour
{
    #region Custom Update Manager Instance
    public static CustomUpdateManager Instance;
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
    #endregion

    [SerializeField] private CustomUpdate _customUpdateGameplay;
    [SerializeField] private CustomUpdate _customUpdateUI;

    public CustomUpdate CustomUpdateGameplay => _customUpdateGameplay;
    public CustomUpdate CustomUpdateUI => _customUpdateUI;

    // Update is called once per frame
    void Update()
    {
        if (_customUpdateGameplay != null)
        {
            PerformCustomUpdate(_customUpdateGameplay);
        }

        if (_customUpdateUI != null)
        {
            PerformCustomUpdate(_customUpdateUI);
        }
    }


    private void PerformCustomUpdate(CustomUpdate customUpdate)
    {
        if (customUpdate.GetDeltaTime >= customUpdate.GetTickInterval)
        {
            #if ENABLE_LOGS
            CustomLogger.Log($"{customUpdate.GetName}: Target FPS={customUpdate.GetTargetFPS} | Tick Interval={customUpdate.GetTickInterval}");
            #endif

            customUpdate.DoTick();

            for (int i = customUpdate.GetManagedObjects.Count - 1; i >= 0; i--)
            {
                var managedObject = customUpdate.GetManagedObjects[i];
                if (managedObject.isActiveAndEnabled)
                {
                    managedObject.ManagedUpdate();
                }
            }
        }
    }

    /// <summary>
    /// Define the Custom Update and its Methods.
    /// </summary>
    [System.Serializable] public class CustomUpdate
    {
        [SerializeField] private string _name = "Custom Update";
        [SerializeField] [Range(1, 60)] private int _targetFPS = 60;
        [SerializeField] private List<ManagedTickObject> _managedObjects;
        [SerializeField] private float _lastUpdate;

        /// <summary>
        /// Sets a Target FPS for the Custom Update.
        /// </summary>
        /// <param name="TargetFPS">Target for FPS.</param>
        public void SetTargetFPS(int TargetFPS)
        {
            _targetFPS = Mathf.Clamp(TargetFPS, 1, 60);
        }

        /// <summary>
        /// Returns Target FPS.
        /// </summary>
        public int GetTargetFPS => _targetFPS;

        /// <summary>
        /// Returns Last Update Time.
        /// </summary>
        private float GetLastUpdate => _lastUpdate;

        /// <summary>
        /// Returns the Time needed to reach the Target FPS.
        /// </summary>
        public float GetTickInterval => 1 / (float)GetTargetFPS;

        /// <summary>
        /// Returns Delta Time for Current Object.
        /// </summary>
        public float GetDeltaTime => Time.time - GetLastUpdate;

        /// <summary>
        /// Gets the Name of the Custom Update.
        /// </summary>
        public string GetName => _name;

        /// <summary>
        /// Returns the List that Contains Updatable Objects.
        /// </summary>
        public List<ManagedTickObject> GetManagedObjects => _managedObjects;

        /// <summary>
        /// Sets Last Update of the Class.
        /// </summary>
        public void DoTick()
        {
            _lastUpdate = Time.time;
        }

        /// <summary>
        /// Adds an Object to the Custom Update.
        /// </summary>
        /// <param name="managedObject">Object to Add.</param>
        public void AddObject(ManagedTickObject managedObject)
        {
            _managedObjects.Add(managedObject);
        }

        /// <summary>
        /// Removes an Object from the Custom Update.
        /// </summary>
        /// <param name="managedObject">Object to Remove.</param>
        public void RemoveObject(ManagedTickObject managedObject)
        {
            _managedObjects.Remove(managedObject);
        }

    }
}
