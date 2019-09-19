using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    /// <summary>
    /// The player to follow
    /// </summary>
    public GameObject player;

    /// <summary>
    /// The standard offset
    /// </summary>
    private Vector3 _offset;

    /// <inheritdoc cref="MonoBehaviour"/>
    private void Start()
    {
        _offset = transform.position - player.transform.position;
    }

    /// <inheritdoc cref="MonoBehaviour"/>
    private void LateUpdate()
    {
        transform.position = player.transform.position + _offset;
    }
}