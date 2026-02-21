using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Renderer))]
public class ObstacleItem : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField]
    private float currentValue = 1f;

    [SerializeField]
    private UnityEvent onDestroyObstacle;

    private Renderer objectRenderer;

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        updateColor();
    }

    public void GetDamage(float value)
    {
        currentValue -= value;
        currentValue = Mathf.Clamp01(currentValue);

        updateColor();

        if (currentValue <= 0f)
        {
            destroyObstacle();
        }
    }

    private void updateColor()
    {
        Color targetColor = Color.Lerp(Color.red, Color.white, currentValue);
        objectRenderer.material.color = targetColor;
    }

    private void destroyObstacle()
    {
        onDestroyObstacle?.Invoke();
        Destroy(gameObject);
    }
}