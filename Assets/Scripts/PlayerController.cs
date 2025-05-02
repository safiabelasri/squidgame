using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Sprite idleSprite;
    public Sprite runningSprite;
    public Sprite deadSprite;

    private Vector3 lastPosition;
    private bool isDead = false;
    private bool isBlocked = false; // Empêche le mouvement si bloqué
    private SpriteRenderer spriteRenderer;
    private GuardianController guardian;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        guardian = FindFirstObjectByType<GuardianController>();
        lastPosition = transform.position; 
    }

    void Update()
    {
        if (isDead || isBlocked) return; // Si mort ou bloqué, ne pas bouger

        HandleMovement();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 newPosition = transform.position + new Vector3(moveX * moveSpeed * Time.deltaTime, moveY * moveSpeed * Time.deltaTime, 0);

        transform.position = newPosition;

        if (moveX != 0 || moveY != 0)
        {
            spriteRenderer.sprite = runningSprite;
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
        }

        if (guardian.IsLooking() && (moveX != 0 || moveY != 0))
        {
            Die();
        }
    }
// Ajoute cette variable en haut du script :

// Modifie BlockMovement :
public void BlockMovement()
{
    isBlocked = true;
    Debug.Log("🚧 Mouvement bloqué par un Castle !");
}

// Ajoute cette méthode pour débloquer le joueur :
public void UnblockMovement()
{
    isBlocked = false;
    moveSpeed = 5f; // Remet la vitesse normale
}
    void Die()
    {
        isDead = true;
        spriteRenderer.sprite = deadSprite;
        Debug.Log("💀 LOSER ! Tu as bougé au mauvais moment !");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Castle"))
        {
            isBlocked = true; // Bloque le joueur
            transform.position = lastPosition; // Remet à la dernière position valide
            Debug.Log("🚧 Le Castle bloque le passage !");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Castle"))
        {
            isBlocked = false; // Débloque le joueur
            lastPosition = transform.position; // Sauvegarde la nouvelle position
            Debug.Log("✅ Déblocage du joueur !");
        }
    }
    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; // Prend le niveau suivant
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Charge le prochain niveau
        }
        else
        {
            Debug.Log("🏆 Félicitations ! Tu as terminé le jeu !");
        }
    }
}
