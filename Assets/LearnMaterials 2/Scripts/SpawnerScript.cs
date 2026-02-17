using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : ParentScript
{
    [Header("Spawn Settings")]

    [SerializeField]
    [Tooltip("Префаб, который будет создаваться")]
    private GameObject prefab;

    [SerializeField]
    [Min(1)]
    [Tooltip("Количество создаваемых копий")]
    private int count = 3;

    [SerializeField]
    [Min(0.1f)]
    [Tooltip("Шаг между объектами")]
    private float step = 1f;
    [ContextMenu("Use")]
    public override void Use()
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab не назначен!");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = transform.position + transform.forward * step * i;
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }
}
