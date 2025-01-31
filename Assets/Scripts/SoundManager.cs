using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource; // Посилання на AudioSource

    // Метод для відтворення звуку
    public void PlayRollSound()
    {
        if (audioSource && audioSource.clip)
        {
            audioSource.Play(); // Відтворюємо звук
        }
    }
}