using UnityEngine;

public class EnemyPewpew : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // destroy bullet after time
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bullet hit player");

            
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
