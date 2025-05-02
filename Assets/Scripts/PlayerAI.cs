using UnityEngine;
using UnityEngine.UI; // Pour changer l'image en cas de Game Over

public class PlayerAI : MonoBehaviour
{
    public float moveSpeed = 2f; // Vitesse du joueur
    public Sprite gameOverSprite; // Image à afficher en cas de Game Over
    private GuardianController guardian;
    private bool canMove = true;
    private int direction = 1; // Direction initiale (1 = droite, -1 = gauche, 0 = immobile)
    private float reactionTime; // Temps avant que le joueur s'arrête (certains sont plus lents)

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        guardian = FindAnyObjectByType<GuardianController>(); // Trouve le Guardian dans la scène
        spriteRenderer = GetComponent<SpriteRenderer>(); // Récupère le SpriteRenderer

        reactionTime = Random.Range(0.1f, 1f); // Certains réagissent vite (0.1s), d’autres plus lentement (1s)
        InvokeRepeating("ChangeDirection", 2f, Random.Range(1f, 3f)); // Change la direction toutes les 1-3 sec
    }

    void Update()
    {
        if (!canMove) return; // Ne pas bouger si mort

        // Vérifie si le joueur doit s’arrêter
        if (guardian.IsLooking())
        {
            Invoke("StopMoving", reactionTime); // Arrêt progressif après reactionTime
        }
        else
        {
            transform.position += new Vector3(moveSpeed * direction * Time.deltaTime, 0, 0);
        }
    }

    void ChangeDirection()
    {
        direction = Random.Range(-1, 2); // -1 = gauche, 1 = droite, 0 = stop
    }

    void StopMoving()
    {
        if (guardian.IsLooking() && direction != 0) // Si bouge encore => Game Over
        {
            GameOver();
        }
        else
        {
            direction = 0; // Arrête le mouvement
        }
    }

    void GameOver()
    {
        canMove = false;
        direction = 0; // Arrête le joueur
        spriteRenderer.sprite = gameOverSprite; // Change l’image en "Game Over"
        Debug.Log(gameObject.name + " 💀 Game Over ! Il a bougé au mauvais moment !");
    }
}
