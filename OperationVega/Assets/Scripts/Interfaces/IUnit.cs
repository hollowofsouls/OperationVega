
namespace Assets.Scripts.Interfaces
{
    using UnityEngine;

    /// <summary>
    /// The Unit interface to represent what it means to be a Unit.
    /// </summary>
    public interface IUnit
    {
        /// <summary>
        /// The harvest function allows an object to harvest a resource.
        /// </summary>
        void Harvest();

        /// <summary>
        /// The set target function.
        /// Sets the object as the target for the unit.
        /// </summary>
        /// <param name="theTarget">
        /// The target to set.
        /// </param>
        void SetTarget(GameObject theTarget);

        /// <summary>
        /// The set target resource function.
        /// The function sets the unit with the resource.
        /// </summary>
        /// <param name="theResource">
        /// The resource to set the unit to go to.
        /// </param>
        void SetTargetResource(GameObject theResource);

        /// <summary>
        /// The set move position function.
        /// Sets the destination for the unit.
        /// </summary>
        /// <param name="theClickPosition">
        /// The click position to move to.
        /// </param>
        void SetTheMovePosition(Vector3 theClickPosition);

        /// <summary>
        /// The go to pickup function.
        /// Parses and sends the unit to pickup a dropped resource.
        /// </summary>
        /// <param name="thepickup">
        /// The item to pickup.
        /// </param>
        void GoToPickup(GameObject thepickup);

        /// <summary>
        /// The change states function.
        /// This function changes the state to the passed in state.
        /// <para></para>
        /// <remarks><paramref name="destinationState"></paramref> -The state to transition to.</remarks>
        /// </summary>
        void ChangeStates(string destinationState);
    }
}
