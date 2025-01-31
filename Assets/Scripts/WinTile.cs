using UnityEngine;
using Cube;
using System;

public class WinTile : MonoBehaviour
{
    public event Action OnPlayerWin;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Win");
        CubeController cubeController = other.GetComponentInParent<CubeController>();
        if (cubeController != null)
        {
            cubeController.EnableRigidbody();
            OnPlayerWin?.Invoke();
        }
    }
}
