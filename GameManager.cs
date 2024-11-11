using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyOne;
    public GameObject cloud;
    public GameObject coinPrefab;  // Reference to the Coin prefab

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    private int score;
    private int playerLives = 3;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        InvokeRepeating("CreateEnemyOne", 1f, 3f);
        InvokeRepeating("SpawnCoin", 2f, 5f);  // Spawn a coin every 5 seconds
        CreateSky();
        score = 0;
        scoreText.text = "Score: " + score;

        UpdateLivesText();  // Initialize lives display
    }

    // Update is called once per frame
    void Update()
    {
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOne, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.Euler(0, 0, 180));
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
        }
    }

    public void EarnScore(int newScore)
    {
        score += newScore;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLivesText(int currentLives = -1)
    {
        if (currentLives != -1) playerLives = currentLives;
        livesText.text = "Lives: " + playerLives;
    }

    // New method to spawn coins randomly
    void SpawnCoin()
    {
        // Generate a random position for the coin within screen bounds
        float xPosition = Random.Range(-9f, 9f);
        float yPosition = Random.Range(-4f, 4f);
        Vector3 spawnPosition = new Vector3(xPosition, yPosition, 0);

        // Instantiate the coin at the random position
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
    }
}