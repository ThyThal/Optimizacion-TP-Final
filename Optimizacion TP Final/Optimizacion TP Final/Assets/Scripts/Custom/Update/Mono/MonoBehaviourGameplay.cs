using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourGameplay : ManagedTickObject
{
    public virtual void Awake()
    {
        CustomUpdateManager.Instance.CustomUpdateGameplay.AddObject(this);
    }

    public virtual void OnDestroy()
    {
        CustomUpdateManager.Instance.CustomUpdateGameplay.RemoveObject(this);
    }
}