
namespace Assets.Scripts
{
    using Interfaces;

    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>
    /// The unit button class.
    /// Handles functionality of the UnitButtons.
    /// </summary>
    public class UnitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        /// <summary>
        /// The unit reference.
        /// Reference to the specific units data to be tied into this button.
        /// </summary>
        [HideInInspector]
        public GameObject Unit;

        /// <summary>
        /// The stats panel reference.
        /// This is the tooltip panel that will toggle on and off.
        /// </summary>
        [HideInInspector]
        public GameObject Tooltippanel;

        /// <summary>
        /// The upgrade panel reference.
        /// This is the upgrade panel that will open up.
        /// </summary>
        [HideInInspector]
        public GameObject Upgradepanel;

        /// <summary>
        /// The on pointer enter function.
        /// </summary>
        /// <param name="eventData">
        /// The event data.
        /// </param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            GameObject selectionsquare = this.Unit.transform.FindChild("SelectionHighlight").gameObject;
            selectionsquare.GetComponent<MeshRenderer>().material.color = Color.red;
            this.UpdateStatsPanel(this.Tooltippanel);
            this.Tooltippanel.SetActive(true);
        }

        /// <summary>
        /// The on pointer exit function.
        /// </summary>
        /// <param name="eventData">
        /// The event data.
        /// </param>
        public void OnPointerExit(PointerEventData eventData)
        {
            GameObject selectionsquare = this.Unit.transform.FindChild("SelectionHighlight").gameObject;
            selectionsquare.GetComponent<MeshRenderer>().material.color = Color.black;
            this.Tooltippanel.SetActive(false);
        }

        /// <summary>
        /// The on pointer click function.
        /// This will open the upgrade panel. Allowing for Upgrades.
        /// </summary>
        /// <param name="eventData">
        /// The event data.
        /// </param>
        public void OnPointerClick(PointerEventData eventData)
        {
            // Pause the game - to be implemented here
            this.UpdateStatsPanel(this.Upgradepanel);
            this.Upgradepanel.SetActive(true);
        }


        /// <summary>
        /// The update panel function.
        /// Updates the passed panel with the units information.
        /// </summary>
        /// <param name="thepanel">
        /// The panel to update with the units information.
        /// </param>
        private void UpdateStatsPanel(GameObject thepanel)
        {
            IUnit u = (IUnit)this.Unit.GetComponent(typeof(IUnit));
            int[] unitstats = u.GetAllStats();

            Text[] theUIStats = thepanel.transform.GetComponentsInChildren<Text>();

            // Skip assigning index 0 because it the "Stats" text.
            theUIStats[1].text = "Health: " + unitstats[0];
            theUIStats[2].text = "MaxHealth: " + unitstats[1];
            theUIStats[3].text = "Strength: " + unitstats[2];
            theUIStats[4].text = "Defense: " + unitstats[3];
            theUIStats[5].text = "Speed: " + unitstats[4];
            theUIStats[6].text = "AttackSpeed: " + unitstats[5];
            theUIStats[7].text = "SkillCooldown: " + unitstats[6];
            theUIStats[8].text = "AttackRange: " + unitstats[7];
            theUIStats[9].text = "ResourceCount: " + unitstats[8];

            if (thepanel.name == this.Upgradepanel.name)
            {
                theUIStats[10].text = "Upgrade Points Available: " + User.UpgradePoints;
            }
        }
     }
}
