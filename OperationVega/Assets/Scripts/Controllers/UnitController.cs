
namespace Assets.Scripts.Controllers
{
    using System.Collections.Generic;

    using Interfaces;

    using UnityEngine;

    /// <summary>
    /// The unit controller class.
    /// This class handles functionality of the units click movement
    /// </summary>
    public class UnitController : MonoBehaviour
    {
        /// <summary>
        /// The instance of the class.
        /// </summary>
        private static UnitController instance;

        /// <summary>
        /// The drag screen.
        /// </summary>
        private static Rect dragscreen = new Rect(0, 0, 0, 0);

        /// <summary>
        /// The selection highlight is the texture.
        /// </summary>
        [SerializeField]
        private Texture2D selectionHighlight;

        /// <summary>
        /// The start click reference.
        /// </summary>
        private Vector3 startclick = -Vector3.one;

        /// <summary>
        /// The the clicked object reference.
        /// </summary>
        public GameObject theclickedobject;

        /// <summary>
        /// The unit that has been selected.
        /// </summary>
        [SerializeField]
        private IGather theUnit;

        /// <summary>
        /// The list of units selected by drag screen.
        /// </summary>
        [SerializeField]
        public List<GameObject> Units = new List<GameObject>();

        /// <summary>
        /// The click destination of where to send the unit.
        /// </summary>
        public Vector3 clickdestination;

        /// <summary>
        /// Gets the instance of the UnitController.
        /// </summary>
        public static UnitController Self
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Gets or sets the drag screen.
        /// </summary>
        public static Rect DragScreen
        {
            get
            {
                return dragscreen;
            }

            set
            {
                dragscreen = value;
            }
        }

        /// <summary>
        /// The invert y function.
        /// Inverts the y.
        /// </summary>
        /// <param name="y">
        /// The y value to pass in.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public static float InvertY(float y)
        {
            return Screen.height - y;
        }

        ///// <summary>
        ///// The spawn unit function.
        ///// This function spawns the passed in unit
        ///// </summary>
        ///// <param name="theUnit">
        ///// The Unit to spawn.
        ///// </param>
        //public void SpawnUnit(IUnit theUnit)
        //{
        //    GameObject player = Instantiate(this.playerPrefab, Vector3.zero, Quaternion.identity);
        //}

        /// <summary>
        /// The check if selected function.
        /// Checks if the current game object is under the drag screen
        /// </summary>
        /// <param name="theunit">
        /// The unit to check if its under the drag screen.
        /// </param>
        public void CheckIfSelected(GameObject theunit)
        {
            if (theunit.GetComponent<Renderer>().isVisible && Input.GetMouseButtonUp(0))
            {
                Vector3 camPos = Camera.main.WorldToScreenPoint(theunit.transform.position);
                camPos.y = InvertY(camPos.y);

                if (DragScreen.Contains(camPos) & !this.Units.Contains(theunit))
                {
                    this.Units.Add(theunit);
                }

            }
        }

        /// <summary>
        /// The start function.
        /// </summary>
        private void Start()
        {
            instance = this;
        }

        /// <summary>
        /// The update function.
        /// </summary>
        private void Update()
        {
            this.ActivateDragScreen();
            this.SelectUnits();
            this.CommandUnits();
        }

