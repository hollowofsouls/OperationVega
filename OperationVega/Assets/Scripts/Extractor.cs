
namespace Assets.Scripts
{
    using System.Collections;

    using Controllers;
    using Interfaces;
    using UnityEngine;

    /// <summary>
    /// The extractor class.
    /// </summary>
    public class Extractor : MonoBehaviour, IUnit, ICombat, IGather, IDamageable
    {
        /// <summary>
        /// The target to attack.
        /// </summary>
        [HideInInspector]
        public IDamageable Target;

        /// <summary>
        /// The enemy gameobject reference.
        /// </summary>
        public GameObject theEnemy;

        /// <summary>
        /// The resource to taint.
        /// </summary>
        [HideInInspector]
        public IResources TargetResource;

        /// <summary>
        /// The target click position to move to.
        /// </summary>
        [HideInInspector]
        public Vector3 TargetClickPosition;

        /// <summary>
        /// The target direction to move at.
        /// </summary>
        [HideInInspector]
        public Vector3 TargetDirection;

        /// <summary>
        /// The health of the extractor.
        /// </summary>
        [HideInInspector]
        public uint Health;

        /// <summary>
        /// The max health of the extractor.
        /// </summary>
        [HideInInspector]
        public uint Maxhealth;

        /// <summary>
        /// The strength of the extractor.
        /// </summary>
        [HideInInspector]
        public uint Strength;

        /// <summary>
        /// The defense of the extractor.
        /// </summary>
        [HideInInspector]
        public uint Defense;

        /// <summary>
        /// The speed of the extractor.
        /// </summary>
        [HideInInspector]
        public uint Speed;

        /// <summary>
        /// The attack speed of the extractor.
        /// </summary>
        [HideInInspector]
        public uint Attackspeed;

        /// <summary>
        /// The skill cool down of the extractor.
        /// </summary>
        [HideInInspector]
        public uint Skillcooldown;

        /// <summary>
        /// The attack range of the extractor.
        /// </summary>
        [HideInInspector]
        public float Attackrange;

        /// <summary>
        /// The resource count of the extractor.
        /// </summary>
        [HideInInspector]
        public uint Resourcecount;

        /// <summary>
        /// The extractor finite state machine.
        /// Used to keep track of the extractors states.
        /// </summary>
        public FiniteStateMachine<string> TheExtractorFsm = new FiniteStateMachine<string>();

        /// <summary>
        /// The time between attacks reference.
        /// Stores the reference to the timer between attacks
        /// </summary>
        private float timebetweenattacks;

        /// <summary>
        /// Instance of the RangeHandler delegate.
        /// Called in changing to the idle state.
        /// </summary>
        private RangeHandler idleHandler;

        /// <summary>
        /// Instance of the RangeHandler delegate.
        /// Called in changing to the battle state.
        /// </summary>
        private RangeHandler battleHandler;

        /// <summary>
        /// Instance of the RangeHandler delegate.
        /// Called in changing to the harvest state.
        /// </summary>
        private RangeHandler harvestHandler;

        /// <summary>
        /// The range handler delegate.
        /// The delegate handles setting the attack range upon changing state.
        /// <para></para>
        /// <remarks><paramref name="number"></paramref> -The number to set the attack range to.</remarks>
        /// </summary>
        private delegate void RangeHandler(float number);

        /// <summary>
        /// The move function providing movement functionality.
        /// </summary>
        public void Move()
        {
            if (Vector3.Magnitude(this.transform.position - this.TargetClickPosition) > this.Attackrange)
            {
                this.transform.position += this.TargetDirection * 2 * Time.deltaTime;
            }
        }

        /// <summary>
        /// The harvest function provides functionality of the extractor to harvest a resource.
        /// </summary>
        public void Harvest()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The decontaminate function provides functionality of the extractor to decontaminate a resource.
        /// </summary>
        public void Decontaminate()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The attack function gives the extractor functionality to attack.
        /// </summary>
        public void Attack()
        {
            if (this.timebetweenattacks >= this.Attackspeed)
            {
                Vector3 thedisplacement = (this.transform.position - this.theEnemy.transform.position).normalized;
                if (Vector3.Dot(thedisplacement, this.theEnemy.transform.forward) < 0)
                {
                    Debug.Log("Extractor crit hit!");
                    this.Target.TakeDamage(10);
                    Enemy e = this.Target as Enemy;
                    Debug.Log(e.Health);
                    this.timebetweenattacks = 0;
                }
                else
                {
                    Debug.Log("Extractor Attacked for normal damage");
                    this.Target.TakeDamage(5);
                    Enemy e = this.Target as Enemy;
                    Debug.Log(e.Health);
                    this.timebetweenattacks = 0;
                }
            }
        }

