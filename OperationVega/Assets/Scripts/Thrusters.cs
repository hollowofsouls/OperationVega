﻿
namespace Assets.Scripts
{
	using System.Linq;

	using Assets.Scripts.Interfaces;

	using UnityEngine;

	/// <summary>
	/// The thrusters class.
	/// </summary>
	public class Thrusters : MonoBehaviour, IRocketParts
	{
		/// <summary>
		/// The quality.
		/// </summary>
		private int quality;

		/// <summary>
		/// The steel.
		/// </summary>
		private int steel;

		/// <summary>
		/// The fuel.
		/// </summary>
		private int fuel;

		/// <summary>
		/// The ship.
		/// </summary>
		private Rocket ship;

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
			if (this.ship.PartList.OfType<Thrusters>().Any())
			{
				this.ship.PartList.Remove(this);

				// Debug.Log("Removed");
			}
			else if (!this.ship.PartList.OfType<Thrusters>().Any())
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
			this.fuel = 50;
			this.ship = FindObjectOfType<Rocket>();
		}

		/// <summary>
		/// Update is called once per frame
		/// </summary>
		private void Update()
		{
			if (Input.GetMouseButtonDown(1))
			{
				this.AddParts();
			}
		}
	}
}