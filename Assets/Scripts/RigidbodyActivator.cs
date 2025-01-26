using UnityEngine;

public class RigidbodyActivator : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private bool isFalling = false;

    private void Awake()
    {
        // Переконуємося, що Rigidbody спочатку вимкнено
        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = true; // Вимикаємо фізику
            _rigidbody.useGravity = false; // Гравітація вимкнена
        }
    }

    public void EnableRigidbody()
    {
        if  (_rigidbody != null && !isFalling)
        {
            isFalling = true;
            _rigidbody.isKinematic = false; // Вмикаємо фізику
            _rigidbody.useGravity = true;   // Вмикаємо гравітацію
            Physics.gravity = new Vector3(0, -50f, 0); 
            _rigidbody.AddForce(Vector3.down * 10f, ForceMode.VelocityChange);
        }
        
    }
}
