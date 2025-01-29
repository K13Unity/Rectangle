using UnityEngine;
using System.Collections;
using Cube;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] PlatformGenerator platformGenerator;

    private int currentLevelIndex = 0; // Індекс поточного рівня
    List<int[,]> levels = new List<int[,]>();

    void Start()
    {
        levels.Add(platformLayout);
        levels.Add(platformLayout1);
        var platformData = levels[currentLevelIndex];
        platformGenerator.GeneratePlatform(platformData);
        // Підписуємося на подію завершення генерації платформи
        // platformGenerator.OnPlatformGenerationComplete += OnPlatformGenerationComplete;
        // LoadLevel(currentLevelIndex); // Завантажуємо перший рівень
    }

    // void LoadLevel(int levelIndex)
    // {
    //     // Видаляємо попередній рівень, якщо він є
    //     if (currentLevelObject != null)
    //     {
    //         Destroy(currentLevelObject);
    //     }

    //     // Створюємо новий об'єкт для рівня
    //     currentLevelObject = new GameObject("Level " + levelIndex);
    //     LevelManager level = levels[levelIndex];

    //     // Генеруємо платформу
    //     platformGenerator.GeneratePlatform(level.platformLayout);

    //     // Розміщуємо ціль
    //     Instantiate(goalPrefab, level.goalPosition, Quaternion.identity);

    //     // Прибираємо створення персонажа тут, воно буде в OnPlatformGenerationComplete
    // }

    // void OnPlatformGenerationComplete()
    // {
    //     // Створюємо гравця вище стартової позиції
    //     Vector3 startPosition = levels[currentLevelIndex].startPosition;
    //     Vector3 spawnPosition = startPosition + Vector3.up * 10f; // Висота 10 одиниць вище стартової позиції
    //     CubeController player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
    //     player.SetSoundManager(_soundManager);

    //     // Запускаємо корутин для падіння персонажа
    //     StartCoroutine(DropPlayer(player, startPosition));
    // }

    // IEnumerator DropPlayer(CubeController player, Vector3 targetPosition)
    // {
    //     float startTime = Time.time;
    //     Vector3 startPosition = player.transform.position;
    //     float dropSpeed = 4f; // Швидкість падіння
    //     _soundManager.PlayRollSound();

    //     while (player.transform.position.y > targetPosition.y)
    //     {
    //         float progress = (Time.time - startTime) * dropSpeed;
    //         player.transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
    //         yield return null;
    //     }
    //     // Після падіння встановлюємо точну позицію
    //     player.transform.position = targetPosition;
    //     // Оновлюємо точки контакту після падіння
    //     player.UpdateContactPoints();
    //     player.UnlockMovement();
    // }

    // public void NextLevel()
    // {
    //     currentLevelIndex++;
    //     if (currentLevelIndex < levels.Length)
    //     {
    //         LoadLevel(currentLevelIndex);
    //     }
    //     else
    //     {
    //         Debug.Log("Вітаємо! Ви завершили всі рівні!");
    //     }
    // }
     private int[,] platformLayout = new int[,]
    {
        {0, 0, 0, 0, 2, 2, 2, 2, 0, 0},
        {0, 0, 0, 2, 0, 0, 0, 0, 0, 0},
        {0, 0, 2, 0, 0, 1, 1, 1, 0, 2},
        {0, 0, 2, 0, 1, 1, 3, 1, 0, 2},
        {0, 0, 2, 0, 1, 1, 1, 1, 0, 2},
        {0, 0, 2, 0, 1, 1, 1, 0, 0, 0},
        {0, 2, 0, 0, 1, 1, 1, 0, 2, 0},
        {0, 0, 0, 1, 1, 1, 1, 0, 2, 0},
        {2, 0, 1, 1, 1, 1, 1, 0, 2, 0},
        {2, 0, 1, 4, 1, 1, 0, 0, 0, 0},
        {2, 0, 1, 1, 1, 1, 0, 2, 0, 0},
        {0, 0, 0, 1, 1, 0, 0, 0, 0, 0},
        {0, 2, 0, 0, 0, 0, 2, 0, 0, 0},
        {0, 0, 0, 2, 2, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    };

    private int[,] platformLayout1 = new int[,]
    {
        {2, 2, 2, 2, 2, 2, 2, 2 },
        {2, 2, 2, 2, 2, 2, 2, 2 },
        {2, 2, 2, 2, 2, 2, 2, 2 },
        {2, 2, 2, 2, 2, 2, 2, 2 },
        {2, 2, 2, 2, 2, 2, 2, 2 },
        {2, 2, 2, 2, 2, 2, 2, 2 },
        {2, 2, 2, 2, 2, 2, 2, 2 },
        {2, 2, 2, 2, 2, 2, 2, 2 },
        {2, 2, 2, 2, 2, 2, 2, 2 },
        {2, 2, 2, 2, 2, 2, 2, 2 },
        {2, 2, 2, 2, 2, 2, 2, 2 },
        {2, 2, 2, 2, 2, 2, 2, 2 },
        {2, 2, 2, 2, 2, 2, 2, 2 },
        {2, 2, 2, 2, 2, 2, 2, 2 }
        
    };
}