using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject crashFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int score = 100;
    [SerializeField] int health = 5;

    Rigidbody rb;
    GameObject parentGameObject;
    GameObject findScoreBoard;
    ScoreBoard scoreBoard;

    private float gameStartTime;

    void Start()
    {
        gameStartTime = Time.time;
        findScoreBoard = GameObject.FindWithTag("ScoreScoreBoard");
        scoreBoard = findScoreBoard.GetComponent<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");

        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }


    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player" && gameObject.tag != "BigEnemy") ProcessHit();
        else if (gameObject.tag == "BigEnemy" && Time.time > 38.5 + gameStartTime) ProcessHit();

    }
    void ProcessHit()
    {
        scoreBoard.IncreaseScore(score);
        if (gameObject.tag == "BigEnemy") BigEnemyHit();
        else SmallEnemyHit();
    }

    void BigEnemyHit()
    {
        float randomX = Random.Range(-80, 80);
        float randomZ = Random.Range(-120, 120);
        Vector3 offset = new Vector3(randomX, 0, randomZ);
        GameObject vfx = Instantiate(hitVFX, transform.position + offset, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        KillEnemy();
        scoreBoard.IncreaseScore(score * 3);
    }

    void SmallEnemyHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        health -= 1;
        if (health <= 0)
        {
            KillEnemy();
            scoreBoard.IncreaseScore(score * 3);
        }
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(crashFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(this.gameObject);
    }

}
