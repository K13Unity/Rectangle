using UnityEngine;
using Cube;
using System;

public class TriggerTile : MonoBehaviour
{
    //створити подію
    public event Action OnPlayerEnter;


    private void OnTriggerEnter(Collider other)
    {
        CubeController _cubeController = other.GetComponent<CubeController>();
        if (_cubeController != null)
        {
            //викликати подію
            OnPlayerEnter?.Invoke();
            _cubeController.EnableRigidbody(); 
        }
    }
} // Трігер має івент -> платформ підписується і має сказати левел менеджеру шо він задестроївся