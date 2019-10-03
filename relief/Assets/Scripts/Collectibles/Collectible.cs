using System;
using UnityEngine;

namespace Collectibles
{
    [RequireComponent(typeof(BoxCollider))]
    public class Collectible : MonoBehaviour
    {
        private BoxCollider _boxCollider;

        private void Start()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
