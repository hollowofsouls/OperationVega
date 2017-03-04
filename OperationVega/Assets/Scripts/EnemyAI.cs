
namespace Assets.Scripts
{
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.AI;

    /// <summary>
    /// The enemy ai class.
    /// Handles enemy ai behavior.
    /// </summary>
    public class EnemyAI : MonoBehaviour
    {
        /// <summary>
        /// The enemy reference.
        /// Reference to this enemy.
        /// </summary>
        private Enemy enemyreference;

        /// <summary>
        /// The my navigation agent reference.
        /// Reference to the navigation agent.
        /// </summary>
        private NavMeshAgent mynavagent;

        /// <summary>
        /// The units hit reference.
        /// Reference to all units found when casting overlap sphere.
        /// </summary>
        private Collider[] unitshit;

        /// <summary>
        /// The check rate reference.
        /// Reference to how long between casting the overlap sphere.
        /// </summary>
        private float checkrate;

        /// <summary>
        /// The next check reference.
        /// Reference to the next check with sphere.
        /// </summary>
        private float nextcheck;

        /// <summary>
        /// The radius reference.
        /// Reference to how big the radius of the overlap sphere is.
        /// </summary>
        [HideInInspector]
        public float Radius = 5.0f;

        /// <summary>
        /// The start.
        /// </summary>
        private void Start()
        {
            this.enemyreference = this.GetComponent<Enemy>();
            this.mynavagent = this.GetComponent<NavMeshAgent>();
            this.mynavagent.stoppingDistance = 1.5f;
            this.checkrate = Random.Range(0.5f, 1.0f);
        }

        /// <summary>
        /// The update.
        /// </summary>
        private void Update()
        {
            this.CheckForUnits();
        }

        /// <summary>
        /// The check for units function.
        /// Checks for units in range to attack.
        /// </summary>
        private void CheckForUnits()
        {
            if (Time.time > this.nextcheck)
            {
                this.nextcheck = Time.time + this.checkrate;
                this.unitshit = Physics.OverlapSphere(this.transform.position, this.Radius, 1 << LayerMask.NameToLayer("Unit"));

                if (this.unitshit.Length > 1)
                {
                    List<GameObject> listforsorting = new List<GameObject>();

                    foreach (Collider c in this.unitshit)
                    {
                        listforsorting.Add(c.transform.gameObject);
                    }

                    listforsorting.Sort(
                        delegate(GameObject a, GameObject b)
                            {
                                float distanceA = Vector3.Distance(a.transform.position, this.transform.position);
                                float distanceB = Vector3.Distance(b.transform.position, this.transform.position);

                                if (distanceA > distanceB) return 1;
                                if (distanceA < distanceB) return -1;

                                return 0;
                            });

                    this.enemyreference.Currenttarget = listforsorting[0];
                    this.mynavagent.SetDestination(listforsorting[0].transform.position);
                    this.enemyreference.ChangeStates("Battle");
                }
                else if (this.unitshit.Length == 1)
                {
                    this.enemyreference.Currenttarget = this.unitshit[0].gameObject;
                    this.mynavagent.SetDestination(this.unitshit[0].transform.position);
                    this.enemyreference.ChangeStates("Battle");
                }
            }
        }
    }
}
