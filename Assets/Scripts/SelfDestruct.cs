using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeTillDestroy = 2f;
    void Start()
    {
        Destroy(gameObject, timeTillDestroy);
    }
}
