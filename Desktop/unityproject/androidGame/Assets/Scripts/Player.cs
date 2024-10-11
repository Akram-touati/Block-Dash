using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Variables publiques pour personnaliser le comportement dans l'�diteur Unity
    [Header("Player Settings")]
    public float moveSpeed;

    // Composants
    private Rigidbody2D rb;

    [Header("UI Elements")]
    public GameObject gameOverUI;

    // Audio
    [Header("Audio Sources")]
    public AudioSource collisionSound; // Son de collision
   

    // Gestion de l'�tat du jeu
    private bool gameIsOver = false;

    // Initialisation des composants
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // S'assurer que les sons sont arr�t�s au d�but du jeu
        if (collisionSound != null)
        {
            collisionSound.Stop();
        }

       

        // Cacher l'�cran Game Over
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    // Gestion des entr�es utilisateur et des mouvements du joueur
    void Update()
    {
        if (gameIsOver)
        {
            // Si le jeu est termin�, appuyer sur l'�cran red�marre la sc�ne
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

            // D�terminer la direction du mouvement selon la position de l'�cran
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
            rb.velocity = Vector2.zero; // Arr�ter le mouvement si aucun clic n'est d�tect�
        }
    }

    // Gestion des collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            rb.velocity = Vector2.zero; // Stopper le mouvement du joueur

            // Jouer le son de collision s'il est assign�
            if (collisionSound != null)
            {
                collisionSound.Play();
            }

            // Afficher l'�cran Game Over et marquer la fin du jeu
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }

            gameIsOver = true; // Mettre � jour l'�tat du jeu

            // Jouer le son de Game Over s'il est assign�
          

            // Arr�ter le jeu via le GameManager
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.StopGame();
            }
        }
    }

    // Fonction pour red�marrer la sc�ne
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
