
namespace Assets.Scripts.Managers
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using Random = System.Random;

    /// <summary>
    /// The objective manager.
    /// </summary>
    public class ObjectiveManager : MonoBehaviour
    {
        /// <summary>
        /// The objective queue.
        /// This queues up each objective as its completed.
        /// </summary>
        public Queue<Objective> ObjectiveQueue = new Queue<Objective>();

        /// <summary>
        /// The objectives dictionary.
        /// </summary>
        public Dictionary<ObjectiveType, Objective> TheObjectives;

        /// <summary>
        /// The instance of the ObjectiveManager.
        /// </summary>
        private static ObjectiveManager instance;

        /// <summary>
        /// Gets the instance of the ObjectiveManager.
        /// </summary>
        public static ObjectiveManager Instance
        {
            get
            {
                // Set instance to the value of instance if instance is NOT null; otherwise,
                // if instance = null, set instance to new ObjectiveManager().
                instance = instance ?? FindObjectOfType(typeof(ObjectiveManager)) as ObjectiveManager;
                return instance;
            }
        }

        /// <summary>
        /// The start function.
        /// </summary>
        private void Start()
        {
            instance = this;
            EventManager.Subscribe("UpdateObjective", this.OnUpdateObjective);
            this.TheObjectives = new Dictionary<ObjectiveType, Objective>();

            Objective mainObjective = new Objective("Built Ship: ", ObjectiveType.Main, false, 0, 1);
            Objective killObjective = new Objective("Kill Enemies: ", ObjectiveType.Kill, false, 0, 3);
            Objective craftObjective = new Objective("Craft Items: ", ObjectiveType.Craft, false, 0, 3);

            this.TheObjectives.Add(ObjectiveType.Main, mainObjective);
            this.TheObjectives.Add(ObjectiveType.Kill, killObjective);
            this.TheObjectives.Add(ObjectiveType.Craft, craftObjective);

            User.UpgradePoints = 8;
        }

      /// <summary>
        /// The on destroy function.
        /// </summary>
        private void OnDestroy()
        {
            EventManager.UnSubscribe("UpdateObjective", this.OnUpdateObjective);
        }

        /// <summary>
        /// The on update objective function.
        /// Function called by the event manager to activate the UpdateObjective function.
        /// </summary>
        private void OnUpdateObjective()
        {
            this.StartCoroutine(this.UpdateObjective());
        }

        /// <summary>
        /// The update objective function.
        /// This function is used to notify the user an objective has been completed then sets a new one.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        private IEnumerator UpdateObjective()
        {
            Debug.Log("Objective complete...");
            yield return new WaitForSeconds(2);
            Debug.Log("Objective updating...");
            Objective obj = this.ObjectiveQueue.Dequeue();

            this.UpgradePointDisbursement(obj);
            Debug.Log(User.UpgradePoints);

            if (obj.Type != ObjectiveType.Main)
            {
                Random random = new Random();
                int randomNumber = random.Next(1, 5);

                obj.Maxvalue += randomNumber;

                if (obj.Maxvalue > 50)
                {
                    int newnumber = random.Next(10, 20);
                    obj.Maxvalue = newnumber;
                }

                obj.Currentvalue = 0;
                obj.IsCompleted = false;
            }
        }

        /// <summary>
        /// The upgrade point disbursement function.
        /// This function handles the distribution of the correct number of upgrade points to the user.
        /// <para></para>
        /// <remarks><paramref name="obj"></paramref> -The objective to check for point disbursement.</remarks>
        /// </summary>
        private void UpgradePointDisbursement(Objective obj)
        {
            switch (obj.Type)
            {
                case ObjectiveType.Main:
                    User.UpgradePoints += 50;
                    break;
                case ObjectiveType.Kill:
                    User.UpgradePoints += (obj.Maxvalue >= 30 && obj.Maxvalue % 5 == 0) ? 5 : 2;
                    break;
                case ObjectiveType.Craft:
                     User.UpgradePoints += (obj.Maxvalue % 2 == 0) ? 2 : 1;
                    break;
            }
        }
    }
}
