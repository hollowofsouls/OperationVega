
namespace Assets.Scripts
{
    using UnityEngine;

    /// <summary>
    /// The spawn point class.
    /// It requires a rigidbody to perform collision detection.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Spawnpoint : MonoBehaviour
    {
        /// <summary>
        /// The the rigid body.
        /// </summary>
        private Rigidbody theRigidbody;

        /// <summary>
        /// The start function.
        /// It sets up the rigidbody characteristics.
        /// </summary>
        private void Start()
        {
            this.theRigidbody = this.GetComponent<Rigidbody>();
            this.theRigidbody.useGravity = false;
            this.theRigidbody.isKinematic = true;
            this.theRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        /// <summary>
        /// The on collision enter.
        /// </summary>
        /// <param name="other">
        /// The other collider that enters.
        /// </param>
        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent(typeof(IGather)))
            {
                // Spawn
            }
        }
    }
}
