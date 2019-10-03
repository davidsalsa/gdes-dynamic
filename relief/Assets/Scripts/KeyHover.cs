using UnityEngine;

public class KeyHover : MonoBehaviour
{
    public float hoverFrequency;
    public float hoverAmplitude;
    public float rotationSpeed;
    void Update()
    {
        // Apply hover effect
        var tempPos = transform.position;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * hoverFrequency) * hoverAmplitude;
        transform.position = tempPos;
        
        // Apply rotating effect
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * rotationSpeed);
    }
}
