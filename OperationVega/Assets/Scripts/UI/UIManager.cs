using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Text = UnityEngine.UI.Text;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Assets.Scripts.Managers;
using Assets.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Interfaces;



namespace UI
{


    public class UIManager : MonoBehaviour
    {
        #region -- VARIABLES --
        [SerializeField]
        private Button m_NewGame;
        [SerializeField]
        private Button m_LoadGame;
        [SerializeField]
        private Button m_QuitGame;
        [SerializeField]
        private Button m_Instructions;
        [SerializeField]
        private Button m_Options;
        [SerializeField]
        private Button m_Workshop;
        [SerializeField]
        private Button m_Craft;
        [SerializeField]
        private Button m_Clear;
        [SerializeField]
        private Button m_CancelAction;
        [SerializeField]
        private Button m_Minerals;
        [SerializeField]
        private Button m_Food;
        [SerializeField]
        private Button m_CookedFood;
        [SerializeField]
        private Button m_Gas;
        [SerializeField]
        private Button m_Fuel;
        [SerializeField]
        private Canvas m_BackgroundUI;

        [SerializeField]
        private RectTransform m_ActionsTAB;
        [SerializeField]
        private RectTransform m_CraftingTAB;
        [SerializeField]
        private RectTransform m_UnitTAB;
        [SerializeField]
        private RectTransform m_WorkshopUI;
        [SerializeField]
        private RectTransform m_OptionsUI;
        [SerializeField]
        private RectTransform m_ThrusterChoice;
        [SerializeField]
        private RectTransform m_CockpitChoice;
        [SerializeField]
        private RectTransform m_WingChoice;
        [SerializeField]
        private RectTransform m_SettingsUI;
        [SerializeField]
        private RectTransform m_CustomizeUI;
        [SerializeField]
        private RectTransform m_ObjectiveUI;
        [SerializeField]
        private RectTransform m_MainUI;
        [SerializeField]
        private RectTransform m_AreyousureUI;

        [SerializeField]
        private Text m_MineralsT;
        [SerializeField]
        private Text m_FoodT;
        [SerializeField]
        private Text m_CookedFoodT;
        [SerializeField]
        private Text m_GasT;
        [SerializeField]
        private Text m_FuelT;
        [SerializeField]
        private Text m_SteelT;
        [SerializeField]
        private Text m_KillT;
        [SerializeField]
        private Text m_CraftT;
        [SerializeField]
        private Text m_MainT;

        [HideInInspector]
        public GameObject foodinstance;
        [SerializeField]
        private GameObject cookedfoodPrefab;
        [SerializeField]
        public GameObject upgradepanel;
        [SerializeField]
        public GameObject tooltippanel;
        [SerializeField]
        private GameObject unitbutton;

        [SerializeField]
        public Image Input1;
        [SerializeField]
        public Image Input2;
        [SerializeField]
        public Image Output;
        [SerializeField]
        public Image minerals;
        [SerializeField]
        public Image food;
        [SerializeField]
        public Image cookedFood;
        [SerializeField]
        public Image Steel;
        [SerializeField]
        public Image fuel;
        [SerializeField]
        public Image gas;


        [SerializeField]
        private Transform contentfield;

        private readonly List<GameObject> theUnitButtonsList = new List<GameObject>();

        private static UIManager instance;


        public static UIManager Self
        {
            get
            {
                return instance;
            }
        }

        bool revertactionstab;
        bool revertcraftingtab;
        bool revertunittab;
        bool undo1;
        bool undo2;
        bool undo3;
        private float Scalefactor;



        #endregion

        #region -- PROPERTIES --
        public Canvas BackgroundUI
        {
            get { return m_BackgroundUI; }
        }
        #endregion

