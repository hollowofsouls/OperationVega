
namespace Assets.Scripts
{
    using System.Collections.Generic;
    using Managers;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// The key bind class.
    /// Handles the functionality of each bound key.
    /// </summary>
    public class KeyBind : MonoBehaviour
    {
        /// <summary>
        /// The timer reference.
        /// This will help stop the button from firing an event during the key change process.
        /// </summary>
        [HideInInspector]
        public float Timer;

        /// <summary>
        /// The current key to change.
        /// </summary>
        [HideInInspector]
        public GameObject CurrentKey;

        /// <summary>
        /// The instance reference.
        /// </summary>
        private static KeyBind instance;

        /// <summary>
        /// The key bind dictionary reference.
        /// The dictionary of bound keys.
        /// </summary>
        private readonly Dictionary<string, KeyCode> thekeybinddictionary = new Dictionary<string, KeyCode>();

        /// <summary>
        /// The sprite dictionary reference.
        /// </summary>
        private readonly Dictionary<KeyCode, Sprite> spritedictionary = new Dictionary<KeyCode, Sprite>();

        /// <summary>
        /// The possible keys reference.
        /// </summary>
        private readonly List<KeyCode> possiblekeys = new List<KeyCode>();

        /// <summary>
        /// The button images reference.
        /// </summary>
        [SerializeField]
        private List<Sprite> buttonimages = new List<Sprite>();

        /// <summary>
        /// Gets the instance of the KeyBind object.
        /// </summary>
        public static KeyBind Self
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// The hotkey change function.
        /// Changes the hotkey accordingly.
        /// </summary>
        public void HotkeyChange()
        {
            if (this.CurrentKey != null)
            {
                Event e = Event.current;

                // Check if its a key and if its a mappable key
                if (e.isKey && this.possiblekeys.Contains(e.keyCode))
                {
                    // Map key
                    this.thekeybinddictionary[this.CurrentKey.GetComponentInChildren<Text>().text] = e.keyCode;
                   
                    // Set sprite here
                    this.CurrentKey.GetComponent<Image>().sprite = this.spritedictionary[e.keyCode];
                    this.CurrentKey = null;

                    // Reset timer
                    this.Timer = 0.0f;
                }
            }
        }

        /// <summary>
        /// The restore to default function.
        /// Restores the keys back to default along with the appropriate sprite.
        /// <para></para>
        /// <remarks><paramref name="hotkeybuttons"></paramref> -The array of buttons to parse through and restore.</remarks>
        /// </summary>
        public void RestoreToDefault(Button[] hotkeybuttons)
        {
            this.thekeybinddictionary["Settings"] = KeyCode.Escape;
            this.thekeybinddictionary["Objectives"] = KeyCode.O;
            this.thekeybinddictionary["Crafting"] = KeyCode.C;
            this.thekeybinddictionary["Actions"] = KeyCode.Tab;
            this.thekeybinddictionary["Units"] = KeyCode.U;
            this.thekeybinddictionary["Workshop"] = KeyCode.M;
            this.thekeybinddictionary["Ability"] = KeyCode.Space;
            this.thekeybinddictionary["Home"] = KeyCode.H;
            this.thekeybinddictionary["Cancel"] = KeyCode.X;
            this.thekeybinddictionary["SAExtractors"] = KeyCode.F1;
            this.thekeybinddictionary["SAMiners"] = KeyCode.F2;
            this.thekeybinddictionary["SAHarvesters"] = KeyCode.F3;
            this.thekeybinddictionary["SAUnits"] = KeyCode.F4;
            this.thekeybinddictionary["BuyExtractor"] = KeyCode.Alpha1;
            this.thekeybinddictionary["BuyMiner"] = KeyCode.Alpha2;
            this.thekeybinddictionary["BuyHarvester"] = KeyCode.Alpha3;
            this.thekeybinddictionary["Controls"] = KeyCode.Slash;
            this.thekeybinddictionary["Save"] = KeyCode.F5;

            // There are 20 buttons but only the first 18 can be modified.
            // This excludes the x button and restore to default button.
            for (int i = 0; i < 18; i++)
            {
                string thetext = hotkeybuttons[i].GetComponentInChildren<Text>().text;
                hotkeybuttons[i].GetComponent<Image>().sprite = this.spritedictionary[this.thekeybinddictionary[thetext]];
            }
        }