        /// <summary>
        /// The take damage function allows an extractor to take damage.
        /// </summary>
        /// <param name="damage">
        /// The amount of damage.
        /// </param>
        public void TakeDamage(uint damage)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The fear factor ability for the extractor.
        /// </summary>
        public void FearFactor()
        {
            
        }

        /// <summary>
        /// The set the target position function.
        /// </summary>
        /// <param name="targetPos">
        /// The target position to go to when clicked.
        /// </param>
        public void SetTheTargetPosition(Vector3 targetPos)
        {
            this.TargetClickPosition = targetPos;
            this.TargetDirection = (this.TargetClickPosition - this.transform.position).normalized;

        }

        /// <summary>
        /// The change states function.
        /// This function changes the state to the passed in state.
        /// <para></para>
        /// <remarks><paramref name="destinationState"></paramref> -The state to transition to.</remarks>
        /// </summary>
        public void ChangeStates(string destinationState)
        {
            string thecurrentstate = this.TheExtractorFsm.CurrentState.Statename;

            switch (destinationState)
            {
                case "Battle":
                    this.TheExtractorFsm.Feed(thecurrentstate + "To" + destinationState, 5.0f);
                    break;
                case "Idle":
                    this.TheExtractorFsm.Feed(thecurrentstate + "To" + destinationState, 0.1f);
                    break;
                case "Harvest":
                    this.TheExtractorFsm.Feed(thecurrentstate + "To" + destinationState, 1.0f);
                    break;
            }
        }

        /// <summary>
        /// The initialize unit function.
        /// This will initialize the unit with the appropriate values for stats.
        /// </summary>
        private void InitUnit()
        {
            this.Attackrange = 0.1f;
            this.Attackspeed = 3;
            this.timebetweenattacks = this.Attackspeed;

            Debug.Log("Extractor Initialized");
        }

        /// <summary>
        /// The reset range function.
        /// This resets the range of distance the unit stands from the clicked position.
        /// </summary>
        /// <param name="num">
        /// The number to set the attack range to.
        /// </param>
        private void ResetRange(float num)
        {
            this.Attackrange = num;
        }

        /// <summary>
        /// The battle state function.
        /// The function called while in the battle state.
        /// </summary>
        private void BattleState()
        {
            this.Move();
            if (this.Target != null)
            {
                float distance = Vector3.Magnitude(this.transform.position - this.TargetClickPosition);

                if (distance <= this.Attackrange)
                {
                    this.Attack();
                }
            }
        }

        /// <summary>
        /// The harvest state function.
        /// The function called while in the harvest state.
        /// </summary>
        private void HarvestState()
        {
                this.Move();
                Debug.Log("Extractor found gas");
        }

        /// <summary>
        /// The awake function.
        /// </summary>
        private void Awake()
        {
            this.idleHandler = this.ResetRange;
            this.battleHandler = this.ResetRange;
            this.harvestHandler = this.ResetRange;

            this.TheExtractorFsm.CreateState("Init", null);
            this.TheExtractorFsm.CreateState("Idle", this.idleHandler);
            this.TheExtractorFsm.CreateState("Battle", this.battleHandler);
            this.TheExtractorFsm.CreateState("Harvest", this.harvestHandler);

            this.TheExtractorFsm.AddTransition("Init", "Idle", "auto");
            this.TheExtractorFsm.AddTransition("Idle", "Battle", "IdleToBattle");
            this.TheExtractorFsm.AddTransition("Battle", "Idle", "BattleToIdle");
            this.TheExtractorFsm.AddTransition("Idle", "Harvest", "IdleToHarvest");
            this.TheExtractorFsm.AddTransition("Harvest", "Idle", "HarvestToIdle");
            this.TheExtractorFsm.AddTransition("Battle", "Harvest", "BattleToHarvest");
            this.TheExtractorFsm.AddTransition("Harvest", "Battle", "HarvestToBattle");
        }

        /// <summary>
        /// The start function.
        /// </summary>
        private void Start()
        {
            this.InitUnit();
            this.TheExtractorFsm.Feed("auto", 0.1f);
        }

        /// <summary>
        /// The update function.
        /// </summary>
        private void Update()
        {
            UnitController.Self.CheckIfSelected(this.gameObject);
            this.UpdateUnit();
        }

        /// <summary>
        /// The update unit function.
        /// This updates the units behavior.
        /// </summary>
        private void UpdateUnit()
        {
            this.timebetweenattacks += 1 * Time.deltaTime;

            switch (this.TheExtractorFsm.CurrentState.Statename)
            {
                case "Idle":
                    this.Move();
                    break;
                case "Battle":
                    this.BattleState();
                    break;
                case "Harvest":
                    this.HarvestState();
                    break;
                default:
                    break;
            }
        }
    }
}
