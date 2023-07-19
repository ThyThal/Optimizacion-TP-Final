using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomLogger
{
    [System.Diagnostics.Conditional("ENABLE_LOGS")]
    static public void Log(object message)
    {
        UnityEngine.Debug.Log(message);
    }
}