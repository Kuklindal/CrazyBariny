using UnityEngine;

public class Test2 : MonoBehaviour
{
    public InteractiveBox target;

    private void Start()
    {
        GetComponent<InteractiveBox>().AddNext(target);
    }
}