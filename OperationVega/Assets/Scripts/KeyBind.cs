
namespace Assets.Scripts
{
    using System.Collections.Generic;
    using Managers;
    using UnityEngine;

    /// <summary>
    /// The key bind class.
    /// Handles the functionality of each bound key.
    /// </summary>
    public class KeyBind : MonoBehaviour
    {
        /// <summary>
        /// The key bind dictionary reference.
        /// The dictionary of bound keys.
        /// </summary>
        private readonly Dictionary<string, KeyCode> thekeybinddictionary = new Dictionary<string, KeyCode>();
        
        /// <summary>
        /// The start function.
        /// </summary>
        private void Start()
        {
            // The default keys and values
            this.thekeybinddictionary.Add("Settings", KeyCode.Escape);
            this.thekeybinddictionary.Add("Objectives", KeyCode.O);
            this.thekeybinddictionary.Add("Workshop", KeyCode.M);
            this.thekeybinddictionary.Add("ToggleCrafting", KeyCode.C);
            this.thekeybinddictionary.Add("ToggleActions", KeyCode.Tab);
            this.thekeybinddictionary.Add("CallHome", KeyCode.H);
            this.thekeybinddictionary.Add("SAExtractors", KeyCode.F1);
            this.thekeybinddictionary.Add("SAMiners", KeyCode.F2);
            this.thekeybinddictionary.Add("SAHarvesters", KeyCode.F3);
            this.thekeybinddictionary.Add("SAUnits", KeyCode.F4);
            this.thekeybinddictionary.Add("SpecialAbility", KeyCode.Space);
            this.thekeybinddictionary.Add("Save", KeyCode.F5);
            this.thekeybinddictionary.Add("+1Miner", KeyCode.Alpha2);
            this.thekeybinddictionary.Add("+1Extractor", KeyCode.Alpha1);
            this.thekeybinddictionary.Add("+1Harvester", KeyCode.Alpha3);
            this.thekeybinddictionary.Add("Yes", KeyCode.Y);
            this.thekeybinddictionary.Add("No", KeyCode.N);
            this.thekeybinddictionary.Add("ControlsMenu", KeyCode.Slash);

        }

        /// <summary>
        /// The update function.
        /// </summary>
        private void Update()
        {
            this.PerformHotKeyAction();
        }

        /// <summary>
        /// The perform hot key action function.
        /// Performs the appropriate action for the key pressed.
        /// </summary>
        private void PerformHotKeyAction()
        {
            if (Input.GetKeyDown(this.thekeybinddictionary["Settings"]))
            {
                EventManager.Publish("Settings");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["Objectives"]))
            {

            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["Workshop"]))
            {
                EventManager.Publish("Workshop");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["ToggleCrafting"]))
            {
                EventManager.Publish("Crafting");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["ToggleActions"]))
            {
                EventManager.Publish("Actions");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["CallHome"]))
            {
                EventManager.Publish("Recall");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["SAExtractors"]))
            {
                EventManager.Publish("SAExtract");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["SAMiners"]))
            {
                EventManager.Publish("SAMiner");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["SAHarvesters"]))
            {
                EventManager.Publish("SAHarvest");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["SAUnits"]))
            {
                EventManager.Publish("SAUnit");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["SpecialAbility"]))
            {
                EventManager.Publish("ActivateAbility");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["Save"]))
            {

            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["+1Miner"]))
            {
                EventManager.Publish("OnMChoice");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["+1Extractor"]))
            {
                EventManager.Publish("OnEChoice");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["+1Harvester"]))
            {
                EventManager.Publish("OnHChoice");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["Yes"]))
            {
                EventManager.Publish("Player choose yes");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["No"]))
            {
                EventManager.Publish("Player choose no");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["ControlsMenu"]))
            {
                EventManager.Publish("Customize");
            }
        }
    }
}
