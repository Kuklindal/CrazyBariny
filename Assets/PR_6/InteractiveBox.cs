using UnityEngine;

public class InteractiveBox : MonoBehaviour
{
    private InteractiveBox next;

    private void Update()
    {
        if (next == null) return;

        Vector3 startPos = transform.position;
        Vector3 endPos = next.transform.position;
        Vector3 direction = (endPos - startPos).normalized;
        float distance = Vector3.Distance(startPos, endPos);

        Debug.DrawLine(startPos, endPos, Color.green);

        if (Physics.Raycast(startPos, direction, out RaycastHit hit, distance))
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
            Debug.Log("Нельзя добавить самого себя как next");
            return;
        }

        next = box;
    }

    public void RemoveNext()
    {
        next = null;
    }
}