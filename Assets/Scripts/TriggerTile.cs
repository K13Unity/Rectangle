using UnityEngine;
using Cube;
using System;

public class TriggerTile : MonoBehaviour
{
    public event Action OnPlayerEnter;


    private void OnTriggerEnter(Collider other)
    {
        var cubeController = other.GetComponent<CubeController>();
        if (cubeController == null) return;
        OnPlayerEnter?.Invoke();
        cubeController.EnableRigidbody();
    }
} 