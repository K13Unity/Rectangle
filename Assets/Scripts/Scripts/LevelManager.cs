using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class LevelManager : MonoBehaviour
    {
        private SaveManager _saveManager;
        [SerializeField] private PlatformGenerator platformGenerator;

        private int _currentLevelIndex = 0; // Індекс поточного рівня
        private List<int[,]> levels = new List<int[,]>();

        private void Start()
        {
            _saveManager = new SaveManager();
            _currentLevelIndex = _saveManager.GetLevelIndex();
            levels.Add(_platformLayout);
            levels.Add(_platformLayout1);
            var platformData = levels[_currentLevelIndex];
            platformGenerator.GeneratePlatform(platformData);
            platformGenerator.OnTriggerTilleTreggered += OnTriggerTilleTriggeret;
            platformGenerator.OnWinTileTriggerTriggered += OnWinTileTriggerEnter;
            // Підписуємося на подію завершення генерації платформи
            // platformGenerator.OnPlatformGenerationComplete += OnPlatformGenerationComplete;
            // LoadLevel(currentLevelIndex); // Завантажуємо перший рівень
        }

        private void Update(){
            if(Input.GetKeyDown(KeyCode.R))
            {
                _saveManager.ResetProgress();
            }
        }

        private void OnTriggerTilleTriggeret()
        {
            StartCoroutine(RestartLevel());
        }
        private void OnWinTileTriggerEnter()
        {
            StartCoroutine(OnLevelComplete());
        }
        private IEnumerator OnLevelComplete()
        {
            yield return platformGenerator.DestroyPlatform();
            _currentLevelIndex++;
            _saveManager.SaveLevelIndex(_currentLevelIndex);
            platformGenerator.GeneratePlatform(levels[_currentLevelIndex]);
        }

        private IEnumerator RestartLevel()
        {
            yield return platformGenerator.DestroyPlatform();
            platformGenerator.GeneratePlatform(levels[_currentLevelIndex]);
        }

        
        private int[,] _platformLayout = new int[,]
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

        private int[,] _platformLayout1 = new int[,]
        {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
       
        };
    }
}