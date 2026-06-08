using UnityEngine;

public class Meteor : MonoBehaviour
{
    public MeteorSpawner meteorSpawner;
    public float moveSpeed;
    public Transform player;
    public Vector3 direction;
    public MeteorSpawner spawner;
    public GameManager gameManager;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        int spriteIndex = Random.Range(0, sprites.Length - 1);
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.position - transform.position;
        Debug.Log("direction: " + direction);
        transform.position += Vector3.Normalize(direction) * moveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!spawner.endScreenActive)
            {
                gameManager.LoadEndScreen();
                Debug.Log("meteor hit player!");
                Destroy(gameObject);
            }
        }
    }
}
