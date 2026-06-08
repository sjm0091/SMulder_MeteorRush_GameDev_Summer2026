
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public Transform enemyFirePoint;
    public EnemySpawner enemySpawner;
    public float moveSpeed;
    public float waveAmount;
    public float waveSpeed;
    public AudioClip shootClip;
    float startY;
    float fireRate = 1.5f;
    float nextFireTime = 0f;
    void Awake()
    {
        moveSpeed = Random.Range(1.5f, 3f);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //moveSpeed = Random.Range(1.5f, 3f);
        waveAmount = Random.Range(0.5f, 2f);
        waveSpeed = Random.Range(1f, 3f);
        startY = transform.position.y;
        nextFireTime = Time.time + Random.Range(0.5f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        float Y = startY + Mathf.Sin(Time.time * waveSpeed) * waveAmount;
        transform.position = new Vector3(transform.position.x, Y, transform.position.z);

        if (Time.time > nextFireTime && transform.position.x < 2.7f && transform.position.x > -2.7f) // added so they don't fire if off screen (so player doesn't hear shooting and not see anything)
        {
            Shoot(); 
            nextFireTime = Time.time + fireRate;
        }

        if (transform.position.x > 4f || transform.position.x < -4f)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        PlayAudio(shootClip);
        GameObject enemyBullet = Instantiate(enemyBulletPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
        enemyBullet.GetComponent<EnemyBullet>().enemy = this;
    }

    public void PlayAudio(AudioClip audioClip)
    {
        enemySpawner.PlayAudio(audioClip);
    }
}
