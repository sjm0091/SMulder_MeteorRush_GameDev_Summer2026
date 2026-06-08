using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioSource audioSource;
    public AudioClip shootClip;
    public bool endScreenActive;
    float moveSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D rb;
    float minX = -2.5f;
    float maxX = 2.5f;
    float minY = -4.5f;
    float maxY = 4.5f;
    float fireRate = 0.25f;
    float nextFireTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!endScreenActive)
        {
            Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);
            transform.position += movement * moveSpeed * Time.deltaTime;

            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            transform.position = pos;
        }
    }

    public void OnMove(InputValue value)
    {
        if (!endScreenActive)
        {
            moveInput = value.Get<Vector2>();
        }
    }

    public void OnAttack()
    {
        if (Time.time >= nextFireTime && !endScreenActive)
        {
            audioSource.PlayOneShot(shootClip);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().player = this;
            nextFireTime = Time.time + fireRate;
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
