using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Si le joueur entre en collision, on bloque son mouvement
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.BlockMovement();
                Debug.Log("🚧 La barrière bloque le passage !");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Si le joueur sort de la collision, on lui permet de se déplacer à nouveau
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.UnblockMovement();
                Debug.Log("✅ Le passage est libre !");
            }
        }
    }
}
