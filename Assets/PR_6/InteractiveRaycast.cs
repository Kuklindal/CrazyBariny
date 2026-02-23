using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveRaycast : MonoBehaviour
{
    [SerializeField] 
    private GameObject prefab;

    private InteractiveBox selectedBox;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            HandleLeftClick();

        if (Input.GetMouseButtonDown(1))
            HandleRightClick();
    }

    private void HandleLeftClick()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // 1️⃣ Клик по плоскости
            if (hit.collider.CompareTag("InteractivePlane"))
            {
                SpawnBox(hit);
                return;
            }

            // 2️⃣ Клик по InteractiveBox
            InteractiveBox clickedBox = hit.collider.GetComponentInParent<InteractiveBox>();

            if (clickedBox != null)
            {
                Debug.Log("Clicked box: " + clickedBox.name);
                
                if (selectedBox == null)
                {
                    selectedBox = clickedBox;
                }
                else
                {
                    selectedBox.AddNext(clickedBox);
                    selectedBox = null;
                }
            }
        }
    }

    private void HandleRightClick()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            InteractiveBox box = hit.collider.GetComponent<InteractiveBox>();

            if (box != null)
            {
                Destroy(box.gameObject);
            }
        }
    }

    private void SpawnBox(RaycastHit hit)
    {
        // Смещаем объект наружу от поверхности
        Vector3 spawnPosition = hit.point + hit.normal * 0.5f;

        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
