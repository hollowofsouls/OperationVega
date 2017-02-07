

namespace Assets.Scripts
{
	using System.Collections;
	using System.Collections.Generic;

	using Assets.Scripts.Interfaces;

	using UnityEngine;

	/// <summary>
	/// The rocket.
	/// </summary>
	public class Rocket : MonoBehaviour
	{
		/// <summary>
		/// The all parts.
		/// </summary>
		private List<IRocketParts> allParts;

		/// <summary>
		/// Gets or sets the part list.
		/// </summary>
		public List<IRocketParts> PartList
		{
			get
			{
				return this.allParts;
			}

			set
			{
				this.allParts = value;
			}
		}

		/// <summary>
		/// Use this for initialization
		/// </summary>
		private void Start()
		{
			this.allParts = new List<IRocketParts>();
		}

		/// <summary>
		/// Update is called once per frame
		/// </summary>
		private void Update()
		{
			Debug.Log(this.allParts.Count);
		}
	}
}