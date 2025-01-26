using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
public class PlatformGenerator : MonoBehaviour
{
    public GameObject tilePrefab; // Префаб звичайної плитки
    public GameObject triggerTilePrefab; // Префаб невидимої плитки-тригера
    public float riseHeight = -10f; // Висота, з якої плитки починають рух
    [SerializeField] float riseSpeed = 2f; // Швидкість підняття плиток
    [SerializeField] float groupDelay = 0.5f; // Затримка між групами плиток
    public int tilesPerGroup = 3; // Кількість плиток у групі
    public event Action OnPlatformGenerationComplete; // Подія завершення генерації платформи

    List<int[,]> levels = new List<int[,]>();
    List<GameObject> tiles = new List<GameObject>();
    // Додайте подію завершення генерації платформи
    

    void Start()
    {
        levels.Add(platformLayout);
        levels.Add(platformLayout1);
        StartCoroutine(GeneratePlatform(levels[0])); // Використовуємо перший макет
    }

    public IEnumerator GeneratePlatform(int[,] layout)
    {
        int rows = layout.GetLength(0);
        int cols = layout.GetLength(1);

        // Створюємо списки позицій для звичайних і тригерних плиток
        List<Vector2Int> tilePositions = new List<Vector2Int>();
        List<Vector2Int> triggerTilePositions = new List<Vector2Int>();

        for (int x = 0; x < rows; x++)
        {
            for (int z = 0; z < cols; z++)
            {
                if (layout[x, z] == 1)
                {
                    tilePositions.Add(new Vector2Int(x, z)); // Звичайні плитки
                }
                else if (layout[x, z] == 2)
                {
                    triggerTilePositions.Add(new Vector2Int(x, z)); // Тригерні плитки
                }
            }
        }

        // Перемішуємо списки позицій
        Shuffle(tilePositions);
        Shuffle(triggerTilePositions);

        // Створюємо групи звичайних плиток
        for (int i = 0; i < tilePositions.Count; i += tilesPerGroup)
        {
            int tilesInThisGroup = Mathf.Min(tilesPerGroup, tilePositions.Count - i);

            for (int j = 0; j < tilesInThisGroup; j++)
            {
                Vector2Int pos = tilePositions[i + j];
                Vector3 startPosition = new Vector3(pos.x, riseHeight, pos.y);
                GameObject tile = Instantiate(tilePrefab, startPosition, Quaternion.identity, transform);
                tiles.Add(tile);

                StartCoroutine(RiseTile(tile, new Vector3(pos.x, 0f, pos.y)));
            }

            yield return new WaitForSeconds(groupDelay);
        }

        // Створюємо тригерні плитки
        foreach (Vector2Int pos in triggerTilePositions)
        {
            Vector3 startPosition = new Vector3(pos.x, riseHeight, pos.y);
            GameObject triggerTile = Instantiate(triggerTilePrefab, startPosition, Quaternion.identity, transform);
            tiles.Add(triggerTile);

            StartCoroutine(RiseTile(triggerTile, new Vector3(pos.x, 0f, pos.y)));
        }
        yield return new WaitUntil(() => AreAllTilesInPosition());

        // Викликаємо подію завершення генерації
        OnPlatformGenerationComplete?.Invoke();
    }

    IEnumerator RiseTile(GameObject tile, Vector3 targetPosition)
    {
        float startTime = Time.time;
        Vector3 startPosition = tile.transform.position;

        while (tile.transform.position != targetPosition)
        {
            float progress = (Time.time - startTime) * riseSpeed;
            tile.transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            yield return null;
        }
    }

    // Метод для перемішування списку
    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    bool AreAllTilesInPosition()
    {
        foreach (GameObject tile in tiles)
        {
            if (tile.transform.position.y != 0f) // Перевіряємо, чи плитка на місці
            {
                return false;
            }
        }
        return true;
    }

    // Приклади макетів
    private int[,] platformLayout = new int[,]
    {
        {0, 0, 0, 0, 2, 2, 2, 2, 0, 0},
        {0, 0, 0, 2, 0, 0, 0, 0, 0, 0},
        {0, 0, 2, 0, 0, 1, 1, 1, 0, 2},
        {0, 0, 2, 0, 1, 1, 1, 1, 0, 2},
        {0, 0, 2, 0, 1, 1, 1, 1, 0, 2},
        {0, 0, 2, 0, 1, 1, 1, 0, 0, 0},
        {0, 2, 0, 0, 1, 1, 1, 0, 2, 0},
        {0, 0, 0, 1, 1, 1, 1, 0, 2, 0},
        {2, 0, 1, 1, 1, 1, 1, 0, 2, 0},
        {2, 0, 1, 0, 1, 1, 0, 0, 0, 0},
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