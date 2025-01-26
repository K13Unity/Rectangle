// using UnityEngine;

// public class GameController : MonoBehaviour
// {
//     public static GameController Instance; // Синглтон для зручного доступу

//     public enum GameState
//     {
//         MainMenu, // Головне меню
//         Playing,  // Гра активна
//         Paused,   // Гра на паузі
//         Win,      // Гравець виграв
//         Lose      // Гравець програв
//     }

//     public GameState currentState = GameState.MainMenu; // Поточний стан гри
//     public LevelManager levelManager; // Посилання на менеджер рівнів

//     void Awake()
//     {
//         // Реалізація синглтона
//         if (Instance == null)
//         {
//             Instance = this;
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     void Start()
//     {
//         StartGame(); // Запуск гри (можна змінити на виклик з головного меню)
//     }

//     void Update()
//     {
//         // Перевірка стану гри
//         switch (currentState)
//         {
//             case GameState.Playing:
//                 CheckWinCondition();
//                 CheckLoseCondition();
//                 break;
//             case GameState.Win:
//                 // Логіка для перемоги
//                 break;
//             case GameState.Lose:
//                 // Логіка для програшу
//                 break;
//         }
//     }

//     public void StartGame()
//     {
//         currentState = GameState.Playing;
//         levelManager.LoadLevel(0); // Завантажуємо перший рівень
//     }

//     public void RestartLevel()
//     {
//         levelManager.LoadLevel(levelManager.currentLevelIndex); // Перезавантажуємо поточний рівень
//         currentState = GameState.Playing;
//     }

//     public void NextLevel()
//     {
//         levelManager.NextLevel(); // Перехід на наступний рівень
//         currentState = GameState.Playing;
//     }

//     public void PauseGame()
//     {
//         currentState = GameState.Paused;
//         Time.timeScale = 0; // Зупиняємо час у грі
//     }

//     public void ResumeGame()
//     {
//         currentState = GameState.Playing;
//         Time.timeScale = 1; // Відновлюємо час у грі
//     }

//     void CheckWinCondition()
//     {
//         // Перевірка умови перемоги (наприклад, гравець досяг цілі)
//         if (/* умова перемоги */)
//         {
//             currentState = GameState.Win;
//             Debug.Log("Ви виграли!");
//         }
//     }

//     void CheckLoseCondition()
//     {
//         // Перевірка умови програшу (наприклад, гравець впав у прірву)
//         if (/* умова програшу */)
//         {
//             currentState = GameState.Lose;
//             Debug.Log("Ви програли!");
//         }
//     }
// }