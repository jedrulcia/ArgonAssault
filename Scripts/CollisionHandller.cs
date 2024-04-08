using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandller : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;
    [SerializeField] ParticleSystem crashVFX;
    [SerializeField] ParticleSystem hitVFX;

    [SerializeField] int health = 10;
    GameObject findHPBoard;
    HPBoard HPBoard;

    void Start()
    {
        findHPBoard = GameObject.FindWithTag("HPScoreBoard");
        HPBoard = findHPBoard.GetComponent<HPBoard>();
    }
    void OnCollisionEnter(Collision other)
    {
        StartCrashingSequence();
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name} -- triggered -- {other.gameObject.name}");
    }
    void StartCrashingSequence()
    {
        crashVFX.Play();
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("ReloadScene", loadDelay);
        AudioSource sfx = gameObject.GetComponent<AudioSource>();
        sfx.Play();
    }
    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Enemy")
        {
            ProcessHit();
        }
    }
    void ProcessHit()
    {
        ParticleSystem vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        health -= 1;
        HPBoard.UpdateHealth(health);
        if (health <= 0) StartCrashingSequence();
    }
}
