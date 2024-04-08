using UnityEngine;

public class Friendly : MonoBehaviour
{
    [SerializeField] GameObject crashFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int health = 5;

    Rigidbody rb;
    GameObject parentGameObject;

    void Start()
    {
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Enemy") ProcessHit();
    }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        health -= 1;
        if (health <= 0) KillFriendly();
    }

    void KillFriendly()
    {
        GameObject vfx = Instantiate(crashFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(this.gameObject);
        AudioSource sfx = gameObject.GetComponent<AudioSource>();
        sfx.Play();
    }
}