        protected void Awake()
        {
            //Bool use to manage action tab
            revertactionstab = true;
            //Bool use to manage crafting tab
            revertcraftingtab = true;
            //Bool use to manage unit tab
            revertunittab = true;

            instance = this;

            undo1 = true;
            undo2 = true;
            undo3 = true;
            ScaleFactor();

            #region -- Ingame Subscribers --
            EventManager.Subscribe("Rally", this.OnRally);
            EventManager.Subscribe("Harvest", this.OnHarvest);
            EventManager.Subscribe("Recall", this.OnRecall);
            EventManager.Subscribe("CancelAction", this.OnCancelAction);
            EventManager.Subscribe("Close WorkShop", this.CloseWorkShop);
            EventManager.Subscribe("Workshop", this.OnWorkShop);
            EventManager.Subscribe("Craft", this.OnCraft);
            EventManager.Subscribe("Clear", this.OnClear);
            EventManager.Subscribe("Mine", this.OnMine);
            EventManager.Subscribe("Extract", this.OnExtract);
            EventManager.Subscribe("Actions", this.OnActions);
            EventManager.Subscribe("Crafting", this.OnCrafting);
            EventManager.Subscribe("UnitTab", this.OnUnit);
            EventManager.Subscribe("Build Rocket", this.OnBuild);
            EventManager.Subscribe("Thrusters", this.OnThrusters);
            EventManager.Subscribe("Apply Chassis", this.OnChassis);
            EventManager.Subscribe("Cockpit", this.OnCockpit);
            EventManager.Subscribe("Apply Wings", this.OnWings);
            EventManager.Subscribe("OnMChoice", this.OnMChoice);
            EventManager.Subscribe("OnHChoice", this.OnHChoice);
            EventManager.Subscribe("OnEChoice", this.OnEChoice);
            EventManager.Subscribe("Player choose yes", this.OnYes);
            EventManager.Subscribe("Player choose no", this.OnNo);
            EventManager.Subscribe("SAExtract", this.OnSAExtract);
            EventManager.Subscribe("SAMiner", this.OnSAMiner);
            EventManager.Subscribe("SAUnit", this.OnSAUnit);
            EventManager.Subscribe("SAHarvest", this.OnSAHarvest);
            #endregion

            #region -- Main Menu Subscribers --
            EventManager.Subscribe("NewGame", this.NewGame);
            EventManager.Subscribe("Options Menu", this.OnOptions);
            EventManager.Subscribe("Instructions", this.OnInstructions);
            EventManager.Subscribe("QuitGame", this.OnQuitGame);
            EventManager.Subscribe("Close Options", this.CloseOptions);
            EventManager.Subscribe("Settings", this.OnSettings);
            EventManager.Subscribe("SettingsClose", this.OnSettingsClose);
            EventManager.Subscribe("Customize", this.OnCustomize);
            EventManager.Subscribe("CustomizeClose", this.OnCustomizeClose);
            #endregion

            #region -- Crafting Subscribers --
            EventManager.Subscribe("Minerals", this.OnMinerals);
            EventManager.Subscribe("Food", this.Food);
            EventManager.Subscribe("CookedFood", this.OnCookedFood);
            EventManager.Subscribe("Gas", this.Gas);
            EventManager.Subscribe("Fuel", this.Fuel);
            #endregion



        }

