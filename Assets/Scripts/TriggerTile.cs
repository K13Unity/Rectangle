using UnityEngine;
using Cube;

public class TriggerTile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Losses");
        CubeController _cubeController = other.GetComponent<CubeController>();
        if (_cubeController != null)
        {
            _cubeController.EnableRigidbody(); 
            
        }
    }
}