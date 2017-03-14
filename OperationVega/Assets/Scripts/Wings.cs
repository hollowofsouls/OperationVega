
namespace Assets.Scripts
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;

	using Assets.Scripts.BaseClasses;

	using UnityEngine;

	/// <summary>
	/// The wings  class.
	/// </summary>
	public class Wings : MonoBehaviour
	{
		/// <summary>
		/// The accessed.
		/// </summary>
		[SerializeField]
		public BaseWings Accessed;
		public void Create(BaseWings w)
		{
			Accessed = w;
		}
	}
}