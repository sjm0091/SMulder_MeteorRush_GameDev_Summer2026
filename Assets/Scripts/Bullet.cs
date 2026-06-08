using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject firePoint;
    public GameManager gameManager;
    public PlayerController player;
    public AudioClip explosionClip;
    float speed = 7f;
    float maxY = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y > maxY)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameManager.score += 1;
            gameManager.scoreText.text = "Score: " + gameManager.score;
            Destroy(other.gameObject);
            player.PlayAudio(explosionClip);
            Destroy(gameObject);
        }
    }
}
