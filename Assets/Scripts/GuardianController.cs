using UnityEngine;
using System.Collections;

public class GuardianController : MonoBehaviour
{
    public float rotationTime = 3.0f;  // Temps total pour tourner
    private bool looking = false;
    private bool isRotating = false;

    void Start()
    {
        InvokeRepeating("StartRotation", rotationTime, rotationTime);
    }

    void StartRotation()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateGuardian());
        }
    }

    IEnumerator RotateGuardian()
    {
        isRotating = true;
        float duration = 0.5f; // Temps de rotation
        float elapsed = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = looking ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        looking = !looking;
        isRotating = false;
    }

    public bool IsLooking() 
    { 
        return !isRotating && looking; // Ne regarde que si la rotation est finie
    }
}
