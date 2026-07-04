using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [Header("Rotation Speed")]
    [SerializeField] private float minSpeed = 30f;
    [SerializeField] private float maxSpeed = 120f;

    private Vector3 rotationAxis;
    private float rotationSpeed;

    private void Start()
    {
        // Losowa oś obrotu
        rotationAxis = Random.onUnitSphere;

        // Losowa prędkość
        rotationSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
    }
}