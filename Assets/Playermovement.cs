using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Playermovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float destroyTime = 5f;
    public AudioClip shootSound;
    public AudioSource audioSource;

    private Camera mainCamera;

    [Header("Spaceship Dimensions")]
    public float spaceshipWidth = 0.5f;
    public float spaceshipHeight = 0.5f;

    void Start()
    {
        mainCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }



    void Update()
    {
        PlayerMovement();
        PlayerShoot();
    }

    void PlayerMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(moveX, moveY, 0f).normalized;
        Vector3 newPos = transform.position + moveDir * moveSpeed * Time.deltaTime;

        float distanceFromCamera = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        Vector3 screenlowerleft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenupperright = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        if (newPos.x < screenlowerleft.x - spaceshipWidth)
            newPos.x = screenupperright.x + spaceshipWidth;
        else if (newPos.x > screenupperright.x + spaceshipWidth)
            newPos.x = screenlowerleft.x - spaceshipWidth;

        if (newPos.y < screenlowerleft.y - spaceshipHeight)
            newPos.y = screenupperright.y + spaceshipHeight;
        else if (newPos.y > screenupperright.y + spaceshipHeight)
            newPos.y = screenlowerleft.y - spaceshipHeight;

        transform.position = newPos;


    }

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject gm = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            if (shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }

            gm.transform.SetParent(null);
            Destroy(gm, destroyTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}