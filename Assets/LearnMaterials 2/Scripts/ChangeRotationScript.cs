using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRotationScript : ParentScript
{
    [Header("Rotation Settings")]
    [SerializeField] private Vector3 rotationAngles = new Vector3(0, 90, 0);
    [SerializeField] private float rotationSpeed = 10f;

    private Coroutine rotateCoroutine;

    void Start()
    {
        Use();
    }

    public override void Use()
    {
        if (rotateCoroutine != null)
            StopCoroutine(rotateCoroutine);

        rotateCoroutine = StartCoroutine(RotateCoroutine());
    }
    private IEnumerator RotateCoroutine()
    {
        Quaternion startRot = transform.rotation;
        Quaternion targetRot = startRot * Quaternion.Euler(rotationAngles);

        float maxAngle = Mathf.Max(
            Mathf.Abs(rotationAngles.x),
            Mathf.Abs(rotationAngles.y),
            Mathf.Abs(rotationAngles.z)
        );

        float duration = maxAngle / rotationSpeed;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }

        transform.rotation = targetRot;
    }
}
