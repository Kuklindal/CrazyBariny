using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    [SerializeField]
    private List<ParentScript> scripts;
    [ContextMenu("Use")]
    public void ActivateAll()
    {
        foreach (var script in scripts)
        {
            script.Use();
        }
    }
}