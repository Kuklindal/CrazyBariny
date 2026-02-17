using System.Collections;
using UnityEngine;

[HelpURL("https://docs.google.com/document/d/1rdTEVSrCcYOjqTJcFCHj46RvnbdJhmQUb3gHMDhVftI/edit?usp=sharing")]
public class ScalerModule : MonoBehaviour
{
    [Header("Module State")]
    [SerializeField]
    [Tooltip("Включает вывод отладочной информации")]
    private bool debug = false;

    [Header("Scale Settings")]
    [SerializeField]
    [Tooltip("Размер, к которому будет стремиться объект")]
    private Vector3 targetScale = new Vector3(2, 2, 2);

    [SerializeField]
    [Tooltip("Скорость изменения размера")]
    [Min(0.01f)]
    private float changeSpeed = 1f;

    private Vector3 defaultScale;
    private Transform myTransform;
    private bool toDefault;
    void Start()
    {
        myTransform = transform;
        defaultScale = myTransform.localScale;
        toDefault = false;
        ActivateModule();
    }

    public void ActivateModule()
    {
        Vector3 target = toDefault ? defaultScale : targetScale;

        if (debug)
            Debug.Log($"[ScalerModule] Изменяем размеры до {target}", this);
        StopAllCoroutines();
        StartCoroutine(ScaleCoroutine(target));
        toDefault = !toDefault;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ActivateModule();
    }
    private IEnumerator ScaleCoroutine(Vector3 target)
    {
        Vector3 start = myTransform.localScale;
        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime * changeSpeed;
            myTransform.localScale = Vector3.Lerp(start, target, t);
            yield return null;
        }
        myTransform.localScale = target;
        if (debug)
            Debug.Log("[ScalerModule] Объект изменил размер", this);
    }
}
