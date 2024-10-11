using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Variables publiques pour personnaliser le comportement dans l'éditeur Unity
    [Header("Player Settings")]
    public float moveSpeed;

    // Composants
    private Rigidbody2D rb;

    [Header("UI Elements")]
    public GameObject gameOverUI;

    // Audio
    [Header("Audio Sources")]
    public AudioSource collisionSound; // Son de collision
   

    // Gestion de l'état du jeu
    private bool gameIsOver = false;

    // Initialisation des composants
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // S'assurer que les sons sont arrêtés au début du jeu
        if (collisionSound != null)
        {
            collisionSound.Stop();
        }

       

        // Cacher l'écran Game Over
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    // Gestion des entrées utilisateur et des mouvements du joueur
    void Update()
    {
        if (gameIsOver)
        {
            // Si le jeu est terminé, appuyer sur l'écran redémarre la scène
            if (Input.GetMouseButtonDown(0))
            {
                RestartGame();
            }
        }
        else
        {
            HandlePlayerMovement();
        }
    }

    // Gestion des mouvements du joueur
    private void HandlePlayerMovement()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Déterminer la direction du mouvement selon la position de l'écran
            if (touchPos.x < 0)
            {
                rb.AddForce(Vector2.left * moveSpeed);
            }
            else
            {
                rb.AddForce(Vector2.right * moveSpeed);
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // Arrêter le mouvement si aucun clic n'est détecté
        }
    }

    // Gestion des collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            rb.velocity = Vector2.zero; // Stopper le mouvement du joueur

            // Jouer le son de collision s'il est assigné
            if (collisionSound != null)
            {
                collisionSound.Play();
            }

            // Afficher l'écran Game Over et marquer la fin du jeu
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }

            gameIsOver = true; // Mettre à jour l'état du jeu

            // Jouer le son de Game Over s'il est assigné
          

            // Arrêter le jeu via le GameManager
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.StopGame();
            }
        }
    }

    // Fonction pour redémarrer la scène
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
