using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float horizontalScreenSize = 11.5f;
    private float verticalScreenSize = 7.5f;
    private float speed;
    private int lives;

    public GameObject bullet;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
        lives = 3;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Initialize the lives display on start
        gameManager.UpdateLivesText(lives);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);
        if (transform.position.x > horizontalScreenSize || transform.position.x <= -horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenSize || transform.position.y < -verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collides with the coin
        if (other.CompareTag("Player"))
        {
            // Add score and destroy the coin
            gameManager.EarnScore(1);
            Destroy(gameObject);
        }
    }

    public void LoseALife()
    {
        lives--;
        gameManager.UpdateLivesText(lives); // Update the lives text on screen
        if (lives == 0)
        {
            Destroy(this.gameObject);
        }
    }
}