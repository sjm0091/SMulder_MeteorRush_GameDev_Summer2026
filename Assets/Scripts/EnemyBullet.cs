using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBullet : MonoBehaviour
{
    public Enemy enemy;
    public AudioClip playerHit;
    public GameManager gameManager;
    float speed = 5f;
    float minY = -10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.PlayAudio(playerHit);
            gameManager.RemoveHealth();
            Debug.Log("Player hit");
            Destroy(gameObject);
        }
    }
}
