
namespace Assets.Scripts
{
	using System.Linq;

	using Assets.Scripts.Interfaces;

	using UnityEngine;

	/// <summary>
	/// The cockpit  class.
	/// </summary>
	public class Cockpit : MonoBehaviour, IRocketParts
	{
		/// <summary>
		/// The capacity.
		/// </summary>
		private int capacity;

		/// <summary>
		/// The steel.
		/// The amount of steel required to build the part.
		/// Accessible through the SteelCost property.
		/// </summary>
		private int steel;

		/// <summary>
		/// The fuel.
		/// The amount of Fuel required to build the part.
		/// Accessible through the FuelCost property.
		/// </summary>
		private int fuel;

		/// <summary>
		/// The quality.
		/// </summary>
		private int quality;

		/// <summary>
		/// The ship.
		/// </summary>
		private Rocket ship;

		/// <summary>
		/// Gets or sets the carrying.
		/// </summary>
		public int Carrying
		{
			get
			{
				return this.capacity;
			}

			set
			{
				this.capacity = value;
			}
		}

		/// <summary>
		/// Gets or sets the quality.
		/// </summary>
		public int Quality
		{
			get
			{
				return this.quality;
			}

			set
			{
				this.quality = value;
			}
		}

		/// <summary>
		/// Gets or sets the steel cost.
		/// </summary>
		public int SteelCost
		{
			get
			{
				return this.steel;
			}

			set
			{
				this.steel = value;
			}
		}

		/// <summary>
		/// Gets or sets the fuel cost.
		/// </summary>
		public int FuelCost
		{
			get
			{
				return this.fuel;
			}

			set
			{
				this.fuel = value;
			}
		}

		/// <summary>
		/// Function for adding the parts to the list.
		/// Need to work on removing parts if one of the same type is selected.
		/// </summary>
		public void AddParts()
		{
			if (this.ship.PartList.OfType<Cockpit>().Any())
			{
				foreach (IRocketParts go in this.ship.PartList)
				{
					if (go as Cockpit)
					{
						this.ship.PartList.Remove(go);
					}
				}
			}
			else if (!this.ship.PartList.OfType<Cockpit>().Any())
			{
				this.ship.PartList.Add(this);
				
				// Debug.Log("Added");
			}
		}

		/// <summary>
		/// Use this for initialization
		/// </summary>
		private void Start()
		{
			this.quality = 20;
			this.steel = 200;
			this.fuel = 0;
			this.capacity = 20;
			this.ship = FindObjectOfType<Rocket>();
		}

		/// <summary>
		/// Update is called once per frame
		/// </summary>
		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				this.AddParts();
			}
		}
	}
}