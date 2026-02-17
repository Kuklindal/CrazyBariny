using System.Collections;
using UnityEngine;

[HelpURL("https://docs.google.com/document/d/1RMamVxE-yUpSfsPD_dEa4-Ak1qu6NTo83qY1O4XLxUY/edit?usp=sharing")]
public class DestroyModule : MonoBehaviour
{
    [Header("Module State")]

    [SerializeField]
    [Tooltip("Включает вывод отладочной информации в консоль")]
    private bool debug = false;


    [Header("Destroy Settings")]

    [SerializeField]
    [Tooltip("Задержка между удалением объектов (в секундах)")]
    [Range(0.05f, 10f)]
    private float destroyDelay = 0.3f;

    [SerializeField]
    [Tooltip("Минимальное количество объектов, которое должно остаться")]
    [Min(0)]
    private int minimalDestroyingObjectsCount = 1;

    [ContextMenu("Начать удаление объектов")]
    public void ActivateModule()
    {
        StartCoroutine(DestroyRandomChildObjectCoroutine());
    }
    private Transform myTransform;

    private void Awake()
    {
        myTransform = transform;
    }

    private IEnumerator DestroyRandomChildObjectCoroutine()
    {
        if (myTransform == null)
            myTransform = transform;
        while (myTransform.childCount > minimalDestroyingObjectsCount)
        {
            int index = Random.Range(0, myTransform.childCount - 1);
            if (debug)
                Debug.Log($"[DestroyModule] Удаляю кубики с index: {index}", this);

            Destroy(myTransform.GetChild(index).gameObject);
            yield return new WaitForSeconds(destroyDelay);
        }
        if (debug)
            Debug.Log("[DestroyModule] Я ТУТ ВСЁ УНИЧТОЖУ", this);

        Destroy(gameObject, Time.deltaTime);
    }
}
