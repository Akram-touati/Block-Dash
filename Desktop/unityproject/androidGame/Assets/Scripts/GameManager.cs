using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public GameObject blockPrefab;         // Pr�fabriqu� pour les blocs
    public Transform spawnPoint;           // Point de spawn des blocs
    public float spawnRate;                // Vitesse de g�n�ration des blocs
    private bool gameStarted = false;      // Indicateur si le jeu a commenc�
    private bool gameIsOver = false;       // Indicateur si le jeu est termin�
    private float maxX = 2.8f;             // Limite maximale du spawn en X
    private int score = 0;                 // Score actuel du joueur

    [Header("UI Elements")]
    public GameObject tapToPlayText;       // Texte "Tap to Play"
    public GameObject gameOverUI;          // �cran Game Over
    public TextMeshProUGUI scoreText;      // Texte du score en cours
    public GameObject finalScorePanel;     // Panel du score final
    public TextMeshProUGUI finalScoreText; // Texte du score final

    [Header("Audio Settings")]
    public AudioSource backgroundMusic;    // Musique de fond

    void Start()
    {
        // Jouer la musique de fond d�s le d�marrage du jeu
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        // Assurer que l'UI de Game Over est cach�e au d�but
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        if (finalScorePanel != null)
        {
            finalScorePanel.SetActive(false);
        }
    }

    void Update()
    {
        // D�marrer le jeu en appuyant sur l'�cran si le jeu n'a pas encore commenc�
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            StartGame();
        }
    }

    // D�marrer la g�n�ration des blocs et cacher l'UI "Tap to Play"
    private void StartGame()
    {
        gameStarted = true;

        if (tapToPlayText != null)
        {
            tapToPlayText.SetActive(false);
        }

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        StartSpawningBlocks();
    }

    // D�marrer la g�n�ration des blocs � intervalles r�guliers
    private void StartSpawningBlocks()
    {
        InvokeRepeating("SpawnBlock", 0.5f, spawnRate);
    }

    // G�n�rer un bloc � un emplacement al�atoire en X
    private void SpawnBlock()
    {
        if (gameIsOver) return; // Ne pas continuer si le jeu est termin�

        Vector3 spawnPosition = spawnPoint.position;
        spawnPosition.x = Random.Range(-maxX, maxX);

        Instantiate(blockPrefab, spawnPosition, Quaternion.identity);

        // Augmenter le score � chaque bloc g�n�r�
        score++;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    // Arr�ter le jeu et afficher le score final
    public void StopGame()
    {
        gameIsOver = true;

        // Arr�ter de g�n�rer des blocs
        CancelInvoke("SpawnBlock");

        // Arr�ter la musique de fond
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        // Afficher le score final
        ShowFinalScore();
    }

    // Afficher l'�cran de score final
    private void ShowFinalScore()
    {
        if (finalScorePanel != null)
        {
            finalScorePanel.SetActive(true);
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "Score: " + score.ToString();
        }
    }
}