        protected void OnDestroy()
        {
            #region -- Ingame Unsubscribers --
            EventManager.UnSubscribe("Rally", this.OnRally);
            EventManager.UnSubscribe("Harvest", this.OnHarvest);
            EventManager.UnSubscribe("Recall", this.OnRecall);
            EventManager.UnSubscribe("CancelAction", this.OnCancelAction);
            EventManager.UnSubscribe("Workshop", this.OnWorkShop);
            EventManager.UnSubscribe("Close WorkShop", this.CloseWorkShop);
            EventManager.UnSubscribe("Craft", this.OnCraft);
            EventManager.UnSubscribe("Clear", this.OnClear);
            EventManager.UnSubscribe("Mine", this.OnMine);
            EventManager.UnSubscribe("Extract", this.OnExtract);
            EventManager.UnSubscribe("Actions", this.OnActions);
            EventManager.UnSubscribe("Crafting", this.OnCrafting);
            EventManager.UnSubscribe("UnitTab", this.OnUnit);
            EventManager.UnSubscribe("Build Rocket", this.OnBuild);
            EventManager.UnSubscribe("Thrusters", this.OnThrusters);
            EventManager.UnSubscribe("Apply Chassis", this.OnChassis);
            EventManager.UnSubscribe("Cockpit", this.OnCockpit);
            EventManager.UnSubscribe("Apply Wings", this.OnWings);
            EventManager.UnSubscribe("OnMChoice", this.OnMChoice);
            EventManager.UnSubscribe("OnHChoice", this.OnHChoice);
            EventManager.UnSubscribe("OnEChoice", this.OnEChoice);
            EventManager.UnSubscribe("Player choose yes", this.OnYes);
            EventManager.UnSubscribe("Player choose no", this.OnNo);
            EventManager.UnSubscribe("SAExtract", this.OnSAExtract);
            EventManager.UnSubscribe("SAMiner", this.OnSAMiner);
            EventManager.UnSubscribe("SAUnit", this.OnSAUnit);
            EventManager.UnSubscribe("SAHarvest", this.OnSAHarvest);
            #endregion

            #region -- Main Menu Unsubscribers --
            EventManager.UnSubscribe("NewGame", this.NewGame);
            EventManager.UnSubscribe("Options Menu", this.OnOptions);
            EventManager.UnSubscribe("Instructions", this.OnInstructions);
            EventManager.UnSubscribe("QuitGame", this.OnQuitGame);
            EventManager.UnSubscribe("Close Options", this.CloseOptions);
            EventManager.UnSubscribe("Settings", this.OnSettings);
            EventManager.UnSubscribe("SettingsClose", this.OnSettingsClose);
            EventManager.UnSubscribe("Customize", this.OnCustomize);
            EventManager.UnSubscribe("CustomizeClose", this.OnCustomizeClose);
            #endregion

            #region -- Crafting Unsubscribers --
            EventManager.UnSubscribe("Minerals", this.OnMinerals);
            EventManager.UnSubscribe("Food", this.OnFood);
            EventManager.UnSubscribe("CookedFood", this.OnCookedFood);
            EventManager.UnSubscribe("Gas", this.OnGas);
            EventManager.UnSubscribe("Fuel", this.OnFuel);
            #endregion
        }
        #region -- VOID FUNCTIONS --

        void Update()
        {
            m_KillT.text = ObjectiveManager.Instance.TheObjectives[ObjectiveType.Kill].GetObjectiveInfo();
            m_CraftT.text = ObjectiveManager.Instance.TheObjectives[ObjectiveType.Craft].GetObjectiveInfo();
            m_MainT.text = ObjectiveManager.Instance.TheObjectives[ObjectiveType.Main].GetObjectiveInfo();

            //Updates the amount of resources the player has.
            m_MineralsT.text = " " + User.MineralsCount;
            m_FoodT.text = " " + User.FoodCount;
            m_CookedFoodT.text = "" + User.CookedFoodCount;
            m_GasT.text = " " + User.GasCount;
            m_FuelT.text = "" + User.FuelCount;
            m_SteelT.text = "" + User.SteelCount;

        }

        void ScaleFactor()
        {
            this.Scalefactor = 0;

            if (Screen.width == 1280 && Screen.height == 720)
            {
                Scalefactor = -90;
            }
            else if (Screen.width == 1360 && Screen.height == 768)
            {
                Scalefactor = -95;
            }
            else if (Screen.width == 1366 && Screen.height == 768)
            {
                Scalefactor = -95;
            }
            else if (Screen.width == 1600 && Screen.height == 900)
            {
                Scalefactor = -115;
            }
            else if (Screen.width == 1920 && Screen.height == 1080)
            {
                Scalefactor = -145;
            }
            else
            {
                Scalefactor = -120;
            }

            if (Scalefactor < 0)
            {
                m_ActionsTAB.offsetMax = new Vector2(m_ActionsTAB.offsetMax.x, Scalefactor);
                m_ActionsTAB.offsetMin = new Vector2(m_ActionsTAB.offsetMin.x, -115);

                m_CraftingTAB.offsetMax = new Vector2(m_CraftingTAB.offsetMax.x, Scalefactor);
                m_CraftingTAB.offsetMin = new Vector2(m_CraftingTAB.offsetMin.x, -115);

                m_UnitTAB.offsetMax = new Vector2(m_UnitTAB.offsetMax.x, Scalefactor);
                m_UnitTAB.offsetMin = new Vector2(m_UnitTAB.offsetMin.x, -115);

            }
        }

