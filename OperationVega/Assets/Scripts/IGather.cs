
namespace Assets.Scripts
{   
    /// <summary>
    /// The Gather interface to allow an object to implement gathering.
    /// </summary>
    public interface IGather
    {
        /// <summary>
        /// The harvest function allows an object to harvest a resource.
        /// </summary>
        void Harvest();

        /// <summary>
        /// The decontaminate function allows decontamination of a resource.
        /// </summary>
        void Decontaminate();
    }
}
