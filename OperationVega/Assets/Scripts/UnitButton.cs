
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
            this.Upgradepanel.SetActive(true);
            this.UpdateStatsPanel(this.Upgradepanel);

            Button[] buttons = this.Upgradepanel.GetComponentsInChildren<Button>();
            buttons[0].onClick.AddListener(delegate { this.Upgradepanel.SetActive(false); });
            buttons[1].onClick.AddListener(delegate { this.UpdateUnitStat(1); });
            buttons[2].onClick.AddListener(delegate { this.UpdateUnitStat(2); });
            buttons[3].onClick.AddListener(delegate { this.UpdateUnitStat(3); });
            buttons[4].onClick.AddListener(delegate { this.UpdateUnitStat(4); });
            buttons[5].onClick.AddListener(delegate { this.UpdateUnitStat(5); });
            buttons[6].onClick.AddListener(delegate { this.UpdateUnitStat(6); });
            buttons[7].onClick.AddListener(delegate { this.UpdateUnitStat(7); });
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

        private void UpdateUnitStat(int thebuttonindex)
        {
            // Just return if no points are available
            if (User.UpgradePoints <= 0)
            {
                return;
            }

            if (this.Unit.GetComponent<Harvester>())
            {
                Harvester h = this.Unit.GetComponent<Harvester>();

                switch (thebuttonindex)
                {
                    case 1:
                        // MaxHealth
                        h.Maxhealth += 20;
                        User.UpgradePoints--;
                        // Update UIText here
                        Debug.Log("Updated max health");
                        break;
                    case 2:
                        // Strength
                        h.Strength += 2;
                        User.UpgradePoints--;
                        // Update UIText here
                        Debug.Log("Updated strength");
                        break;
                    case 3:
                        // Defense
                        h.Defense += 2;
                        User.UpgradePoints--;
                        // Update UIText here
                        Debug.Log("Updated strength");
                        break;
                    case 4:
                        if (User.UpgradePoints >= 2)
                        {
                            // Speed
                            h.Speed++;
                            User.UpgradePoints -= 2;
                            // Update UIText here
                            Debug.Log("Updated speed");
                        }
                        break;
                    case 5:
                        if (User.UpgradePoints >= 4)
                        {
                            // Attack Speed
                            h.Attackspeed--;
                            User.UpgradePoints -= 4;
                            // Update UIText here
                            Debug.Log("Updated attack speed");
                        }
                        break;
                    case 6:
                        // SkillCooldown
                        h.Skillcooldown--;
                        User.UpgradePoints--;
                        // Update UIText here
                        Debug.Log("Updated skill cooldown");
                        break;
                    case 7:
                        if (User.UpgradePoints >= 4)
                        {   // Attack Range
                            h.Healrange++;
                            User.UpgradePoints -= 4;
                            // Update UIText here
                            Debug.Log("Updated attack range");
                        }
                        break;
                }
            }
            else if (this.Unit.GetComponent<Miner>())
            {
                Miner m = this.Unit.GetComponent<Miner>();

                switch (thebuttonindex)
                {
                    case 1:
                        // MaxHealth
                        m.Maxhealth += 20;
                        User.UpgradePoints--;
                        // Update UIText here
                        Debug.Log("Updated max health");
                        break;
                    case 2:
                        // Strength
                        m.Strength += 2;
                        User.UpgradePoints--;
                        // Update UIText here
                        Debug.Log("Updated strength");
                        break;
                    case 3:
                        // Defense
                        m.Defense += 2;
                        User.UpgradePoints--;
                        // Update UIText here
                        Debug.Log("Updated strength");
                        break;
                    case 4:
                        if (User.UpgradePoints >= 2)
                        {
                            // Speed
                            m.Speed++;
                            User.UpgradePoints -= 2;
                            // Update UIText here
                            Debug.Log("Updated speed");
                        }
                        break;
                    case 5:
                        if (User.UpgradePoints >= 4)
                        {
                            // Attack Speed
                            m.Attackspeed--;
                            User.UpgradePoints -= 4;
                            // Update UIText here
                            Debug.Log("Updated attack speed");
                        }
                        break;
                    case 6:
                        // SkillCooldown
                        m.Skillcooldown--;
                        User.UpgradePoints--;
                        // Update UIText here
                        Debug.Log("Updated skill cooldown");
                        break;
                    case 7:
                        if (User.UpgradePoints >= 4)
                        {   // Attack Range
                            m.Attackrange++;
                            User.UpgradePoints -= 4;
                            // Update UIText here
                            Debug.Log("Updated attack range");
                        }
                        break;
                }
            }
            else if (this.Unit.GetComponent<Extractor>())
            {
                Extractor e = this.Unit.GetComponent<Extractor>();

                switch (thebuttonindex)
                {
                    case 1:
                        // MaxHealth
                        e.Maxhealth += 20;
                        User.UpgradePoints--;
                        // Update UIText here
                        Debug.Log("Updated max health");
                        break;
                    case 2:
                        // Strength
                        e.Strength += 2;
                        User.UpgradePoints--;
                        // Update UIText here
                        Debug.Log("Updated strength");
                        break;
                    case 3:
                        // Defense
                        e.Defense += 2;
                        User.UpgradePoints--;
                        // Update UIText here
                        Debug.Log("Updated strength");
                        break;
                    case 4:
                        if (User.UpgradePoints >= 2)
                        {
                            // Speed
                            e.Speed++;
                            User.UpgradePoints -= 2;
                            // Update UIText here
                            Debug.Log("Updated speed");
                        }
                        break;
                    case 5:
                        if (User.UpgradePoints >= 4)
                        {
                            // Attack Speed
                            e.Attackspeed--;
                            User.UpgradePoints -= 4;
                            // Update UIText here
                            Debug.Log("Updated attack speed");
                        }
                        break;
                    case 6:
                        // SkillCooldown
                        e.Skillcooldown--;
                        User.UpgradePoints--;
                        // Update UIText here
                        Debug.Log("Updated skill cooldown");
                        break;
                    case 7:
                        if (User.UpgradePoints >= 4)
                        {   // Attack Range
                            e.Attackrange++;
                            User.UpgradePoints -= 4;
                            // Update UIText here
                            Debug.Log("Updated attack range");
                        }
                        break;
                }
            }
        }
    }
}