        /// <summary>
        /// The start function.
        /// </summary>
        private void Start()
        {
            instance = this;

            this.SetUpDefaultKeys();
            this.SetUpPossibleKeys();
            this.SetUpSprites();

            this.Timer = 1.0f;
        }

        /// <summary>
        /// The update function.
        /// </summary>
        private void Update()
        {
            if (this.Timer > 1.0f)
            {
                this.PerformHotKeyAction();
            }

            this.Timer += 1 * Time.deltaTime;
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
            else if (Input.GetKeyDown(this.thekeybinddictionary["Crafting"]))
            {
                EventManager.Publish("Crafting");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["Actions"]))
            {
                EventManager.Publish("Actions");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["Units"]))
            {
                EventManager.Publish("UnitTab");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["Workshop"]))
            {
                EventManager.Publish("Workshop");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["Ability"]))
            {
                EventManager.Publish("ActivateAbility");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["Home"]))
            {
                EventManager.Publish("Recall");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["Cancel"]))
            {
                EventManager.Publish("CancelAction");
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
            else if (Input.GetKeyDown(this.thekeybinddictionary["BuyExtractor"]))
            {
                EventManager.Publish("OnEChoice");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["BuyMiner"]))
            {
                EventManager.Publish("OnMChoice");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["BuyHarvester"]))
            {
                EventManager.Publish("OnHChoice");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["Controls"]))
            {
                EventManager.Publish("Customize");
            }
            else if (Input.GetKeyDown(this.thekeybinddictionary["Save"]))
            {

            }
        }

        /// <summary>
        /// The set up default keys function.
        /// Sets up the default keys.
        /// </summary>
        private void SetUpDefaultKeys()
        {
            // The default keys and values
            this.thekeybinddictionary.Add("Settings", KeyCode.Escape);
            this.thekeybinddictionary.Add("Objectives", KeyCode.O);
            this.thekeybinddictionary.Add("Crafting", KeyCode.C);
            this.thekeybinddictionary.Add("Actions", KeyCode.Tab);
            this.thekeybinddictionary.Add("Units", KeyCode.U);
            this.thekeybinddictionary.Add("Workshop", KeyCode.M);
            this.thekeybinddictionary.Add("Ability", KeyCode.Space);
            this.thekeybinddictionary.Add("Home", KeyCode.H);
            this.thekeybinddictionary.Add("Cancel", KeyCode.X);
            this.thekeybinddictionary.Add("SAExtractors", KeyCode.F1);
            this.thekeybinddictionary.Add("SAMiners", KeyCode.F2);
            this.thekeybinddictionary.Add("SAHarvesters", KeyCode.F3);
            this.thekeybinddictionary.Add("SAUnits", KeyCode.F4);
            this.thekeybinddictionary.Add("BuyExtractor", KeyCode.Alpha1);
            this.thekeybinddictionary.Add("BuyMiner", KeyCode.Alpha2);
            this.thekeybinddictionary.Add("BuyHarvester", KeyCode.Alpha3);
            this.thekeybinddictionary.Add("Controls", KeyCode.Slash);
            this.thekeybinddictionary.Add("Save", KeyCode.F5);
        }

        /// <summary>
        /// The set up possible keys function.
        /// Adds the keys to the list that can be mapped.
        /// </summary>
        private void SetUpPossibleKeys()
        {
            // Possible keys for mapping - center keyboard
            this.possiblekeys.Add(KeyCode.Alpha0);
            this.possiblekeys.Add(KeyCode.Alpha1);
            this.possiblekeys.Add(KeyCode.Alpha2);
            this.possiblekeys.Add(KeyCode.Alpha3);
            this.possiblekeys.Add(KeyCode.Alpha4);
            this.possiblekeys.Add(KeyCode.Alpha5);
            this.possiblekeys.Add(KeyCode.Alpha6);
            this.possiblekeys.Add(KeyCode.Alpha7);
            this.possiblekeys.Add(KeyCode.Alpha8);
            this.possiblekeys.Add(KeyCode.Alpha9);
            this.possiblekeys.Add(KeyCode.F1);
            this.possiblekeys.Add(KeyCode.F2);
            this.possiblekeys.Add(KeyCode.F3);
            this.possiblekeys.Add(KeyCode.F4);
            this.possiblekeys.Add(KeyCode.F5);
            this.possiblekeys.Add(KeyCode.F6);
            this.possiblekeys.Add(KeyCode.F7);
            this.possiblekeys.Add(KeyCode.F8);
            this.possiblekeys.Add(KeyCode.B);
            this.possiblekeys.Add(KeyCode.C);
            this.possiblekeys.Add(KeyCode.E);
            this.possiblekeys.Add(KeyCode.F);
            this.possiblekeys.Add(KeyCode.G);
            this.possiblekeys.Add(KeyCode.H);
            this.possiblekeys.Add(KeyCode.I);
            this.possiblekeys.Add(KeyCode.J);
            this.possiblekeys.Add(KeyCode.K);
            this.possiblekeys.Add(KeyCode.L);
            this.possiblekeys.Add(KeyCode.M);
            this.possiblekeys.Add(KeyCode.N);
            this.possiblekeys.Add(KeyCode.O);
            this.possiblekeys.Add(KeyCode.P);
            this.possiblekeys.Add(KeyCode.Q);
            this.possiblekeys.Add(KeyCode.R);
            this.possiblekeys.Add(KeyCode.U);
            this.possiblekeys.Add(KeyCode.V);
            this.possiblekeys.Add(KeyCode.X);
            this.possiblekeys.Add(KeyCode.Z);
            this.possiblekeys.Add(KeyCode.KeypadEnter);
            this.possiblekeys.Add(KeyCode.Escape);
            this.possiblekeys.Add(KeyCode.Slash);
            this.possiblekeys.Add(KeyCode.Space);
            this.possiblekeys.Add(KeyCode.Tab);

            // Possible keys for mapping - right keypad
            this.possiblekeys.Add(KeyCode.Keypad0);
            this.possiblekeys.Add(KeyCode.Keypad1);
            this.possiblekeys.Add(KeyCode.Keypad2);
            this.possiblekeys.Add(KeyCode.Keypad3);
            this.possiblekeys.Add(KeyCode.Keypad4);
            this.possiblekeys.Add(KeyCode.Keypad5);
            this.possiblekeys.Add(KeyCode.Keypad6);
            this.possiblekeys.Add(KeyCode.Keypad7);
            this.possiblekeys.Add(KeyCode.Keypad8);
            this.possiblekeys.Add(KeyCode.Keypad9);
            this.possiblekeys.Add(KeyCode.KeypadDivide);
        }

        /// <summary>
        /// The set up sprites function.
        /// Sets up sprites in the dictionary to match the correct key.
        /// </summary>
        private void SetUpSprites()
        {
            int startingindex = 0;

            // I have the sprites set in the inspector at the appropriate index
            // to match the appropriate key
            for (int i = 0; i < 53; i++)
            {
                // Set Center Keyboard Sprites
                if (i < 43)
                {
                    startingindex = i;
                }
                else if (i == 43)
                {  // Set the Right Keypad Sprites
                    startingindex = 0;
                }

                this.spritedictionary.Add(this.possiblekeys[i], this.buttonimages[startingindex]);
                startingindex++;
            }

            // The slash key sprite is the same for the right keypad slash
            this.spritedictionary.Add(KeyCode.KeypadDivide, this.buttonimages[40]);
        }
    }
}
