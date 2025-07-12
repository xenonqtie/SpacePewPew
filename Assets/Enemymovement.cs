using UnityEngine;

public class Enemymovement : MonoBehaviour
{
    public float speed = 2f;
    public GameObject bulletPrefab;
    public float fireRate = 2f;

    private float fireTimer;

    void Start()
    {
        fireTimer = 0f; 
    }

    void Update()
    {
       
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Fire();
            fireTimer = fireRate; 
        }
    }

    void Fire()
    {
        Vector3 spawnPos = transform.position + Vector3.down * 0.5f;
        Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        Debug.Log("🔫 Enemy fired bullet");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🚨 Enemy collided with player!");
            Gameover gameOverManager = FindObjectOfType<Gameover>();
            if (gameOverManager != null)

            {
                gameOverManager.GameOver();
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}

