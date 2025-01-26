using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource; // Посилання на AudioSource

    // Метод для відтворення звуку
    public void PlayRollSound()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play(); // Відтворюємо звук
        }
        else
        {
            Debug.LogWarning("AudioSource або AudioClip не налаштовані!");
        }
    }
}