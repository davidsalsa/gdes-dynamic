using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// The speed of the tank
    /// </summary>
    public float speed = 1;

    public float maxPickupDistance;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        HandleMovement();
        HandleMouseClick();
    }

    /// <summary>
    /// Applies the movement to the tank
    /// </summary>
    private void HandleMovement()
    {
        var movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        movement.Normalize();

        transform.Translate(movement * (speed * Time.deltaTime));
    }

    private void HandleMouseClick()
    {
        if (!Input.GetMouseButtonDown(0) || Camera.main == default) return;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit) &&
            hit.collider.CompareTag("Equipable"))
        {
            if (Vector3.Distance(hit.transform.position, transform.position) <= maxPickupDistance)
            {
                EquipItem(hit.collider.gameObject);
            }
        }
    }

    private void EquipItem(GameObject equipableObject)
    {
        var localTransform = transform;
        equipableObject.transform.parent = localTransform;
        equipableObject.transform.position = localTransform.position;
        equipableObject.transform.Translate(new Vector3(0.5f, 0, 0.8f));
        equipableObject.transform.Rotate(70, 0, 0);
    }
}