        /// <summary>
        /// The create unit button function.
        /// This function populates the panel with the a button for the unit that was
        /// passed in.
        /// <para></para>
        /// <remarks><paramref name="theunit"></paramref> -The unit to pass in so the unit button will have reference to it.</remarks>
        /// </summary>
        public void CreateUnitButton(GameObject theunit)
        {
            GameObject button = Instantiate(this.unitbutton);
            button.transform.SetParent(this.contentfield);

            IUnit u = (IUnit)theunit.GetComponent(typeof(IUnit));

            if (u is Harvester)
            {
                button.GetComponentInChildren<Text>().text = "H";
            }
            else if (u is Miner)
            {
                button.GetComponentInChildren<Text>().text = "M";
            }
            else if (u is Extractor)
            {
                button.GetComponentInChildren<Text>().text = "E";
            }

            button.AddComponent<UnitButton>().Unit = theunit;
            //button.GetComponent<UnitButton>().Tooltippanel = this.tooltippanel;
            //button.GetComponent<UnitButton>().Upgradepanel = this.upgradepanel;

            this.theUnitButtonsList.Add(button);
        }

        /// <summary>
        /// The clear unit buttons list function.
        /// This function destroys the buttons populated for a unit and clears the list.
        /// </summary>
        public void ClearUnitButtonsList()
        {
            foreach (GameObject go in this.theUnitButtonsList)
            {
                Destroy(go);
            }
            this.theUnitButtonsList.Clear();
        }


        public void OnActionsClick()
        {
            EventManager.Publish("Actions");
        }

        private void OnActions()
        {
            //If true set values to zero
            if (revertactionstab)
            {
                m_ActionsTAB.offsetMax = new Vector2(m_ActionsTAB.offsetMax.x, 0);
                m_ActionsTAB.offsetMin = new Vector2(m_ActionsTAB.offsetMin.x, 0);

                revertactionstab = false;
            }
            //If not true set to this position
            else if (!revertactionstab)
            {
                revertactionstab = true;

                m_ActionsTAB.offsetMax = new Vector2(m_ActionsTAB.offsetMax.x, Scalefactor);
                m_ActionsTAB.offsetMin = new Vector2(m_ActionsTAB.offsetMin.x, -115);
            }
            Debug.Log("Move Actions Tab down");
        }

        public void OnCraftingClick()
        {

            EventManager.Publish("Crafting");
        }

        private void OnCrafting()
        {

            if (revertcraftingtab)
            {
                m_CraftingTAB.offsetMax = new Vector2(m_CraftingTAB.offsetMax.x, 0);
                m_CraftingTAB.offsetMin = new Vector2(m_CraftingTAB.offsetMin.x, 0);
              
                revertcraftingtab = false;

            }
            else if (!revertcraftingtab)
            {
                revertcraftingtab = true;

                m_CraftingTAB.offsetMax = new Vector2(m_CraftingTAB.offsetMax.x, Scalefactor);
                m_CraftingTAB.offsetMin = new Vector2(m_CraftingTAB.offsetMin.x, -115);
            }


            Debug.Log("Move Crafting Tab down");
        }
        public void OnUnitClick()
        {
            EventManager.Publish("UnitTab");
        }
        private void OnUnit()
        {
            if(revertunittab)
            {
                m_UnitTAB.offsetMax = new Vector2(m_UnitTAB.offsetMax.x, 0);
                m_UnitTAB.offsetMin = new Vector2(m_UnitTAB.offsetMin.x, 0);

                revertunittab = false;
            }
            else if(!revertunittab)
            {
                revertunittab = true;

                m_UnitTAB.offsetMax = new Vector2(m_UnitTAB.offsetMax.x, Scalefactor);
                m_UnitTAB.offsetMin = new Vector2(m_UnitTAB.offsetMin.x, -115);
            }

            Debug.Log("Move Unit Tab down");
        }
        public void OnRallyClick()
        {
            EventManager.Publish("Rally");
        }
        private void OnRally()
        {
            //Function will be use to rally upon click.
            Debug.Log("Begin Rallying ");
        }
        public void OnHarvestClick()
        {
            EventManager.Publish("Harvest");
        }
        private void OnHarvest()
        {
            //Function will be use to harvest upon click.
            Debug.Log("Begin Harvesting");
        }
        public void OnRecallClick()
        {
            EventManager.Publish("Recall");
        }

