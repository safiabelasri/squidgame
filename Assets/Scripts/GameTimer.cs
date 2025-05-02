using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 10;
    public Text timerText;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Temps restant: " + Mathf.Round(timeRemaining);
        }
        else
        {
            Debug.Log("🎉 Tu as gagné !");
        }
    }
}
