using UnityEngine;

public class InteractiveBox : MonoBehaviour
{
    private InteractiveBox next;

    private void Update()
    {
        if (next == null) return;

        Vector3 startPos = transform.position + Vector3.up * 0.5f;
        Vector3 endPos = next.transform.position + Vector3.up * 0.5f;
        Vector3 direction = (endPos - startPos).normalized;
        float distance = Vector3.Distance(startPos, endPos);

        Debug.DrawLine(startPos, endPos, Color.green);

        RaycastHit[] hits = Physics.RaycastAll(startPos, direction, distance);

        foreach (RaycastHit hit in hits)
        {
        ObstacleItem obstacle = hit.collider.GetComponent<ObstacleItem>();

         if (obstacle != null)
         {
          obstacle.GetDamage(Time.deltaTime);
         }
        }
    }

    public void AddNext(InteractiveBox box)
    {
        if (box == this)
        {
            Debug.Log("Нельзя добавить самого себя");
            return;
        }
        Debug.Log("Связь создана: " + name + " -> " + box.name);
        next = box;
    }

    public void RemoveNext()
    {
        next = null;
    }
}