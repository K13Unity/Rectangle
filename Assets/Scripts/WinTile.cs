using UnityEngine;
using Cube;

public class WinTile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Win");
        CubeController _cubeController = other.GetComponentInParent<CubeController>();
        if (_cubeController != null)
        {
            _cubeController.EnableRigidbody();
        }
    }
}
