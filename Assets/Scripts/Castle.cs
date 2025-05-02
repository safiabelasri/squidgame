using UnityEngine;

public class Castle : MonoBehaviour
{
    public float detectionRange = 0.5f; // Distance minimale pour bloquer le joueur

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < detectionRange)
            {
                StopPlayerMovement(player);
            }
        }
    }

    void StopPlayerMovement(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.BlockMovement();
            Debug.Log("🚧 Le joueur est bloqué par un Castle !");
        }
    }
}
