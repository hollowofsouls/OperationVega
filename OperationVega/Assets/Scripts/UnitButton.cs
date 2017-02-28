
namespace Assets.Scripts
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// The unit button class.
    /// Handles functionality of the UnitButtons.
    /// </summary>
    public class UnitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject Unit;

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
        }
    }
}
