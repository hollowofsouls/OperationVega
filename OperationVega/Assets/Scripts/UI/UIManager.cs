using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Text = UnityEngine.UI.Text;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Assets.Scripts.Managers;



namespace UI
{
    using Assets.Scripts;
    
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
        private Image m_Input1;
        [SerializeField]
        private Image m_Input2;

        bool revert;
        bool undo;
       


        #endregion

        #region -- PROPERTIES --
        public Canvas BackgroundUI
        {
            get { return m_BackgroundUI; }
        }
        #endregion

        protected void Awake()
        {
            //Bool use to manage crafting / action tab
            revert = true;
            undo = true;
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
            EventManager.Subscribe("Build Rocket", this.OnBuild);
            EventManager.Subscribe("Thrusters", this.OnThrusters);
            EventManager.Subscribe("Apply Chassis", this.OnChassis);
            EventManager.Subscribe("Cockpit", this.OnCockpit);
            EventManager.Subscribe("Apply Wings", this.OnWings);
            #endregion

            #region -- Main Menu Subscribers --
            EventManager.Subscribe("NewGame", this.NewGameClick);
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
            EventManager.Subscribe("Minerals", this.Minerals);
            EventManager.Subscribe("Food", this.Food);
            EventManager.Subscribe("CookedFood", this.CookedFood);
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
            EventManager.UnSubscribe("Build Rocket", this.OnBuild);
            EventManager.UnSubscribe("Thrusters", this.OnThrusters);
            EventManager.UnSubscribe("Apply Chassis", this.OnChassis);
            EventManager.UnSubscribe("Cockpit", this.OnCockpit);
            EventManager.UnSubscribe("Apply Wings", this.OnWings);
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
            //Updates the amount of resources the player has.
            m_MineralsT.text = " " + User.MineralsCount;
            m_FoodT.text = " " + User.FoodCount;
            m_CookedFoodT.text = "" + User.CookedFoodCount;
            m_GasT.text = " " + User.GasCount;
            m_FuelT.text = "" + User.FuelCount;
            m_SteelT.text = "" + User.SteelCount;
            
        }

       
        public void OnActionsClick()
        {
            EventManager.Publish("Actions");
        }

        public void OnActions()
        {
            //If true set values to zero
            if (revert)
            {
                m_ActionsTAB.offsetMax = new Vector2(m_ActionsTAB.offsetMax.x, 0);
                m_ActionsTAB.offsetMin = new Vector2(m_ActionsTAB.offsetMin.x, 0);

                revert = false;
            }
            //If not true set to this position
            else if(!revert)
            {
                revert = true;

                m_ActionsTAB.offsetMax = new Vector2(m_ActionsTAB.offsetMax.x, -115);
                m_ActionsTAB.offsetMin = new Vector2(m_ActionsTAB.offsetMin.x, -115);
            }
            Debug.Log("Move Actions Tab down");
        }

        public void OnCraftingClick()
        {
           
            EventManager.Publish("Crafting");
        }

        public void OnCrafting()
        {

            if (revert)
            {
                m_CraftingTAB.offsetMax = new Vector2(m_CraftingTAB.offsetMax.x, 0);
                m_CraftingTAB.offsetMin = new Vector2(m_CraftingTAB.offsetMin.x, 0);
                //m_CraftingTAB.localPosition = new Vector3(m_CraftingTAB.localPosition.x, , m_CraftingTAB.localPosition.z);
       
                revert = false;
                                       
            }
            else if(!revert)
            {
                //m_CraftingTAB.position = new Vector3(m_CraftingTAB.position.x, 115, m_CraftingTAB.position.z);
                revert = true;

                m_CraftingTAB.offsetMax = new Vector2(m_CraftingTAB.offsetMax.x, -115);
                m_CraftingTAB.offsetMin = new Vector2(m_CraftingTAB.offsetMin.x, -115);
            }


            Debug.Log("Move Crafting Tab down");
        }
        public void OnRallyClick()
        {
            EventManager.Publish("Rally");
        }
        public void OnRally()
        {    
            //Function will be use to rally upon click.
            Debug.Log("Begin Rallying ");
        }
        public void OnHarvestClick()
        {
            EventManager.Publish("Harvest");
        }
        public void OnHarvest()
        {           
            //Function will be use to harvest upon click.
            Debug.Log("Begin Harvesting");
        }
        public void OnRecallClick()
        {
            EventManager.Publish("Recall");
        }