        private void OnRecall()
        {
            UnitController.Self.CallHome();
            //Function will be use to recall upon click.
            Debug.Log("Recall to barracks.");
        }
        public void OnCancelActionClick()
        {
            EventManager.Publish("CancelAction");
        }
        private void OnCancelAction()
        {
            UnitController.Self.CancelAction();
            //Function will cancel previous action upon click.
            Debug.Log("Cancel Previous Action");
        }

        public void OnCraftClick()
        {

            //This function will run the craft function
            EventManager.Publish("Craft");
        }
        private void OnCraft()
        {

            Debug.Log("Craft");
        }
        public void OnClearClick()
        {
            EventManager.Publish("Clear");
        }
        private void OnClear()
        {
            minerals.sprite = Input1.sprite;
            Input2.sprite = Input2.sprite;
            //This function will clear items in the craft.
            Debug.Log("Clear");
        }
        public void OnWorkShopClick()
        {
            EventManager.Publish("Workshop");
        }
        private void OnWorkShop()
        {
            m_WorkshopUI.gameObject.SetActive(true);
            //This function will bring up the workshop within the game.
            Debug.Log("Workshop");
        }
        public void OnMineClick()
        {
            EventManager.Publish("Mine");
        }
        private void OnMine()
        {
            //This function will prompt the mining function
            Debug.Log("Begin Mining");
        }
        public void OnExtractClick()
        {
            EventManager.Publish("Extract");
        }
        private void OnExtract()
        {
            Debug.Log("Begin Extracting");
        }
        #region -- Main Menu Functions --
        public void OnOptionsClick()
        {
            EventManager.Publish("Options Menu");
        }
        private void OnOptions()
        {
            m_OptionsUI.gameObject.SetActive(true);
            m_SettingsUI.gameObject.SetActive(false);
            Debug.Log("Options Menu");
        }
        public void CloseOptionsClick()
        {
            EventManager.Publish("Close Options");
        }

        private void CloseOptions()
        {
            //Sets the options panel to false when the back button is clicked.
            m_OptionsUI.gameObject.SetActive(false);
            m_SettingsUI.gameObject.SetActive(true);
            Debug.Log("Close Options");
        }

        public void CloseWorkShopClick()
        {
            EventManager.Publish("Close WorkShop");
        }
        private void CloseWorkShop()
        {
            //This function will close work shop menu
            m_WorkshopUI.gameObject.SetActive(false);
            Debug.Log("Close Workshop Menu");

        }
        public void NewGameClick()
        {
            EventManager.Publish("NewGame");
        }
        private void NewGame()
        {
            SceneManager.LoadScene(1);
            //Function will begin game from main menu
            Debug.Log("New Game");
        }

        public void OnInstructionsClick()
        {
            EventManager.Publish("Instructions");
        }

