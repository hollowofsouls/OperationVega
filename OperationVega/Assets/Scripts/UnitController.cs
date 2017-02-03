
namespace Assets.Scripts
{
    using System.Collections.Generic;

    using UnityEngine;

    using UnityStandardAssets.ImageEffects;

    /// <summary>
    /// The unit controller.
    /// </summary>
    public class UnitController : MonoBehaviour
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static UnitController instance;

        /// <summary>
        /// The drag screen.
        /// </summary>
        private static Rect dragscreen = new Rect(0, 0, 0, 0);

        /// <summary>
        /// The selection highlight.
        /// </summary>
        [SerializeField]
        private Texture2D selectionHighlight;

        /// <summary>
        /// The start click.
        /// </summary>
        private Vector3 startclick = -Vector3.one;


        /// <summary>
        /// The the player.
        /// </summary>
        [SerializeField]
        private IUnit theUnit;

        [SerializeField]
        private Harvester thePlayer;

        /// <summary>
        /// The players.
        /// </summary>
        [SerializeField]
        private List<IGather> players = new List<IGather>();

        /// <summary>
        /// The player prefab.
        /// </summary>
        //[SerializeField]
        //private GameObject playerPrefab;

        /// <summary>
        /// Gets the self.
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
        /// Gets or sets the players.
        /// </summary>
        public List<IGather> Players
        {
            get
            {
                return this.players;
            }

            set
            {
                this.players = value;
            }
        }

        /// <summary>
        /// The invert y.
        /// </summary>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public static float InvertY(float y)
        {
            return Screen.height - y;
        }
 
        ///// <summary>
        ///// The spawn player.
        ///// </summary>
        //public void SpawnPlayer()
        //{
        //    GameObject player = Instantiate(this.playerPrefab, Vector3.zero, Quaternion.identity);
        //}

        /// <summary>
        /// The start.
        /// </summary>
        private void Start()
        {
            instance = this;
        }

        /// <summary>
        /// The update.
        /// </summary>
        private void Update()
        {
            this.SelectPlayer();
            this.MultiSelectPlayers();
            //this.ClearSelectedPlayers();
            //this.MoveDraggedUnits();
        }

        /// <summary>
        /// The shoot ray.
        /// </summary>
        /// <returns>
        /// The <see cref="Transform"/>.
        /// </returns>
        private Transform ShootRay()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            Physics.Raycast(ray.origin, ray.direction, out hit);

            if (hit.transform != null)
            {
                if (hit.transform.GetComponent(typeof(IUnit)))
                {
                    GameObject go = hit.transform.GetComponent(typeof(IUnit)).gameObject;
                    this.thePlayer = go.GetComponent<Harvester>();
                }

                return hit.transform;
            }
            return null;
        }

        /// <summary>
        /// The select player.
        /// </summary>
        private void SelectPlayer()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Transform objecthit = this.ShootRay();

                if (objecthit != null & this.thePlayer != null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit = new RaycastHit();

                    if (Physics.Raycast(ray.origin, ray.direction, out hit))
                    {
                        Vector3 destination = new Vector3(hit.point.x, 0.5f, hit.point.z);
                        this.thePlayer.TargetPosition = this.thePlayer.TargetPosition = destination;
                        this.thePlayer.TargetDirection = (this.thePlayer.TargetPosition - this.thePlayer.transform.position).normalized;
                    }
                }
            }
        }

        /// <summary>
        /// The multi select players.
        /// </summary>
        private void MultiSelectPlayers()
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

        ///// <summary>
        ///// The clear selected players.
        ///// </summary>
        //private void ClearSelectedPlayers()
        //{
        //    if (Input.GetKeyDown(KeyCode.Mouse1))
        //    {
        //        if (this.players != null)
        //        {
        //            foreach (IGather p in this.players)
        //            {
        //                p.TargetPos = p.transform;
        //            }
        //        }

        //        this.players.Clear();

        //        if (this.thePlayer != null)
        //        {
        //            this.thePlayer.TargetPos = this.thePlayer.transform;
        //            this.thePlayer = null;
        //        }
        //    }
        //}

        ///// <summary>
        ///// The move dragged units.
        ///// </summary>
        //private void MoveDraggedUnits()
        //{
        //    if (Input.GetKeyDown(KeyCode.Mouse0))
        //    {
        //        Transform target = this.ShootRay();

        //        if (this.players.Count > 0)
        //        {
        //            foreach (IGather p in this.players)
        //            {
        //                p.TargetPos = target;
        //                p.DirectionToTarget = (p.TargetPos.position - p.transform.position).normalized;
        //            }
        //        }
               
        //    }
        //}

        /// <summary>
        /// The on gui.
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
