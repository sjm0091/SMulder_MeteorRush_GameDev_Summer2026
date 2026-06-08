using System.Collections.Generic;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public bool endScreenActive;
    public PlayerController player;
    public SpriteRenderer playerSpriteRenderer;
    public EnemySpawner enemySpawner;
    public MeteorSpawner meteorSpawner;
    public GameObject countDownNumbers;
    public SpriteRenderer countDownTextSpriteRenderer;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI gameOverText;
    public int score;
    public int health;
    public int startingHealth;
    public GameObject scoreBoard;
    public GameObject healthIconPrefab;
    public Transform healthIconPositioner;
    public Transform healthIconParent;
    public Transform canvas;
    public List<GameObject> healthIconList;
    public Vector3[] healthIconXPositions;
    public AudioSource audioSource;
    public AudioClip explosionClip;
    public Sprite[] numberSpriteList;
    public Sprite[] cloudSprites;
    public int maxRows; // Maximum rows of health icons allowed
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countDownTextSpriteRenderer = countDownNumbers.GetComponent<SpriteRenderer>();
        countDownNumbers.SetActive(false);
        endScreenActive = false;
        player.endScreenActive = false;
        meteorSpawner.endScreenActive = false;
        endScoreText.gameObject.SetActive(false);
        if (startingHealth > (maxRows * 8))
        {
            startingHealth = maxRows * 8;
        }
        health = startingHealth;
        scoreText.gameObject.SetActive(true);
        int row = 0;
        for (int i = 0; i < startingHealth; i++)
        {
            if ((i % 8) == 0 && i != 0)
            {
                row++;
            }
            GameObject healthIcon = Instantiate(healthIconPrefab, healthIconPositioner.position, healthIconPositioner.rotation, healthIconParent);
            healthIcon.transform.localPosition = new Vector3(healthIconXPositions[i % 8].x, healthIcon.transform.position.y + (row * (50 + 25)), 0f);
            //healthIcon.transform.position = new Vector3(((i + 1) * (75/2)) + ((i + 1) * 60), healthIcon.transform.position.y, healthIcon.transform.position.z);
            healthIconList.Add(healthIcon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadEndScreen()
    {
        audioSource.PlayOneShot(explosionClip);
        StartCoroutine(PlayerExplodeAnimation());
        if (health != 0)
        {
            Destroy(healthIconParent.gameObject);
        }
        Time.timeScale = 0.1f;
        endScoreText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        endScoreText.text = "Score: " + score;
        scoreText.gameObject.SetActive(false);
        endScreenActive = true;
        player.endScreenActive = true;
        enemySpawner.endScreenActive = true;
        meteorSpawner.endScreenActive = true;
        countDownNumbers.SetActive(true);
        StartCoroutine(StartOver());
    }

    public void RemoveHealth()
    {
        GameObject lastIcon = healthIconList[healthIconList.Count - 1];
        healthIconList.Remove(lastIcon);
        health--;
        Destroy(lastIcon);
        if (healthIconList.Count == 0)
        {
            LoadEndScreen();
        }
    }

    IEnumerator StartOver()
    {
        //StartCoroutine(CountDown());
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator PlayerExplodeAnimation()
    {
        for (int i = 0; i < cloudSprites.Length; i++)
        {
            playerSpriteRenderer.sprite = cloudSprites[cloudSprites.Length - i - 1];
            yield return new WaitForSeconds(0.01f);
        }
        playerSpriteRenderer.sprite = null;
    }
}