        /// <summary>
        /// The select units function.
        /// This function is used for unit selection
        /// </summary>
        private void SelectUnits()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                this.ClearSelectedUnits();

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    if (hit.transform.GetComponent(typeof(IGather)))
                    {
                        this.theUnit = (IGather)hit.transform.GetComponent(typeof(IGather));
                    }
                }
            }
        }

        /// <summary>
        /// The activate drag screen function.
        /// This controls the drag screen
        /// </summary>
        private void ActivateDragScreen()
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.theUnit = null;
                this.Units.Clear();
                this.startclick = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (dragscreen.width < 0)
                {
                    dragscreen.x += dragscreen.width;
                    dragscreen.width = -dragscreen.width;
                }
                if (dragscreen.height < 0)
                {
                    dragscreen.y += dragscreen.height;
                    dragscreen.height = -dragscreen.height;
                }

                this.startclick = -Vector3.one;
            }

            if (Input.GetMouseButton(0))
            {
                dragscreen = new Rect(
                    this.startclick.x,
                    InvertY(this.startclick.y),
                    Input.mousePosition.x - this.startclick.x,
                    InvertY(Input.mousePosition.y) - InvertY(this.startclick.y));
            }
        }

        /// <summary>
        /// The clear selected units function.
        /// This clears the list of selected units and the current selected unit
        /// </summary>
        private void ClearSelectedUnits()
        {
            this.Units.Clear();
            this.theUnit = null;
            this.theclickedobject = null;
        }

        /// <summary>
        /// The command units function.
        /// Gives the units proper commands.
        /// </summary>
        private void CommandUnits()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    this.CheckTheClickedObject(hit);

                    if (hit.transform.GetComponent<Enemy>())
                    {
                        this.CommandToAttack(hit);
                    }
                    else if (hit.transform.GetComponent(typeof(IResources)))
                    {
                        this.CommandToHarvest(hit);
                    }
                    else
                    {
                        this.CommandToIdle();
                    }

                    this.CommandToMove();
                }
            }
        }

        /// <summary>
        /// The command to attack function.
        /// </summary>
        /// <param name="hit">
        /// The hit object.
        /// </param>
        private void CommandToAttack(RaycastHit hit)
        {
                if (this.theUnit != null)
                {
                    // Attack with a single unit
                    switch (this.theUnit.GetType().ToString())
                    {
                        case "Assets.Scripts.Extractor":
                            Extractor extractor = this.theUnit as Extractor;
                            extractor.Target = (IDamageable)hit.transform.GetComponent(typeof(IDamageable));
                            extractor.ChangeStates("Battle");
                            break;
                        case "Assets.Scripts.Miner":
                            Miner miner = this.theUnit as Miner;
                            miner.Target = (IDamageable)hit.transform.GetComponent(typeof(IDamageable));
                            miner.ChangeStates("Battle");
                            break;
                    case "Assets.Scripts.Harvester":
                        Harvester harvester = this.theUnit as Harvester;
                        harvester.Target = (IDamageable)hit.transform.GetComponent(typeof(IDamageable));
                        harvester.ChangeStates("Battle");
                        break;
                }
                } 
                else if (this.Units.Count > 0)
                {
                    foreach (GameObject go in this.Units)
                    {
                        IGather unit = (IGather)go.GetComponent(typeof(IGather));

                        switch (unit.GetType().ToString())
                        {
                            case "Assets.Scripts.Extractor":
                                Extractor extractor = unit as Extractor;
                                extractor.Target = (IDamageable)hit.transform.GetComponent(typeof(IDamageable));
                                extractor.ChangeStates("Battle");
                                break;
                            case "Assets.Scripts.Miner":
                                Miner miner = unit as Miner;
                                miner.Target = (IDamageable)hit.transform.GetComponent(typeof(IDamageable));
                                miner.ChangeStates("Battle");
                                break;
                        case "Assets.Scripts.Harvester":
                            Harvester harvester = unit as Harvester;
                            harvester.Target = (IDamageable)hit.transform.GetComponent(typeof(IDamageable));
                            harvester.ChangeStates("Battle");
                            break;
                    }
                    }
                }
            
        }

        /// <summary>
        /// The command to harvest function.
        /// </summary>
        /// <param name="hit">
        /// The hit object.
        /// </param>
        private void CommandToHarvest(RaycastHit hit)
        {
                if (this.theUnit != null)
                {
                    // Harvest with a single unit
                    switch (this.theUnit.GetType().ToString())
                    {
                        case "Assets.Scripts.Extractor":
                            Extractor extractor = this.theUnit as Extractor;
                            extractor.TargetResource = hit.transform.GetComponent<Gas>();
                            if (extractor.TargetResource != null)
                            {
                                extractor.ChangeStates("Harvest");
                            }
                            break;
                        case "Assets.Scripts.Miner":
                            Miner miner = this.theUnit as Miner;
                            miner.TargetResource = hit.transform.GetComponent<Minerals>();
                            if (miner.TargetResource != null)
                            {
                                miner.ChangeStates("Harvest");
                            }
                            break;
                        case "Assets.Scripts.Harvester":
                            Harvester harvester = this.theUnit as Harvester;
                            harvester.TargetResource = hit.transform.GetComponent<Food>();
                            if (harvester.TargetResource != null)
                            {
                                harvester.ChangeStates("Harvest");
                            }
                            break;
                    }
                }
                else if (this.Units.Count > 0)
                {
                    foreach (GameObject go in this.Units)
                    {
                        IGather unit = (IGather)go.GetComponent(typeof(IGather));

                        switch (unit.GetType().ToString())
                        {
                            case "Assets.Scripts.Extractor":
                                Extractor extractor = unit as Extractor;
                                extractor.TargetResource = hit.transform.GetComponent<Gas>();
                                if (extractor.TargetResource != null)
                                {
                                    extractor.ChangeStates("Harvest");
                                }
                                break;
                            case "Assets.Scripts.Miner":
                                Miner miner = unit as Miner;
                                miner.TargetResource = hit.transform.GetComponent<Minerals>();
                                if (miner.TargetResource != null)
                                {
                                    miner.ChangeStates("Harvest");
                                }
                                break;
                            case "Assets.Scripts.Harvester":
                                Harvester harvester = unit as Harvester;
                                harvester.TargetResource = hit.transform.GetComponent<Food>();
                                if (harvester.TargetResource != null)
                                {
                                    harvester.ChangeStates("Harvest");
                                }
                                break;
                        }
                    }
                }
        }

        /// <summary>
        /// The command to move function.
        /// </summary>
        private void CommandToMove()
        {
            if (this.theUnit != null & this.theclickedobject != null)
            {
                this.CheckUnitTypeAndState(this.theUnit);
            }
            else if (this.Units.Count > 0 & this.theclickedobject != null)
            {
                foreach (GameObject p in this.Units)
                {
                    IGather g = (IGather)p.GetComponent(typeof(IGather));
                    this.CheckUnitTypeAndState(g);
                }
            }
        }

        /// <summary>
        /// The command to idle function.
        /// </summary>
        private void CommandToIdle()
        {
            // Send unit back to idle
            if (this.theUnit != null)
            {
                switch (this.theUnit.GetType().ToString())
                {
                    case "Assets.Scripts.Extractor":
                        Extractor extractor = this.theUnit as Extractor;
                        extractor.Target = null;
                        extractor.TargetResource = null;
                        extractor.ChangeStates("Idle");
                        break;
                    case "Assets.Scripts.Miner":
                        Miner miner = this.theUnit as Miner;
                        miner.Target = null;
                        miner.TargetResource = null;
                        miner.ChangeStates("Idle");
                        break;
                    case "Assets.Scripts.Harvester":
                        Harvester harvester = this.theUnit as Harvester;
                        harvester.Target = null;
                        harvester.TargetResource = null;
                        harvester.ChangeStates("Idle");
                        break;
                }
            }
            else if (this.Units.Count > 0)
            {
                foreach (GameObject go in this.Units)
                {
                    IGather unit = (IGather)go.GetComponent(typeof(IGather));

                    switch (unit.GetType().ToString())
                    {
                        case "Assets.Scripts.Extractor":
                            Extractor extractor = unit as Extractor;
                            extractor.Target = null;
                            extractor.TargetResource = null;
                            extractor.ChangeStates("Idle");
                            break;
                        case "Assets.Scripts.Miner":
                            Miner miner = unit as Miner;
                            miner.Target = null;
                            miner.TargetResource = null;
                            miner.ChangeStates("Idle");
                            break;
                        case "Assets.Scripts.Harvester":
                            Harvester harvester = unit as Harvester;
                            harvester.Target = null;
                            harvester.TargetResource = null;
                            harvester.ChangeStates("Idle");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// The check clicked object function.
        /// This determines the type of object clicked.
        /// </summary>
        /// <param name="hit">
        /// The object hit by the ray cast.
        /// </param>
        private void CheckTheClickedObject(RaycastHit hit)
        {
            this.theclickedobject = hit.transform.gameObject;

            if (hit.transform.GetComponent(typeof(IUnit)) || hit.transform.GetComponent(typeof(IResources)))
            {
                this.clickdestination = new Vector3(
                    this.theclickedobject.transform.position.x,
                    0.5f,
                    this.theclickedobject.transform.position.z);
            }
            else
            {
                this.clickdestination = new Vector3(hit.point.x, 0.5f, hit.point.z);
            }
        }

        /// <summary>
        /// The check unit type and state function.
        /// This function allows movement but restricts movement to the wrong resource.
        /// </summary>
        /// <param name="gatheringunit">
        /// The gathering unit.
        /// </param>
        private void CheckUnitTypeAndState(IGather gatheringunit)
        {
            IResources theresource = this.theclickedobject.GetComponent<IResources>();

            switch (gatheringunit.GetType().ToString())
            {
                case "Assets.Scripts.Extractor":
                    Extractor extractor = gatheringunit as Extractor;
                    string estate = extractor.TheExtractorFsm.CurrentState.Statename;
                    Gas thegas = theresource as Gas;
                    if (theresource != null & thegas == null)
                    {

                    }
                    else
                    {
                        gatheringunit.SetTheTargetPosition(this.clickdestination);
                    }
                    break;
                case "Assets.Scripts.Miner":
                    Miner miner = gatheringunit as Miner;
                    string mstate = miner.TheMinerFsm.CurrentState.Statename;
                    Minerals themineral = theresource as Minerals;
                    if (theresource != null & themineral == null)
                    {
                    }
                    else
                    {
                        gatheringunit.SetTheTargetPosition(this.clickdestination);
                    }
                    break;
                case "Assets.Scripts.Harvester":
                    Harvester harvester = gatheringunit as Harvester;
                    string hstate = harvester.TheHarvesterFsm.CurrentState.Statename;
                    Food thefood = theresource as Food;
                    if (theresource != null & thefood == null)
                    {
                        Debug.Log("No Food but is a resource");
                    }
                    else
                    {
                        gatheringunit.SetTheTargetPosition(this.clickdestination);
                    }
                    break;
            }
        }

        /// <summary>
        /// The On GUI function.
        /// This draws the drag screen to the screen
        /// </summary>
        private void OnGUI()
        {
            if (this.startclick != -Vector3.one)
            {
                GUI.color = new Color(1, 1, 1, 0.5f);
                GUI.DrawTexture(dragscreen, this.selectionHighlight);
            }
        }
    }
}