        private void OnInstructions()
        {
            //Function will bring up the instructions.
            Debug.Log("Instructions");
        }
        public void OnSettingsClick()
        {
            EventManager.Publish("Settings");
        }
        private void OnSettings()
        {
            m_SettingsUI.gameObject.SetActive(true);
            Debug.Log("Settings Menu");
        }
        public void OnSettingsCloseClick()
        {
            EventManager.Publish("SettingsClose");
        }
        private void OnSettingsClose()
        {
            m_SettingsUI.gameObject.SetActive(false);
            Debug.Log("Settings Close");
        }
        public void OnCustomizeClick()
        {
            EventManager.Publish("Customize");
        }
        private void OnCustomize()
        {
            //Used to set all the UI to inactive when the customize menu is open.
            m_CustomizeUI.gameObject.SetActive(true);
            m_OptionsUI.gameObject.SetActive(false);
            m_MainUI.gameObject.SetActive(false);
            m_ActionsTAB.gameObject.SetActive(false);
            m_CraftingTAB.gameObject.SetActive(false);
            m_Workshop.gameObject.SetActive(false);
            m_ObjectiveUI.gameObject.SetActive(false);
            Debug.Log("Customize Menu");
        }
        public void OnCustomizeCloseClick()
        {
            EventManager.Publish("CustomizeClose");
        }
        private void OnCustomizeClose()
        {
            //Used to set all UI to active when the customize menu is open.
            m_CustomizeUI.gameObject.SetActive(false);
            m_OptionsUI.gameObject.SetActive(true);
            m_MainUI.gameObject.SetActive(true);
            m_ActionsTAB.gameObject.SetActive(true);
            m_CraftingTAB.gameObject.SetActive(true);
            m_Workshop.gameObject.SetActive(true);
            m_ObjectiveUI.gameObject.SetActive(true);
            Debug.Log("Customize closed");
        }
        public void OnMChoiceClick()
        {
            EventManager.Publish("OnMChoice");
        }
        private void OnMChoice()
        {
            m_AreyousureUI.gameObject.SetActive(true);
            Debug.Log("Miners Choice");
        }
        public void OnHChoiceClick()
        {
            EventManager.Publish("OnHChoice");
        }
        private void OnHChoice()
        {
            m_AreyousureUI.gameObject.SetActive(true);
            Debug.Log("Harvester Choice");
        }
        public void OnEChoiceClick()
        {
            EventManager.Publish("OnEChoice");
        }
        private void OnEChoice()
        {
            
            m_AreyousureUI.gameObject.SetActive(true);
            Debug.Log("Extractor Choice");
        }
        public void OnYesClick()
        {
            EventManager.Publish("Player choose yes");
        }
        private void OnYes()
        {
            Debug.Log("User choose Yes");
            m_AreyousureUI.gameObject.SetActive(false);
        }
        public void OnNoClick()
        {
            EventManager.Publish("Player choose no");
        }
        private void OnNo()
        {
            Debug.Log("User choose No");
            m_AreyousureUI.gameObject.SetActive(false);
        }
        public void OnQuitGameClick()
        {
            EventManager.Publish("QuitGame");
        }
        private void OnQuitGame()
        {
            Application.Quit();
            //Function will quit game upon click.
            Debug.Log("Quit Game");
        }
        #endregion

        #region -- Crafting -- 
        public void Minerals()
        {
            EventManager.Publish("Minerals");
        }
        private void OnMinerals()
        {
            //Will Change the source image to the first craft slot
            //Second Slot if first one is selected.
            Input1.sprite = minerals.sprite;
            Debug.Log("Minerals");
        }

        public void Food()
        {

            EventManager.Publish("Food");
        }
        private void OnFood()
        {
            //Will Change the source image to the first craft slot
            //Second Slot if first one is selected.
            User.FoodCount++;
            Debug.Log("Food");
        }

        public void CookedFood()
        {
            EventManager.Publish("CookedFood");
        }

        private void OnCookedFood()
        {
            //Will Change the source image to the first craft slot
            //Second Slot if first one is selected.
            if (User.CookedFoodCount > 0)
            {
                Vector3 mousePosition;
                mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                this.foodinstance = Instantiate(cookedfoodPrefab, mousePosition, Quaternion.identity);
                Debug.Log("Cooked Food");

                User.CookedFoodCount--;
            }
        }

        public void Gas()
        {
            EventManager.Publish("Gas");
        }

        private void OnGas()
        {
            //Will Change the source image to the first craft slot
            //Second Slot if first one is selected.
            User.GasCount++;
            Debug.Log("Gas");
        }

        public void Fuel()
        {
            EventManager.Publish("Fuel");
        }

        private void OnFuel()
        {
            //Will Change the source image to the first craft slot
            //Second Slot if first one is selected.
            User.FuelCount++;

            Debug.Log("Fuel");
        }

        #endregion
        #endregion


