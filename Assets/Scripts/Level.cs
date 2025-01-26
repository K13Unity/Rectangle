using UnityEngine;

[System.Serializable]
public class Level
{
    public int[,] platformLayout = new int[8, 13]; // Приклад ініціалізації масиву 5x5
    public Vector3 startPosition = new Vector3(1, 0, 4);
    public Vector3 goalPosition = new Vector3(7, -1.1f, 1);
}