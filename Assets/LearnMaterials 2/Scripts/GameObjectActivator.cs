using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Задаёт указанным объектам значение activeSelf, равное targetState
/// </summary>
[HelpURL("https://docs.google.com/document/d/1GP4_m0MzOF8L5t5pZxLChu3V_TFIq1czi1oJQ2X5kpU/edit?usp=sharing")]
public class GameObjectActivator : MonoBehaviour
{
    [Header("Module Settings")]

    [SerializeField]
    [Tooltip("Отрисовывать связи с управляемыми объектами.\nЗелёная линия — будет включен.\nКрасная — выключен.")]
    private bool debug = false;

    [SerializeField]
    [Tooltip("Список объектов, которые будут переключаться.")]
    private List<StateContainer> targets = new List<StateContainer>();


    private void Awake()
    {
        foreach (var item in targets)
        {
            if (item != null && item.targetGO != null)
            {
                item.defaultValue = item.targetGO.activeSelf;
            }
        }
    }

    [ContextMenu("Переключить объекты")]
    public void ActivateModule()
    {
        SetStateForAll();
    }

    [ContextMenu("Вернуть объекты в состояние по умолчанию")]
    public void ReturnToDefaultState()
    {
        foreach (var item in targets)
        {
            if (item != null && item.targetGO != null)
            {
                item.targetState = item.defaultValue;
                item.targetGO.SetActive(item.defaultValue);
            }
        }
    }

    private void SetStateForAll()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null && targets[i].targetGO != null)
            {
                targets[i].targetGO.SetActive(targets[i].targetState);
                targets[i].targetState = !targets[i].targetState;
            }
            else
            {
                Debug.LogError("Элемент " + i + " равен null. Вероятно, была утеряна ссылка. Источник :" + gameObject.name);
            }
        }
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        if (!debug || targets == null) return;

        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(transform.position, 0.3f);

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null && targets[i].targetGO != null)
            {
                Gizmos.color = targets[i].targetState ? Color.green : Color.red;
                Gizmos.DrawLine(transform.position, targets[i].targetGO.transform.position);
            }
        }
    }
    #endregion
}

[System.Serializable]
public class StateContainer
{
    [Tooltip("Объект, которому нужно задать состояние")]
    public GameObject targetGO;

    [Tooltip("Если включено — объект будет активирован")]
    public bool targetState = false;

    [HideInInspector]
    public bool defaultValue;
}