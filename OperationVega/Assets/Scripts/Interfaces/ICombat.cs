
namespace Assets.Scripts.Interfaces
{
    using UnityEngine;

    /// <summary>
    /// The Combat interface that allows an object to have combat functionality.
    /// </summary>
    public interface ICombat
    {
        /// <summary>
        /// The attack function that is used to attack an IUnit.
        /// </summary>
        void Attack();

        /// <summary>
        /// The take damage function allows an object to take damage.
        /// </summary>
        /// <param name="damage">
        /// The amount of damage.
        /// </param>
        void TakeDamage(uint damage);

        /// <summary>
        /// The change states function.
        /// This function changes the state to the passed in state.
        /// <para></para>
        /// <remarks><paramref name="destinationState"></paramref> -The state to transition to.</remarks>
        /// </summary>
        void ChangeStates(string destinationState);
    }
}
