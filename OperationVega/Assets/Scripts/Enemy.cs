
namespace Assets.Scripts
{
    using System.Collections.Generic;
    using System.Linq;

    using Assets.Scripts.Managers;

    using Controllers;
    using Interfaces;
    using UnityEngine;
    using UnityEngine.AI;

    /// <summary>
    /// The enemy class.
    /// </summary>
    [RequireComponent(typeof(MeshCollider))]
    public class Enemy : MonoBehaviour, ICombat
    {
        /// <summary>
        /// The enemy finite state machine.
        /// Used to keep track of the enemy states.
        /// </summary>
        private readonly FiniteStateMachine<string> theEnemyFSM = new FiniteStateMachine<string>();

        /// <summary>
        /// The current target reference.
        /// </summary>
        private GameObject currenttarget;

        /// <summary>
        /// The target to attack.
        /// </summary>
        private ICombat target;

        /// <summary>
        /// The resource to taint.
        /// </summary>
        private IResources targetResource;

        /// <summary>
        /// The my stats reference.
        /// This reference will contain all of this enemy stats data.
        /// </summary>
        private Stats mystats;

        /// <summary>
        /// The time to taint reference.
        /// </summary>
        private float timetotaint;

        /// <summary>
        /// The targets list.
        /// All the possible targets on the map.
        /// </summary>
        private List<GameObject> theTargets = new List<GameObject>();

        /// <summary>
        /// The time between attacks reference.
        /// Stores the reference to the timer between attacks.
        /// </summary>
        private float timebetweenattacks;

        /// <summary>
        /// The navigation agent reference.
        /// </summary>
        private NavMeshAgent navagent;

        /// <summary>
        /// Instance of the Handler delegate.
        /// Called in changing to the idle state.
        /// </summary>
        private Handler idleHandler;

        /// <summary>
        /// The range handler delegate.
        /// The delegate handles setting the stopping distance upon changing state.
        /// </summary>
        private delegate void Handler();

        /// <summary>
        /// The attack function gives the enemy functionality to attack.
        /// </summary>
        public void Attack()
        {
            if (this.timebetweenattacks >= this.mystats.Attackspeed && Vector3.Distance(this.currenttarget.transform.position, this.transform.position) <= this.mystats.Attackrange)
            {
                Debug.Log("Enemy hit!");
                this.target.TakeDamage(5);
                this.timebetweenattacks = 0;
            }

            if (Vector3.Distance(this.currenttarget.transform.position, this.transform.position) > this.mystats.Attackrange)
            {
                Debug.Log("Unit out of range");
                this.target = null;
                this.theEnemyFSM.Feed(this.theEnemyFSM.CurrentState.Statename + "ToIdle");
            }
        }

        /// <summary>
        /// The take damage function allows an enemy to take damage.
        /// <para></para>
        /// <remarks><paramref name="damage"></paramref> -The amount to be calculated when the object takes damage.</remarks>
        /// </summary>
        public void TakeDamage(int damage)
        {
            Debug.Log("Enemy took damage");
            this.mystats.Health -= damage;
        }

        /// <summary>
        /// The taint function allows the enemy to taint a resource.
        /// </summary>
        public void Taint()
        {
            this.timetotaint += 1 * Time.deltaTime;

            if (this.timetotaint >= 3)
            {
                Debug.Log("I Tainted it");
                this.targetResource.Taint = true;
                this.targetResource = null;
                this.theEnemyFSM.Feed(this.theEnemyFSM.CurrentState.Statename + "ToIdle");
                this.timetotaint = 0;
            }
        }

        /// <summary>
        /// The awake function.
        /// </summary>
        private void Awake()
        {
            this.idleHandler = this.Search;
            this.theEnemyFSM.CreateState("Init", null);
            this.theEnemyFSM.CreateState("Idle", this.idleHandler);
            this.theEnemyFSM.CreateState("Battle", null);
            this.theEnemyFSM.CreateState("TaintResource", null);

            this.theEnemyFSM.AddTransition("Init", "Idle", "auto");
            this.theEnemyFSM.AddTransition("Idle", "Battle", "IdleToBattle");
            this.theEnemyFSM.AddTransition("Battle", "Idle", "BattleToIdle");
            this.theEnemyFSM.AddTransition("Idle", "TaintResource", "IdleToTaintResource");
            this.theEnemyFSM.AddTransition("TaintResource", "Idle", "TaintResourceToIdle");
            this.theEnemyFSM.AddTransition("Battle", "TaintResource", "BattleToTaintResource");
            this.theEnemyFSM.AddTransition("TaintResource", "Battle", "TaintResourceToBattle");
        }

