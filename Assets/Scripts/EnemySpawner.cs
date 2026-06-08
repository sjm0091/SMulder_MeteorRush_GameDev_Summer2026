using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool endScreenActive;
    public GameObject enemyPrefab;
    public AudioSource audioSource;
    float spawnRate = 1.5f;
    float minY = 1.5f;
    float maxY = 4f;
    float nextSpawnTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime && !endScreenActive)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        bool spawnFromLeft = Random.value > 0.5f;
        float spawnX = spawnFromLeft ? -3f : 3f;
        float spawnY = Random.Range(minY, maxY);
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(spawnX, spawnY, 0f), Quaternion.Euler(0f, 0f, 180f));
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.enemySpawner = this;
        if (!spawnFromLeft)
        {
            enemyScript.moveSpeed *= -1;
        }
    }

    public void PlayAudio(AudioClip audioClip)
    {
        if (!endScreenActive)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
