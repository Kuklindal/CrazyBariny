using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTargetScript : ParentScript
{
    [Header("Shrink Settings")]

    [SerializeField]
    [Tooltip("Объект, чьи дочерние элементы будут удалены")]
    private Transform target;

    [SerializeField]
    [Min(0.1f)]
    [Tooltip("Скорость сжатия")]
    private float shrinkSpeed = 2f;
    [ContextMenu("Use")]
    public override void Use()
    {
        if (target == null)
        {
            Debug.LogError("Target не назначен!");
            return;
        }

        StartCoroutine(ShrinkAllChildren());
    }

    private IEnumerator ShrinkAllChildren()
    {
        foreach (Transform child in target)
        {
            StartCoroutine(ShrinkAndDestroy(child));
        }

        yield return null;
    }

    private IEnumerator ShrinkAndDestroy(Transform obj)
    {
        Vector3 startScale = obj.localScale;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * shrinkSpeed;
            obj.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
            yield return null;
        }

        Destroy(obj.gameObject);
    }
}