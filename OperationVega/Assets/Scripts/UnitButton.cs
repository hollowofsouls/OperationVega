
namespace Assets.Scripts
{
    using UI;

    using UnityEngine;
    using UnityEngine.AI;
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
        /// The unit stats reference.
        /// This will be a reference to the units stats.
        /// </summary>
        private Stats unitstats;

        /// <summary>
        /// The stats buttons reference.
        /// </summary>
        private Button[] statsbuttons;

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
            this.UpdateStatsPanel(UIManager.Self.tooltippanel);
            UIManager.Self.tooltippanel.SetActive(true);
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
            UIManager.Self.tooltippanel.SetActive(false);
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
            UIManager.Self.upgradepanel.SetActive(true);
            this.UpdateStatsPanel(UIManager.Self.upgradepanel);
            UIManager.Self.unit = this.Unit;
        }

        /// <summary>
        /// The start function.
        /// </summary>
        private void Start()
        {
            this.unitstats = this.Unit.GetComponent<Stats>();
            this.statsbuttons = UIManager.Self.upgradepanel.GetComponentsInChildren<Button>();
            this.statsbuttons[0].onClick.AddListener(delegate { UIManager.Self.upgradepanel.SetActive(false); });
            this.statsbuttons[1].onClick.AddListener(delegate { UIManager.Self.UpdateUnitStat(1); });
            this.statsbuttons[2].onClick.AddListener(delegate { UIManager.Self.UpdateUnitStat(2); });
            this.statsbuttons[3].onClick.AddListener(delegate { UIManager.Self.UpdateUnitStat(3); });
            this.statsbuttons[4].onClick.AddListener(delegate { UIManager.Self.UpdateUnitStat(4); });
            this.statsbuttons[5].onClick.AddListener(delegate { UIManager.Self.UpdateUnitStat(5); });
            this.statsbuttons[6].onClick.AddListener(delegate { UIManager.Self.UpdateUnitStat(6); });
            this.statsbuttons[7].onClick.AddListener(delegate { UIManager.Self.UpdateUnitStat(7); });

        }

        /// <summary>
        /// The update panel function.
        /// Updates the passed panel with the units information.
        /// <para></para>
        /// <remarks><paramref name="thepanel"></paramref> -The panel to update with the units information.</remarks>
        /// </summary>
        private void UpdateStatsPanel(GameObject thepanel)
        {
            Text[] theUIStats = thepanel.transform.GetComponentsInChildren<Text>();

            // Skip assigning index 0 because it the "Stats" text.
            theUIStats[1].text = "Health: " + this.unitstats.Health;
            theUIStats[2].text = "MaxHealth: " + this.unitstats.Maxhealth;
            theUIStats[3].text = "Strength: " + this.unitstats.Strength;
            theUIStats[4].text = "Defense: " + this.unitstats.Defense;
            theUIStats[5].text = "Speed: " + this.unitstats.Speed;
            theUIStats[6].text = "AttackSpeed: " + this.unitstats.Attackspeed;
            theUIStats[7].text = "SkillCooldown: " + this.unitstats.Skillcooldown;
            theUIStats[8].text = "AttackRange: " + this.unitstats.Attackrange;
            theUIStats[9].text = "ResourceCount: " + this.unitstats.Resourcecount;

            if (thepanel.name == UIManager.Self.upgradepanel.name)
            {
                theUIStats[10].text = "Upgrade Points Available: " + User.UpgradePoints;
            }
        }
    }
}
