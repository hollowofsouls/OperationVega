
namespace Assets.Scripts.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Managers;

    using UnityEngine;

    /// <summary>
    /// The ai controller.
    /// </summary>
    public class AIController : MonoBehaviour
    {
        /// <summary>
        /// The spawnpoints reference.
        /// </summary>
        public List<GameObject> Spawnpoints;

        /// <summary>
        /// The enemy prefab reference.
        /// </summary>
        public GameObject EnemyPrefab;

        /// <summary>
        /// The enemies list reference.
        /// </summary>
        private List<Enemy> enemies;

        /// <summary>
        /// The ai controller instance.
        /// </summary>
        private static AIController aicontroller;

        /// <summary>
        /// Gets the instance of the ai controller.
        /// </summary>
        public static AIController Instance
        {
            get
            {
                aicontroller = aicontroller ?? FindObjectOfType(typeof(AIController)) as AIController;
                return aicontroller;
            }
        }

        private void Start()
        {
            Spawnpoints = GameObject.FindGameObjectsWithTag("Spawnpoint").ToList();
            this.enemies = new List<Enemy>();
        }

        /// <summary>
        /// The find closest target function.
        /// Finds the closest target to the enemy.
        /// </summary>
        private void FindClosestTarget()
        {
            
        }

        /// <summary>
        /// The set target function.
        /// Sets the target of the passed in enemy.
        /// </summary>
        /// <param name="e">
        /// The enemy to set the target for.
        /// </param>
        private void SetTarget(Enemy e)
        {
            
        }
    }
}
