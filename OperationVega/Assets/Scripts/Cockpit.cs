﻿

namespace Assets.Scripts
{
	using System.Collections;
	using System.Collections.Generic;

	using Assets.Scripts.Interfaces;

	using UnityEngine;

	/// <summary>
	/// The cockpit.
	/// </summary>
	public class Cockpit : MonoBehaviour, IRocketParts
	{
		/// <summary>
		/// The capacity.
		/// </summary>
		private int capacity;

		/// <summary>
		/// The steel.
		/// </summary>
		private int steel;

		/// <summary>
		/// The fuel.
		/// </summary>
		private int fuel;

		/// <summary>
		/// The quality.
		/// </summary>
		private int quality;

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
		/// Use this for initialization
		/// </summary>
		private void Start()
		{
			this.quality = 20;
			this.steel = 200;
			this.fuel = 0;
			this.capacity = 20;
		}

		/// <summary>
		/// Update is called once per frame
		/// </summary>
		private void Update()
		{
		}
	}
}