        #region -- WorkShop --
        public void OnBuildClick()
        {
            EventManager.Publish("Build Rocket");
        }
        private void OnBuild()
        {
            //Function that will craft the ship when all parts are obtained.
            Debug.Log("Build Rocket");
        }
        public void OnThrustersClick()
        {
            EventManager.Publish("Thrusters");
        }
        private void OnThrusters()
        {
            if (undo1)
            {
                m_ThrusterChoice.gameObject.SetActive(true);

                undo1 = false;
            }
            else if (!undo1)
            {
                m_ThrusterChoice.gameObject.SetActive(false);
                undo1 = true;
            }

            //Function that will apply the selected thruster on the ship
            Debug.Log("Apply Thrusters");
        }
        public void OnChassisClick()
        {
            EventManager.Publish("Chassis");
        }
        private void OnChassis()
        {

            //Function that will apply the selected chassis on the ship
            Debug.Log("Apply Chassis");
        }
        public void OnCockpitClick()
        {
            EventManager.Publish("Cockpit");
        }
        private void OnCockpit()
        {
            if (undo2)
            {
                m_CockpitChoice.gameObject.SetActive(true);

                undo2 = false;
            }
            else if (!undo2)
            {
                m_CockpitChoice.gameObject.SetActive(false);

                undo2 = true;
            }
            //Function that will apply the selected cockpit on the ship
            Debug.Log("Apply Cockpit");
        }
        public void OnWingsClick()
        {

            EventManager.Publish("Apply Wings");
        }
        private void OnWings()
        {
            if (undo3)
            {
                m_WingChoice.gameObject.SetActive(true);

                undo3 = false;
            }
            else if (!undo3)
            {
                m_WingChoice.gameObject.SetActive(false);

                undo3 = true;
            }
            //Function that will apply the selected wing on the ship
            Debug.Log("Apply Wings");
        }
        public void OnWC1Click()
        {
            EventManager.Publish("WingChoice1");
        }
        private void OnWC1()
        {
            Debug.Log("Player chose WingChoice1");
        }
        public void OnWC2Click()
        {
            EventManager.Publish("WingChoice2");

        }
        private void OnWC2()
        {
            Debug.Log("Player chose WingChoice2");
        }
        public void OnWC3Click()
        {
            EventManager.Publish("WingChoice3");
        }
        private void OnWC3()
        {
            Debug.Log("Player chose WingChoice3");
        }
        public void OnCP1Click()
        {
            EventManager.Publish("Player chose CP1");
        }
        private void OnCP1()
        {
            Debug.Log("Player chose Cockpit1");
        }
        public void OnCP2Click()
        {
            EventManager.Publish("Player chose CP2");
        }
        private void OnCP2()
        {
            Debug.Log("Player chose Cockpit2");
        }
        public void OnCP3Click()
        {
            EventManager.Publish("Player chose CP3");
        }
        private void OnCP3()
        {
            Debug.Log("Player chose Cockpit3");
        }
        public void OnTC1Click()
        {
            EventManager.Publish("Player chose TC1");
        }
        private void OnTC1()
        {
            Debug.Log("Player chose Thrust1");
        }
        public void OnTC2Click()
        {
            EventManager.Publish("Player chose TC2");
        }
        private void OnTC2()
        {
            Debug.Log("Player chose Thrust2");
        }
        public void OnTC3Click()
        {
            EventManager.Publish("Player chose TC3");
        }
        private void OnTC3()
        {
            Debug.Log("Player chose Thrust3");
        }



        #endregion

        #region -- SelectAll --
        public void OnSAExtractClick()
        {
            EventManager.Publish("SAExtract");
        }
        private void OnSAExtract()
        {
            //Calls the SelectAll Extractors function within the UnitController.
            UnitController.Self.SelectAllExtractors();
            Debug.Log("Select All Extract");
        }

        public void OnSAMinerClick()
        {
            EventManager.Publish("SAMiner");
        }
        private void OnSAMiner()
        {
            //Calls the SelectAll Miners function within the UnitController.
            UnitController.Self.SelectAllMiners();
            Debug.Log("Select All Miner");
        }

        public void OnSAUnitClick()
        {
            EventManager.Publish("SAUnit");
        }
        private void OnSAUnit()
        {
            //Calls the Select All Units function within the UnitController.
            UnitController.Self.SelectAllUnits();
            Debug.Log("Select All Units");
        }
        public void OnSAHarvestClick()
        {
            EventManager.Publish("SAHarvest");
        }
        private void OnSAHarvest()
        {
            //Calls the Select All Harvesters function within the UnitController.
            UnitController.Self.SelectAllHarvesters();
            Debug.Log("Select All Harvesters");
        }
        #endregion


    }
}