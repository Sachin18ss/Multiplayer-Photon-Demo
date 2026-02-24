using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float _rotationSpeed = 50f;

    private void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }
}
