using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionScript : ParentScript
{
    [Header("Movement Settings")]
    [SerializeField] private Vector3 targetPosition = new Vector3(3, 0, 0);
    [SerializeField] private float moveSpeed = 1f;

    private Coroutine moveCoroutine;
    void Start()
    {
        Use();
    }
    public override void Use()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveCoroutine());
    }
    private IEnumerator MoveCoroutine()
    {
        Vector3 startPos = transform.position;
        float distance = Vector3.Distance(startPos, targetPosition);
        float duration = distance / moveSpeed;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(startPos, targetPosition, t);
            yield return null;
        }

        transform.position = targetPosition;
    }
}
