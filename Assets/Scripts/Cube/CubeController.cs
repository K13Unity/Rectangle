using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Cube
{
    public class CubeController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        public List<Transform> points; 
        private List<Transform> contactPoints = new List<Transform>(); 
        private SoundManager _soundManager;
        public float rotationAngle = 90f;
        private bool isRolling = false;
        private bool isDropping = true;
        private bool isFalling = false;
        public float rollSpeed = 200f;


        void Start()
        {
            if (_rigidbody != null)
            {
                DisableRigidbody(); 
            }
            UpdateContactPoints();
            
        }

        void Update()
        {
            if(isRolling || isDropping) return;

            
            if (Input.GetKeyDown(KeyCode.A)) // Клавіша для обертання вліво
            {
                Transform nearestPoint = FindContactPoint(Vector3.left);
                StartCoroutine(Roll(nearestPoint, Vector3.left)); // Обертання вліво
            }

            if (Input.GetKeyDown(KeyCode.D)) // Клавіша для обертання вправо
            {
                Transform nearestPoint = FindContactPoint(Vector3.right);
                StartCoroutine(Roll(nearestPoint, Vector3.right)); // Обертання вправо
            }

            if (Input.GetKeyDown(KeyCode.W)) // Клавіша для обертання вперед
            {
                Transform nearestPoint = FindContactPoint(Vector3.forward);
                StartCoroutine(Roll(nearestPoint, Vector3.forward)); // Обертання вперед
            }

            if (Input.GetKeyDown(KeyCode.S)) // Клавіша для обертання назад
            {
                Transform nearestPoint = FindContactPoint(Vector3.back);
                StartCoroutine(Roll(nearestPoint, Vector3.back)); // Обертання назад
            }
        }

        public void UnlockMovement()
        {
            isDropping = false;
        }

        public void UpdateContactPoints()
        {
            contactPoints.Clear();
            foreach (var point in points)
            {
                if (point.position.y > -0.1f && point.position.y < 0.1f)
                {
                    contactPoints.Add(point);
                    point.gameObject.SetActive(true);
                }
                else
                {
                    point.gameObject.SetActive(false);
                }
            }
        }

        Transform FindContactPoint(Vector3 direction)
        {
            Transform bestPoint = null;
            float maxDot = -Mathf.Infinity;

            foreach (var point in contactPoints)
            {
                Vector3 toPoint = (point.position - transform.position).normalized;
                float dot = Vector3.Dot(toPoint, direction);

                if (dot > maxDot)
                {
                    maxDot = dot;
                    bestPoint = point;
                }
            }

            return bestPoint;
        }

        private IEnumerator Roll(Transform pivot, Vector3 direction)
        {
             if (isFalling) yield break;
            isRolling = true;
            
            float angle = 0f;
            float targetAngle = 90f;
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction).normalized;

            if (_soundManager != null && !isFalling)
            {
                _soundManager.PlayRollSound(); // Відтворюємо звук тільки якщо персонаж не падає
            }


            while (angle < targetAngle)
            {
                float step = rollSpeed * Time.deltaTime;
                angle += step;

                if (angle > targetAngle)
                    step -= angle - targetAngle;

                transform.RotateAround(pivot.position, rotationAxis, step);
                yield return null;
            }
            
            isRolling = false;
            UpdateContactPoints();
        }

        public void SetSoundManager(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }

         public void EnableRigidbody()
        {
            if  (_rigidbody != null && !isFalling)
            {
                isFalling = true;
                _rigidbody.isKinematic = false; 
                _rigidbody.useGravity = true; 
                Physics.gravity = new Vector3(0, -40f, 0); 
                _rigidbody.AddForce(Vector3.down * 15f, ForceMode.VelocityChange);
            }
        }

        public void DisableRigidbody()
        {
            if (_rigidbody != null)
            {
                isFalling = false;
                _rigidbody.isKinematic = true; 
                _rigidbody.useGravity = false; 
            }
        }
    }
}