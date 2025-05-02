using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class SceneTransition : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string sceneToLoad = "level2"; // Scène cible
    [SerializeField] private float transitionDelay = 1.5f;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip transitionSound;
    private AudioSource audioSource;

    [Header("Visual Effects")]
    [SerializeField] private Animator fadeAnimator; // Animator avec les triggers "Start" et "End"

    [Header("Optional")]
    [SerializeField] private GameObject playerToDisable;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && transitionSound != null)
        {
            Debug.LogWarning("Aucun AudioSource trouvé, mais un son de transition est défini.");
        }

        if (fadeAnimator == null)
        {
            Debug.LogWarning("Aucun Animator de transition défini.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadSceneCoroutine());
        }
    }

    private IEnumerator LoadSceneCoroutine()
    {
        if (playerToDisable != null)
            playerToDisable.SetActive(false); // Désactive le joueur si précisé

        if (fadeAnimator != null)
            fadeAnimator.SetTrigger("Start"); // Début de l'animation

        if (audioSource != null && transitionSound != null)
            audioSource.PlayOneShot(transitionSound);

        yield return new WaitForSeconds(transitionDelay); // Attente

        SceneManager.LoadScene(sceneToLoad); // Changement de scène
    }
}