        /// <summary>
        /// The start function
        /// </summary>
        private void Start()
        {
            if (!this.GetComponent<Stats>())
            {
                this.gameObject.AddComponent<Stats>();
            }

            this.mystats = this.GetComponent<Stats>();
            this.mystats.Health = 100;
            this.mystats.Maxhealth = 100;
            this.mystats.Strength = 3;
            this.mystats.Defense = 2;
            this.mystats.Speed = 2;
            this.mystats.Attackspeed = 2;
            this.mystats.Skillcooldown = 20;
            this.mystats.Attackrange = 1.5f;

            this.timetotaint = 0;

            this.timebetweenattacks = this.mystats.Attackspeed;
            this.navagent = this.GetComponent<NavMeshAgent>();
            this.navagent.updateRotation = false;
            this.navagent.speed = this.mystats.Speed;

            MeshCollider mc = this.GetComponent<MeshCollider>();
            mc.sharedMesh = this.GetComponentsInChildren<MeshFilter>()[1].mesh;
            this.theEnemyFSM.Feed("auto");
        }

        /// <summary>
        /// The search function.
        /// This function is called upon switching to the idle state.
        /// This function finds all the objects in the scene that can be targeted.
        /// </summary>
        private void Search()
        {
            this.theTargets = GameObject.FindGameObjectsWithTag("Targetable").ToList();
        }

        /// <summary>
        /// The idle state function.
        /// Has the functionality of checking for closest targets.
        /// </summary>
        private void IdleState()
        {
            if (this.SortTargets())
            {
                this.FindClosestTarget();
            }
        }

        /// <summary>
        /// The battle state function.
        /// The function called while in the battle state.
        /// </summary>
        private void BattleState()
        {
            if (this.target != null)
            {
                if (this.navagent.remainingDistance <= this.mystats.Attackrange && !this.navagent.pathPending)
                {
                    this.Attack();
                }
            }
        }

        /// <summary>
        /// The Taint Resource state function.
        /// The function called while in the taint resource state.
        /// </summary>
        private void TaintResourceState()
        {
            if (this.targetResource != null && !this.targetResource.Taint)
            {
                if (this.navagent.remainingDistance <= this.mystats.Attackrange && !this.navagent.pathPending)
                {
                    this.Taint();
                }
            }
            else
            {
                this.targetResource = null;
                this.theEnemyFSM.Feed(this.theEnemyFSM.CurrentState.Statename + "ToIdle");
            }
        }

        /// <summary>
        /// The find closest target function.
        /// Finds the closest target to the enemy.
        /// </summary>
        private void FindClosestTarget()
        {
            if (this.currenttarget.GetComponent(typeof(IUnit)))
            {
                this.target = (ICombat)this.currenttarget.GetComponent(typeof(ICombat));
                this.navagent.stoppingDistance = 1.5f;
                this.navagent.SetDestination(this.currenttarget.transform.position);
                this.theEnemyFSM.Feed(this.theEnemyFSM.CurrentState.Statename + "ToBattle");
            }
            else if (this.currenttarget.GetComponent(typeof(IResources)))
            {
                this.targetResource = (IResources)this.currenttarget.GetComponent(typeof(IResources));
                this.navagent.stoppingDistance = 1.0f;
                this.navagent.SetDestination(this.currenttarget.transform.position);
                this.theEnemyFSM.Feed(this.theEnemyFSM.CurrentState.Statename + "ToTaintResource");
            }
        }

        /// <summary>
        /// The update enemy function.
        /// This updates the enemy behavior.
        /// </summary>
        private void UpdateEnemy()
        {
            this.timebetweenattacks += 1 * Time.deltaTime;

            switch (this.theEnemyFSM.CurrentState.Statename)
            {
                case "Idle":
                    this.IdleState();
                    break;
                case "Battle":
                    this.BattleState();
                    break;
                case "TaintResource":
                    this.TaintResourceState();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// The update rotation function.
        /// This function updates the enemy rotation accordingly.
        /// </summary>
        private void UpdateRotation()
        {
            Vector3 dir = this.currenttarget.transform.position - this.transform.position;
            Quaternion lookrotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookrotation, Time.deltaTime * 5).eulerAngles;
            this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        /// <summary>
        /// The sort targets function.
        /// Sorts the list until able to find a unit to attack or resource to taint.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool SortTargets()
        {
            this.theTargets.Sort(delegate(GameObject a, GameObject b)
            {
                float distanceA = Vector3.Distance(a.transform.position, this.transform.position);
                float distanceB = Vector3.Distance(b.transform.position, this.transform.position);

                if (distanceA > distanceB)
                    return 1;
                if (distanceA < distanceB)
                    return -1;

                return 0;
            });

            if (this.theTargets[0].GetComponent(typeof(IResources)))
            {
                this.targetResource = (IResources)this.theTargets[0].GetComponent(typeof(IResources));
                
                if (this.targetResource.Taint)
                {
                    this.theTargets.Remove(this.theTargets[0]);
                    return false;
                }
            }
            this.currenttarget = this.theTargets[0];
            
            // Return true if its a unit or an untainted resource
            return true;
        }

        /// <summary>
        /// The update function.
        /// </summary>
        private void Update()
        {
            this.UpdateEnemy();
            this.UpdateRotation();
        }

        /// <summary>
        /// The on destroy function.
        /// </summary>
        private void OnDestroy()
        {
           ObjectiveManager.Instance.TheObjectives[ObjectiveType.Kill].Currentvalue++;
        }
    }
}