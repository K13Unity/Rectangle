using UnityEngine;

public class RigidbodyActivator : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbodyComponent;
    private bool _isFalling = false;

    private void Awake()
    {
        // Переконуємося, що Rigidbody спочатку вимкнено
        if (rigidbodyComponent != null)
        {
            rigidbodyComponent.isKinematic = true; // Вимикаємо фізику
            rigidbodyComponent.useGravity = false; // Гравітація вимкнена
        }
    }

    public void EnableRigidbody()
    {
        if  (rigidbodyComponent != null && !_isFalling)
        {
            _isFalling = true;
            rigidbodyComponent.isKinematic = false; // Вмикаємо фізику
            rigidbodyComponent.useGravity = true;   // Вмикаємо гравітацію
            Physics.gravity = new Vector3(0, -50f, 0); 
            rigidbodyComponent.AddForce(Vector3.down * 10f, ForceMode.VelocityChange);
        }
    }
}
