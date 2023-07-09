using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourUI : ManagedTickObject
{
    public virtual void Awake()
    {
        CustomUpdateManager.Instance.CustomUpdateUI.AddObject(this);
    }

    public virtual void OnDestroy()
    {
        CustomUpdateManager.Instance.CustomUpdateUI.RemoveObject(this);
    }
}