        public void OnRecall()
        {            
            //Function will be use to recall upon click.
            Debug.Log("Recall to barracks.");
        }
        public void OnCancelActionClick()
        {
            EventManager.Publish("CancelAction");
        }
        public void OnCancelAction()
        {          
            //Function will cancel previous action upon click.
            Debug.Log("Cancel Previous Action");
        }

        public void OnCraftClick()
        {

            //This function will run the craft function
            EventManager.Publish("Craft");
        }
        void OnCraft()
        {
           
            Debug.Log("Craft");
        }
        public void OnClearClick()
        {
            EventManager.Publish("Clear");
        }
        public void OnClear()
        {
            //This function will clear items in the craft.
            Debug.Log("Clear");
        }
        public void OnWorkShopClick()
        {
            EventManager.Publish("Workshop");
        }
        public void OnWorkShop()
        {
            m_WorkshopUI.gameObject.SetActive(true);
            //This function will bring up the workshop within the game.
            Debug.Log("Workshop");
        }
        public void OnMineClick()
        {
            EventManager.Publish("Mine");
        }
        public void OnMine()
        {
            //This function will prompt the mining function
            Debug.Log("Begin Mining");
        }
        public void OnExtractClick()
        {
            EventManager.Publish("Extract");
        }
        public void OnExtract()
        {
            Debug.Log("Begin Extracting");
        }
        #region -- Main Menu Functions --
        public void OnOptionsClick()
        {
            EventManager.Publish("Options Menu");
        }
        public void OnOptions()
        {
            m_OptionsUI.gameObject.SetActive(true);
            m_SettingsUI.gameObject.SetActive(false);
            Debug.Log("Options Menu");
        }
        public void CloseOptionsClick()
        {
            EventManager.Publish("Close Options");
        }

        public void CloseOptions()
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
        public void CloseWorkShop()
        {
           //This function will close work shop menu
            m_WorkshopUI.gameObject.SetActive(false);
            Debug.Log("Close Workshop Menu");

        }
        public void NewGameClick()
        {
            EventManager.Publish("NewGame");
        }
        public void NewGame()
        {
            SceneManager.LoadScene(1);
            //Function will begin game from main menu
            Debug.Log("New Game");
        }

        public void OnInstructionsClick()
        {
            EventManager.Publish("Instructions");
        }

