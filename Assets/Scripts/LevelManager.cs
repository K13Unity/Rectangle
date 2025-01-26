using UnityEngine;
using System.Collections;
using Cube;

public class LevelManager : MonoBehaviour
{
    [SerializeField] SoundManager _soundManager; // Посилання на SoundManager
    [SerializeField] Level[] levels; // Масив рівнів
    [SerializeField] CubeController playerPrefab; // Префаб гравця
    [SerializeField] GameObject goalPrefab; // Префаб цілі (фінішу)
    [SerializeField] PlatformGenerator platformGenerator;

    private int currentLevelIndex = 0; // Індекс поточного рівня
    private GameObject currentLevelObject; // Поточний рівень на сцені

    void Start()
    {
        // Підписуємося на подію завершення генерації платформи
        platformGenerator.OnPlatformGenerationComplete += OnPlatformGenerationComplete;
        LoadLevel(currentLevelIndex); // Завантажуємо перший рівень
    }

    void LoadLevel(int levelIndex)
    {
        // Видаляємо попередній рівень, якщо він є
        if (currentLevelObject != null)
        {
            Destroy(currentLevelObject);
        }

        // Створюємо новий об'єкт для рівня
        currentLevelObject = new GameObject("Level " + levelIndex);
        Level level = levels[levelIndex];

        // Генеруємо платформу
        platformGenerator.GeneratePlatform(level.platformLayout);

        // Розміщуємо ціль
        Instantiate(goalPrefab, level.goalPosition, Quaternion.identity);

        // Прибираємо створення персонажа тут, воно буде в OnPlatformGenerationComplete
    }

    void OnPlatformGenerationComplete()
    {
        // Створюємо гравця вище стартової позиції
        Vector3 startPosition = levels[currentLevelIndex].startPosition;
        Vector3 spawnPosition = startPosition + Vector3.up * 10f; // Висота 10 одиниць вище стартової позиції
        CubeController player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        player.SetSoundManager(_soundManager);

        // Запускаємо корутин для падіння персонажа
        StartCoroutine(DropPlayer(player, startPosition));
    }

    IEnumerator DropPlayer(CubeController player, Vector3 targetPosition)
    {
        float startTime = Time.time;
        Vector3 startPosition = player.transform.position;
        float dropSpeed = 4f; // Швидкість падіння
        _soundManager.PlayRollSound();

        while (player.transform.position.y > targetPosition.y)
        {
            float progress = (Time.time - startTime) * dropSpeed;
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            yield return null;
        }
        // Після падіння встановлюємо точну позицію
        player.transform.position = targetPosition;
        // Оновлюємо точки контакту після падіння
        player.UpdateContactPoints();
        player.UnlockMovement();
    }

    public void NextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex < levels.Length)
        {
            LoadLevel(currentLevelIndex);
        }
        else
        {
            Debug.Log("Вітаємо! Ви завершили всі рівні!");
        }
    }
}