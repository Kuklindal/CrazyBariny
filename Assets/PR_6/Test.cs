using UnityEngine;

public class Test : MonoBehaviour
{
    private ObstacleItem obstacle;

    private void Start()
    {
        obstacle = GetComponent<ObstacleItem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            obstacle.GetDamage(0.2f);
        }
    }
}