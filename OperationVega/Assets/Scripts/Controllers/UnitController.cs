
namespace Assets.Scripts.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Managers;

    using Interfaces;

    using UI;

    using UnityEngine;
    using UnityEngine.AI;
    using UnityEngine.EventSystems;

    /// <summary>
    /// The unit controller class.
    /// This class handles functionality of the units click movement and actions.
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
        /// The list of units selected by the drag screen.
        /// </summary>
        [HideInInspector]
        private readonly List<GameObject> units = new List<GameObject>();

        /// <summary>
        /// The click destination of where to send the unit.
        /// </summary>
        private Vector3 clickdestination;

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
        /// The selected object reference.
        /// This is the object that is left clicked on.
        /// </summary>
        private GameObject theselectedobject;

        /// <summary>
        /// The barracks reference.
        /// </summary>
        private GameObject theBarracks;

        /// <summary>
        /// The unit that has been selected.
        /// </summary>
        private IUnit theUnit;

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
        /// Inverts the y so the drag screen will drag accordingly.
        /// <para>
        /// </para>
        /// <remarks>
        /// <paramref name="y"></paramref> -The number to subtract from the screen height.
        /// </remarks>
        /// </summary>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public static float InvertY(float y)
        {
            return Screen.height - y;
        }

        /// <summary>
        /// The check if selected function.
        /// Checks if the current game object is under the drag screen.
        /// <para></para>
        /// <remarks><paramref name="theunit"></paramref> -The object to check if it is under the drag screen.</remarks>
        /// </summary>
        public void CheckIfSelected(GameObject theunit)
        {
            if (theunit.GetComponent<Renderer>().isVisible && Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 camPos = Camera.main.WorldToScreenPoint(theunit.transform.position);
                camPos.y = InvertY(camPos.y);

                if (DragScreen.Contains(camPos) & !this.units.Contains(theunit))
                {
                    GameObject selectionsquare = theunit.transform.FindChild("SelectionHighlight").gameObject;
                    selectionsquare.GetComponent<MeshRenderer>().enabled = true;
                    selectionsquare.GetComponent<MeshRenderer>().material.color = Color.black;
                    this.units.Add(theunit);
                    UIManager.Self.CreateUnitButton(theunit);
                }

            }
        }

        /// <summary>
        /// The select all harvesters function.
        /// This function selects all harvesters on the map.
        /// </summary>
        public void SelectAllHarvesters()
        {
            this.ClearSelectedUnits();
            List<Harvester> units = FindObjectsOfType<Harvester>().ToList();
            foreach (Harvester h in units)
            {
                GameObject selectionsquare = h.gameObject.transform.FindChild("SelectionHighlight").gameObject;
                selectionsquare.GetComponent<MeshRenderer>().enabled = true;
                selectionsquare.GetComponent<MeshRenderer>().material.color = Color.black;
                this.units.Add(h.gameObject);
                UIManager.Self.CreateUnitButton(h.gameObject);
            }
        }

        /// <summary>
        /// The select all extractors function.
        /// This function selects all extractors on the map.
        /// </summary>
        public void SelectAllExtractors()
        {
            this.ClearSelectedUnits();
            List<Extractor> units = FindObjectsOfType<Extractor>().ToList();
            foreach (Extractor e in units)
            {
                GameObject selectionsquare = e.gameObject.transform.FindChild("SelectionHighlight").gameObject;
                selectionsquare.GetComponent<MeshRenderer>().enabled = true;
                selectionsquare.GetComponent<MeshRenderer>().material.color = Color.black;
                this.units.Add(e.gameObject);
                UIManager.Self.CreateUnitButton(e.gameObject);
            }
        }

        /// <summary>
        /// The select all miners function.
        /// This function selects all miners on the map.
        /// </summary>
        public void SelectAllMiners()
        {
            this.ClearSelectedUnits();
            List<Miner> units = FindObjectsOfType<Miner>().ToList();
            foreach (Miner m in units)
            {
                GameObject selectionsquare = m.gameObject.transform.FindChild("SelectionHighlight").gameObject;
                selectionsquare.GetComponent<MeshRenderer>().enabled = true;
                selectionsquare.GetComponent<MeshRenderer>().material.color = Color.black;
                this.units.Add(m.gameObject);
                UIManager.Self.CreateUnitButton(m.gameObject);
            }
        }

        /// <summary>
        /// The select all units function.
        /// This function selects all units on the map.
        /// </summary>
        public void SelectAllUnits()
        {
            this.ClearSelectedUnits();
            List<GameObject> units = GameObject.FindGameObjectsWithTag("Targetable").ToList();

            foreach (GameObject go in units)
            {
                if (go.GetComponent(typeof(IUnit)))
                {
                    GameObject selectionsquare = go.transform.FindChild("SelectionHighlight").gameObject;
                    selectionsquare.GetComponent<MeshRenderer>().enabled = true;
                    selectionsquare.GetComponent<MeshRenderer>().material.color = Color.black;
                    this.units.Add(go);
                    UIManager.Self.CreateUnitButton(go);
                }
            }
        }

        /// <summary>
        /// The cancel action function.
        /// Cancels the current action of the selected unit(s).
        /// </summary>
        public void CancelAction()
        {
            if (this.theselectedobject != null)
            {
                this.theUnit.SetTheMovePosition(this.theselectedobject.transform.position);
                this.theUnit.ChangeStates("Idle");
            }

            if (this.units.Count > 0)
            {
                foreach (GameObject go in this.units)
                {
                    if (go.GetComponent<NavMeshAgent>())
                    {
                        go.GetComponent<NavMeshAgent>().SetDestination(go.transform.position);
                        IUnit u = (IUnit)go.GetComponent(typeof(IUnit));
                        u.ChangeStates("Idle");
                    }
                }
            }
        }

        /// <summary>
        /// The call home function.
        /// This function sends the units back to the barracks.
        /// </summary>
        public void CallHome()
        {
            Vector3 doorposition = this.theBarracks.transform.FindChild("Door").position;
            
            this.SelectAllUnits();

            if (this.units.Count > 0)
            {
                // Make an offset to prevent units from pushing each other back and forth.
                int x = -1;
                int z = 0;
                float theoffset = Mathf.Sqrt(this.units.Count);

                for (int i = 0; i < this.units.Count; i++)
                {
                    // Increment x
                    x++;
                    // If the x is equal to the offset
                    if (x == (int)theoffset)
                    { // Increment z
                        z += 1;
                        // Set x back to 0
                        x = 0;
                    }

                    // Multiply but 1.5f to give an offset from each unit, this prevents jittering.
                    Vector3 thedestination = new Vector3(doorposition.x - (x * 1.5f), 0.5f, doorposition.z + (z * 1.5f));

                    if (this.units[i].GetComponent(typeof(IUnit)))
                    {
                        IUnit u = (IUnit)this.units[i].GetComponent(typeof(IUnit));
                        u.SetTheMovePosition(thedestination);
                        u.ChangeStates("Idle");
                    }
                }
              }
        }

        /// <summary>
        /// The start function.
        /// </summary>
        private void Start()
        {
            instance = this;
            this.theBarracks = GameObject.Find("Barracks");
            EventManager.Subscribe("ActivateAbility", this.ActivateAbility);
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
        /// The on destroy function.
        /// </summary>
        private void OnDestroy()
        {
            EventManager.UnSubscribe("ActivateAbility", this.ActivateAbility);
        }

        /// <summary>
        /// The select units function.
        /// This function is used for unit selection.
        /// </summary>
        private void SelectUnits()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
            {
                this.ClearSelectedUnits();
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    if (hit.transform.GetComponent(typeof(IUnit)))
                    {
                        this.theUnit = (IUnit)hit.transform.GetComponent(typeof(IUnit));
                        this.theselectedobject = hit.transform.gameObject;
                        GameObject selectionsquare = this.theselectedobject.transform.FindChild("SelectionHighlight").gameObject;
                        selectionsquare.GetComponent<MeshRenderer>().enabled = true;
                        selectionsquare.GetComponent<MeshRenderer>().material.color = Color.black;
                        //UIManager.Self.currentcooldown = this.theselectedobject.GetComponent<Stats>().CurrentSkillCooldown;
                        //UIManager.Self.abilityunit = this.theselectedobject;
                        UIManager.Self.CreateUnitButton(this.theselectedobject);
                    }
                }
            }
        }

        /// <summary>
        /// The activate drag screen function.
        /// This controls the drag screen.
        /// </summary>
        private void ActivateDragScreen()
        {
            if (Input.GetMouseButtonDown(0))
            {
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
        /// This clears the list of selected units and the current selected unit.
        /// </summary>
        private void ClearSelectedUnits()
        {
            if (this.units.Count > 0)
            {
                foreach (GameObject go in this.units)
                {
                    GameObject selectionsquare = go.transform.FindChild("SelectionHighlight").gameObject;
                    selectionsquare.GetComponent<MeshRenderer>().enabled = false;
                }

                UIManager.Self.ClearUnitButtonsList();
            }


            this.units.Clear();
            this.theUnit = null;

            if (this.theselectedobject != null)
            {
                GameObject selectionsquare = this.theselectedobject.transform.FindChild("SelectionHighlight").gameObject;
                selectionsquare.GetComponent<MeshRenderer>().enabled = false;
                this.theselectedobject = null;
                UIManager.Self.ClearUnitButtonsList();
            }
        }

        /// <summary>
        /// The command units function.
        /// Gives the units proper commands.
        /// </summary>
        private void CommandUnits()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && !EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    this.clickdestination = new Vector3(hit.point.x, 0.5f, hit.point.z);

                    this.HealUnit(hit);

                    if (hit.transform.GetComponent<Enemy>())
                    {
                        this.CommandToAttack(hit);
                    }
                    else if (hit.transform.GetComponent(typeof(IResources)))
                    {
                        this.CommandToHarvest(hit);
                    }
                    else if (hit.transform.tag == "PickUp")
                    {
                        this.CommandToPickUp(hit);
                    }
                    else if (hit.transform.gameObject.name == "Silo")
                    {
                        this.CommandToStock(hit);
                    }
                    else if (hit.transform.gameObject.name == "Decontamination")
                    {
                        this.CommandToDecontaminate(hit);
                    }
                    else
                    {
                        this.CommandToIdle(hit);
                    }
                }
            }
        }

        /// <summary>
        /// The command to attack function.
        /// Sends unit(s) to attack.
        /// <para></para>
        /// <remarks><paramref name="hit"></paramref> -The object that was hit by the ray cast.</remarks>
        /// </summary>
        private void CommandToAttack(RaycastHit hit)
        {
            // single check only one clicked
            if (this.theUnit != null)
            {
                this.theUnit.SetTheMovePosition(hit.transform.position);
                this.theUnit.SetTarget(hit.transform.gameObject);
                this.theUnit.ChangeStates("Battle");
            } 
            // multiple selected
            else if (this.units.Count > 0)
            {
                foreach (GameObject go in this.units)
                {
                    if (!go.GetComponent(typeof(IUnit)))
                    {
                        Debug.LogWarning(string.Format("hey, no component on {0}", go.name));
                    }
                    else
                    {
                        IUnit unit = (IUnit)go.GetComponent(typeof(IUnit));

                        unit.SetTheMovePosition(hit.transform.position);
                        unit.SetTarget(hit.transform.gameObject);
                        unit.ChangeStates("Battle");
                    }
                }
            }
        }

        /// <summary>
        /// The command to harvest function.
        /// Sends unit(s) to harvest a resource.
        /// <para></para>
        /// <remarks><paramref name="hit"></paramref> -The object that was hit by the ray cast.</remarks>
        /// </summary>
        private void CommandToHarvest(RaycastHit hit)
        {
            if (this.theUnit != null)
            {
               this.theUnit.SetTargetResource(hit.transform.gameObject);
            }
            else if (this.units.Count > 0)
            {
                foreach (GameObject go in this.units)
                {
                    if (!go.GetComponent(typeof(IUnit)))
                    {
                        Debug.LogWarning(string.Format("hey, no component on {0}", go.name));
                    }
                    else
                    {
                        IUnit unit = (IUnit)go.GetComponent(typeof(IUnit));
                        unit.SetTargetResource(hit.transform.gameObject);
                    }
                }
            }
        }

        /// <summary>
        /// The command to idle function.
        /// Send unit(s) to idle.
        /// <para></para>
        /// <remarks><paramref name="hit"></paramref> -The object that was hit by the ray cast.</remarks>
        /// </summary>
        private void CommandToIdle(RaycastHit hit)
        {
            // If the destination clicked is another unit, or the destination clicked is the barracks, just return and dont move.
            if (hit.transform.gameObject.GetComponent(typeof(IUnit)) || hit.transform.gameObject.name == "Barracks")
            {
                return;
            }

            // Send unit back to idle
            if (this.theUnit != null)
            {
                this.theUnit.SetTheMovePosition(this.clickdestination);
                this.theUnit.ChangeStates("Idle");
            }
            else if (this.units.Count > 0)
            {
                for (int i = 0; i < this.units.Count; i++)
                {
                    float angle = i * (2 * 3.14159f / this.units.Count);
                    float x = Mathf.Cos(angle) * 1.5f;
                    float z = Mathf.Sin(angle) * 1.5f;

                    this.clickdestination = new Vector3(this.clickdestination.x + x, 0.5f, this.clickdestination.z + z);

                    if (!this.units[i].GetComponent(typeof(IUnit)))
                    {
                        Debug.LogWarning(string.Format("hey, no component on {0}", this.units[i].name));
                    }
                    else
                    {
                        IUnit unit = (IUnit)this.units[i].GetComponent(typeof(IUnit));

                        unit.SetTheMovePosition(this.clickdestination);
                        unit.ChangeStates("Idle");
                    }
                }
            }
        }

        /// <summary>
        /// The command to pick up function.
        /// This function sends unit(s) to the pickup item.
        /// <para></para>
        /// <remarks><paramref name="hit"></paramref> -The object that was hit by the ray cast.</remarks>
        /// </summary>
        private void CommandToPickUp(RaycastHit hit)
        {
            if (this.theUnit != null)
            {
                this.theUnit.GoToPickup(hit.transform.gameObject);
            }
        }

        /// <summary>
        /// The command to stock function.
        /// Sends unit(s) to stock.
        /// <para></para>
        /// <remarks><paramref name="hit"></paramref> -The object that was hit by the ray cast.</remarks>
        /// </summary>
        private void CommandToStock(RaycastHit hit)
        {
            if (this.theUnit != null)
            {
                this.theUnit.SetTheMovePosition(this.clickdestination);
                this.theUnit.ChangeStates("Stock");
            }
            else if (this.units.Count > 0)
            {
                foreach (GameObject go in this.units)
                {
                    if (!go.GetComponent(typeof(IUnit)))
                    {
                        Debug.LogWarning(string.Format("hey, no component on {0}", go.name));
                    }
                    else
                    {
                        IUnit unit = (IUnit)go.GetComponent(typeof(IUnit));

                        unit.SetTheMovePosition(this.clickdestination);
                        unit.ChangeStates("Stock");
                    }
                }
            }
        }

        /// <summary>
        /// The command to decontaminate function.
        /// Send unit(s) to decontaminate.
        /// <para></para>
        /// <remarks><paramref name="hit"></paramref> -The object that was hit by the ray cast.</remarks>
        /// </summary>
        private void CommandToDecontaminate(RaycastHit hit)
        {
            if (this.theUnit != null)
            {
                if (this.theselectedobject.transform.Find("MineralsTainted") || this.theselectedobject.transform.Find("FoodTainted"))
                {
                    this.theUnit.ChangeStates("Decontaminate");
                    Transform thedoor = hit.transform.Find("FrontDoor");
                    Debug.Log(thedoor.name);
                    Vector3 destination = new Vector3(thedoor.position.x, 0.5f, thedoor.position.z);
                    this.theUnit.SetTheMovePosition(destination);
                }
            }
            else if (this.units.Count > 0)
            {
                foreach (GameObject go in this.units)
                {
                    if (!go.GetComponent(typeof(IUnit)))
                    {
                        Debug.LogWarning(string.Format("hey, no component on {0}", go.name));
                    }
                    else
                    {
                        IUnit unit = (IUnit)go.GetComponent(typeof(IUnit));

                        if (go.transform.Find("MineralsTainted") || go.transform.Find("FoodTainted"))
                        {
                            unit.ChangeStates("Decontaminate");
                            Transform thedoor = hit.transform.Find("FrontDoor");
                            Debug.Log(thedoor.name);
                            Vector3 destination = new Vector3(thedoor.position.x, 0.5f, thedoor.position.z);
                            unit.SetTheMovePosition(destination);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The heal unit function.
        /// Heals the unit with food.
        /// <para></para>
        /// <remarks><paramref name="hit"></paramref> -The object that was hit by the ray cast.</remarks>
        /// </summary>
        private void HealUnit(RaycastHit hit)
        {
            // The object hit is a unit and we have an instance of the cooked food to use
            if (hit.transform.gameObject.GetComponent(typeof(IUnit)) && UIManager.Self.foodinstance != null)
            {
                Stats stats = hit.transform.gameObject.GetComponent<Stats>();

                // If the unit can be healed
                if (stats.Health < stats.Maxhealth)
                {
                    // Heal unit
                    hit.transform.gameObject.GetComponent<Stats>().Health += 20;
                    
                    // Destroy the food
                    Destroy(UIManager.Self.foodinstance);
                }
            }
        }

        /// <summary>
        /// The Activate Ability function.
        /// Activates the ability of the selected unit.
        /// </summary>
        private void ActivateAbility()
        {
            if (this.theUnit != null)
            {
                this.theUnit.SpecialAbility();
            }
        }

        /// <summary>
        /// The On GUI function.
        /// This draws the drag screen to the screen.
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
