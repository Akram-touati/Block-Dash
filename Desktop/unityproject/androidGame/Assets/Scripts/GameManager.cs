using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public GameObject blockPrefab;         // Préfabriqué pour les blocs
    public Transform spawnPoint;           // Point de spawn des blocs
    public float spawnRate;                // Vitesse de génération des blocs
    private bool gameStarted = false;      // Indicateur si le jeu a commencé
    private bool gameIsOver = false;       // Indicateur si le jeu est terminé
    private float maxX = 2.8f;             // Limite maximale du spawn en X
    private int score = 0;                 // Score actuel du joueur

    [Header("UI Elements")]
    public GameObject tapToPlayText;       // Texte "Tap to Play"
    public GameObject gameOverUI;          // Écran Game Over
    public TextMeshProUGUI scoreText;      // Texte du score en cours
    public GameObject finalScorePanel;     // Panel du score final
    public TextMeshProUGUI finalScoreText; // Texte du score final

    [Header("Audio Settings")]
    public AudioSource backgroundMusic;    // Musique de fond

    void Start()
    {
        // Jouer la musique de fond dès le démarrage du jeu
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }

        // Assurer que l'UI de Game Over est cachée au début
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
        // Démarrer le jeu en appuyant sur l'écran si le jeu n'a pas encore commencé
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            StartGame();
        }
    }

    // Démarrer la génération des blocs et cacher l'UI "Tap to Play"
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

    // Démarrer la génération des blocs à intervalles réguliers
    private void StartSpawningBlocks()
    {
        InvokeRepeating("SpawnBlock", 0.5f, spawnRate);
    }

    // Générer un bloc à un emplacement aléatoire en X
    private void SpawnBlock()
    {
        if (gameIsOver) return; // Ne pas continuer si le jeu est terminé

        Vector3 spawnPosition = spawnPoint.position;
        spawnPosition.x = Random.Range(-maxX, maxX);

        Instantiate(blockPrefab, spawnPosition, Quaternion.identity);

        // Augmenter le score à chaque bloc généré
        score++;
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    // Arrêter le jeu et afficher le score final
    public void StopGame()
    {
        gameIsOver = true;

        // Arrêter de générer des blocs
        CancelInvoke("SpawnBlock");

        // Arrêter la musique de fond
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }

        // Afficher le score final
        ShowFinalScore();
    }

    // Afficher l'écran de score final
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
