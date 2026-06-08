using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Transform player;
    public bool endScreenActive;
    float maxY = 0f;
    float minY = -5f;
    float spawnRate = 4f;
    float nextSpawnTime = 0f;
    bool firstMeteor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        firstMeteor = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnMeteor();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    public void SpawnMeteor()
    {
        float spawnY = 0;
        if (firstMeteor)
        {
            spawnY = Random.Range(-2, maxY);
            firstMeteor = false;
        }
        else
        {
            spawnY = Random.Range(minY, maxY);
        }
        bool spawnFromLeft = Random.value > 0.5f;
        float spawnX = spawnFromLeft ? -3f : 3f;
        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);
        GameObject meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.Euler(0f, 180f, 0f));
        meteor.GetComponent<Meteor>().player = player;
        meteor.GetComponent<Meteor>().spawner = this;
    }
}
