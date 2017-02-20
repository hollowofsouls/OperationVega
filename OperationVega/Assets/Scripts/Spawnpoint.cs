
namespace Assets.Scripts
{
    using Interfaces;

    using UnityEngine;

    /// <summary>
    /// The spawn point class.
    /// It requires a rigidbody to perform collision detection.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Spawnpoint : MonoBehaviour
    {
        /// <summary>
        /// The spawn count reference.
        /// The number of enemies to spawn at a time.
        /// </summary>
        private int spawncount;

        /// <summary>
        /// The enemy prefab reference.
        /// </summary>
        public GameObject EnemyPrefab;

        /// <summary>
        /// The start function.
        /// It sets up the rigidbody characteristics.
        /// </summary>
        private void Start()
        {
            this.spawncount = 3;

            Rigidbody theRigidbody = this.GetComponent<Rigidbody>();

            theRigidbody = this.GetComponent<Rigidbody>();
            theRigidbody.useGravity = false;
            theRigidbody.isKinematic = true;
            theRigidbody.constraints = RigidbodyConstraints.FreezeAll;

            SphereCollider thesphereCollider = this.GetComponent<SphereCollider>();
            thesphereCollider.radius = 1.5f;
            thesphereCollider.isTrigger = true;
        }

        /// <summary>
        /// The on collision enter.
        /// </summary>
        /// <param name="other">
        /// The other collider that enters.
        /// </param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent(typeof(IUnit)))
            {
                for (int i = 0; i < this.spawncount; i++)
                {
                    float angle = i * (2 * 3.14159f / this.spawncount);
                    float x = Mathf.Cos(angle) * 1.5f;
                    float z = Mathf.Sin(angle) * 1.5f;

                    Vector3 spawnposition = new Vector3(this.transform.position.x + x, 0, this.transform.position.z + z);
                    // Spawn
                    Instantiate(this.EnemyPrefab, spawnposition, Quaternion.LookRotation(-other.transform.forward));
                }
            }
        }
    }
}