        public void OnInstructions()
        {          
            //Function will bring up the instructions.
            Debug.Log("Instructions");
        }
        public void OnSettingsClick()
        {
            EventManager.Publish("Settings");
        }
        public void OnSettings()
        {
            m_SettingsUI.gameObject.SetActive(true);
            Debug.Log("Settings Menu");
        }
        public void OnSettingsCloseClick()
        {
            EventManager.Publish("SettingsClose");
        }
        public void OnSettingsClose()
        {
            m_SettingsUI.gameObject.SetActive(false);
            Debug.Log("Settings Close");
        }
        public void OnCustomizeClick()
        {
            EventManager.Publish("Customize");
        }
        public void OnCustomize()
        {
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
        public void OnCustomizeClose()
        {
            m_CustomizeUI.gameObject.SetActive(false);
            m_OptionsUI.gameObject.SetActive(true);
            m_MainUI.gameObject.SetActive(true);
            m_ActionsTAB.gameObject.SetActive(true);
            m_CraftingTAB.gameObject.SetActive(true);
            m_Workshop.gameObject.SetActive(true);
            m_ObjectiveUI.gameObject.SetActive(true);
            Debug.Log("Customize closed");
        }
        public void OnQuitGameClick()
        {
            EventManager.Publish("QuitGame");
        }
        public void OnQuitGame()
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
        public void OnMinerals()
        {
            //Will Change the source image to the first craft slot
            //Second Slot if first one is selected.
            Debug.Log("Minerals");
        }
        
        public void Food()
        {

            EventManager.Publish("Food");
        }
        public void OnFood()
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

        public void OnCookedFood()
        {
            //Will Change the source image to the first craft slot
            //Second Slot if first one is selected.
            Debug.Log("Cooked Food");
        }

        public void Gas()
        {
            EventManager.Publish("Gas");
        }

        public void OnGas()
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

        public void OnFuel()
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
        public void  OnBuild()
        {
            //Function that will craft the ship when all parts are obtained.
            Debug.Log("Build Rocket");
        }
        public void OnThrustersClick()
        {
            EventManager.Publish("Thrusters");
        }
        public void OnThrusters()
        {
            if (undo)
            {
                m_ThrusterChoice.gameObject.SetActive(true);

                undo = false;
            }
            else if(!undo)
            {
                m_ThrusterChoice.gameObject.SetActive(false);
                undo = true;
            }

            //Function that will apply the selected thruster on the ship
            Debug.Log("Apply Thrusters");
        }
        public void OnChassisClick()
        {
            EventManager.Publish("Chassis");
        }
        public void OnChassis()
        {
            
            //Function that will apply the selected chassis on the ship
            Debug.Log("Apply Chassis");
        }
        public void OnCockpitClick()
        {
            EventManager.Publish("Cockpit");
        }
        public void OnCockpit()
        {
            if(undo)
            {
                m_CockpitChoice.gameObject.SetActive(true);

                undo = false;
            }
            else if(!undo)
            {
                m_CockpitChoice.gameObject.SetActive(false);

                undo = true;
            }
            //Function that will apply the selected cockpit on the ship
            Debug.Log("Apply Cockpit");
        }
        public void OnWingsClick()
        {

            EventManager.Publish("Apply Wings");
        }
        public void OnWings()
        {
            if (undo)
            {
                m_WingChoice.gameObject.SetActive(true);

                undo = false;
            }
            else if(!undo)
            {
                m_WingChoice.gameObject.SetActive(false);

                undo = true;
            }
            //Function that will apply the selected wing on the ship
            Debug.Log("Apply Wings");
        }
        public void OnWC1Click()
        {
            EventManager.Publish("WingChoice1");
        }
        public void OnWC1()
        {
            Debug.Log("Player chose WingChoice1");
        }
        public void OnWC2Click()
        {
            EventManager.Publish("WingChoice2");

        }
        public void OnWC2()
        {
            Debug.Log("Player chose WingChoice2");
        }
        public void OnWC3Click()
        {
            EventManager.Publish("WingChoice3");
        }
        public void OnWC3()
        {
            Debug.Log("Player chose WingChoice3");
        }
        public void OnCP1Click()
        {
            EventManager.Publish("Player chose CP1");
        }
        public void OnCP1()
        {
            Debug.Log("Player chose Cockpit1");
        }
        public void OnCP2Click()
        {
            EventManager.Publish("Player chose CP2");
        }
        public void OnCP2()
        {
            Debug.Log("Player chose Cockpit2");
        }
        public void OnCP3Click()
        {
            EventManager.Publish("Player chose CP3");
        }
        public void OnCP3()
        {
            Debug.Log("Player chose Cockpit3");
        }
        public void OnTC1Click()
        {
            EventManager.Publish("Player chose TC1");
        }
        public void OnTC1()
        {
            Debug.Log("Player chose Thrust1");
        }
        public void OnTC2Click()
        {
            EventManager.Publish("Player chose TC2");
        }
        public void OnTC2()
        {
            Debug.Log("Player chose Thrust2");
        }
        public void OnTC3Click()
        {
            EventManager.Publish("Player chose TC3");
        }
        public void OnTC3()
        {
            Debug.Log("Player chose Thrust3");
        }

        

        #endregion

    }
}
