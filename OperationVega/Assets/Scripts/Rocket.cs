
namespace Assets.Scripts
{
	using System.Collections.Generic;
	using System.Linq;

	using Assets.Scripts.Interfaces;

	using UnityEngine;

	/// <summary>
	/// The rocket class.
	/// </summary>
	public class Rocket : MonoBehaviour
	{
		/// <summary>
		/// The all parts.
		/// </summary>
		private List<IRocketParts> allParts;

		/// <summary>
		/// The total quality.
		/// </summary>
		private int totalQuality;

		private Cockpit tempCockpit;

		private Chassis tempChassis;

		private Wings tempWings;

		private Thrusters tempThrusters;

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
		/// Gets or sets the total quality.
		/// </summary>
		public int FullQuality
		{
			get
			{
				return this.totalQuality;
			}

			set
			{
				this.totalQuality = value;
			}
		}

		/// <summary>
		/// The ship build.
		/// </summary>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public bool ShipBuild()
		{
			if (this.allParts.OfType<Cockpit>().Any() &&
				this.allParts.OfType<Chassis>().Any() &&
				this.allParts.OfType<Thrusters>().Any() &&
				this.allParts.OfType<Wings>().Any())
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Function for adding a cockpit to the list.
		/// </summary>
		/// <param name="secondaryList">
		/// The secondaryList. A second list of the parts.
		/// Solves error that's thrown when a part is removed from the list.
		/// </param>
		/// <param name="selectedParts">
		/// The selected Parts.
		/// </param>
		public void AddCockpit(List<IRocketParts> secondaryList, Cockpit selectedParts)
		{
			if (User.SteelCount >= selectedParts.SteelCost && User.FuelCount >= selectedParts.FuelCost)
			{
				if (this.allParts.OfType<Cockpit>().Any())
				{
					foreach (var go in secondaryList)
					{
						if (go as Cockpit)
						{
							this.allParts.Remove(go);
						}
					}
				}
				else if (!this.allParts.OfType<Cockpit>().Any())
				{
					this.allParts.Add(selectedParts);
					User.SteelCount -= selectedParts.SteelCost;
					User.FuelCount -= selectedParts.FuelCost;
				}
			}

			if (User.SteelCount < selectedParts.SteelCost)
			{
				Debug.Log("You don't have enough steel.");
			}

			if (User.FuelCount < selectedParts.FuelCost)
			{
				Debug.Log("You don't have enough fuel.");
			}
		}

		/// <summary>
		/// Function for adding a chassis to the list.
		/// </summary>
		/// <param name="secondaryList">
		/// The secondaryList. A second list of the parts.
		/// </param>
		/// <param name="selectedParts">
		/// The selected Parts.
		/// </param>
		public void AddChassis(List<IRocketParts> secondaryList, Chassis selectedParts)
		{
			if (User.SteelCount >= selectedParts.SteelCost && User.FuelCount >= selectedParts.FuelCost)
			{
				if (this.allParts.OfType<Chassis>().Any())
				{
					foreach (var go in secondaryList)
					{
						if (go as Chassis)
						{
							this.allParts.Remove(go);
						}
					}
				}
				else if (!this.allParts.OfType<Chassis>().Any())
				{
					this.allParts.Add(selectedParts);
					User.SteelCount -= selectedParts.SteelCost;
					User.FuelCount -= selectedParts.FuelCost;
				}
			}

			if (User.SteelCount < selectedParts.SteelCost)
			{
				Debug.Log("You don't have enough steel.");
			}

			if (User.FuelCount < selectedParts.FuelCost)
			{
				Debug.Log("You don't have enough fuel.");
			}
		}

		/// <summary>
		/// Function for adding wings to the list.
		/// </summary>
		/// <param name="secondaryList">
		/// The secondaryList. A second list of the parts.
		/// </param>
		/// <param name="selectedParts">
		/// The selected Parts.
		/// </param>
		public void AddWings(List<IRocketParts> secondaryList, Wings selectedParts)
		{
			if (User.SteelCount >= selectedParts.SteelCost && User.FuelCount >= selectedParts.FuelCost)
			{
				if (this.allParts.OfType<Wings>().Any())
				{
					foreach (var go in secondaryList)
					{
						if (go as Wings)
						{
							this.allParts.Remove(go);
						}
					}
				}
				else if (!this.allParts.OfType<Wings>().Any())
				{
					this.allParts.Add(selectedParts);
					User.SteelCount -= selectedParts.SteelCost;
					User.FuelCount -= selectedParts.FuelCost;
				}
			}

			if (User.SteelCount < selectedParts.SteelCost)
			{
				Debug.Log("You don't have enough steel.");
			}

			if (User.FuelCount < selectedParts.FuelCost)
			{
				Debug.Log("You don't have enough fuel.");
			}
		}

		/// <summary>
		/// Function for adding thrusters to the list.
		/// </summary>
		/// <param name="secondaryList">
		/// The secondaryList. A second list of the parts.
		/// </param>
		/// <param name="selectedParts">
		/// The selected Parts.
		/// </param>
		public void AddThrusters(List<IRocketParts> secondaryList, Thrusters selectedParts)
		{
			if (User.SteelCount >= selectedParts.SteelCost && User.FuelCount >= selectedParts.FuelCost)
			{
				if (this.allParts.OfType<Thrusters>().Any())
				{
					foreach (var go in secondaryList)
					{
						if (go as Thrusters)
						{
							this.allParts.Remove(go);
						}
					}
				}
				else if (!this.allParts.OfType<Thrusters>().Any())
				{
					this.allParts.Add(selectedParts);
					User.SteelCount -= selectedParts.SteelCost;
					User.FuelCount -= selectedParts.FuelCost;
				}
			}

			if (User.SteelCount < selectedParts.SteelCost)
			{
				Debug.Log("You don't have enough steel.");
			}

			if (User.FuelCount < selectedParts.FuelCost)
			{
				Debug.Log("You don't have enough fuel.");
			}
		}

		/// <summary>
		/// Use this for initialization
		/// </summary>
		private void Start()
		{
			this.allParts = new List<IRocketParts>();
			this.tempCockpit = FindObjectOfType<Cockpit>();
			this.tempChassis = FindObjectOfType<Chassis>();
			this.tempWings = FindObjectOfType<Wings>();
			this.tempThrusters = FindObjectOfType<Thrusters>();
		}

		/// <summary>
		/// Update is called once per frame
		/// </summary>
		private void Update()
		{
			var spareList = this.allParts.ToList();

			if (Input.GetMouseButtonDown(0))
			{
				this.AddCockpit(spareList, this.tempCockpit);
			}

			if (Input.GetMouseButtonDown(1))
			{
				this.AddChassis(spareList, this.tempChassis);
			}

			if (Input.GetKeyDown(KeyCode.Keypad1))
			{
				this.AddWings(spareList, this.tempWings);
			}

			if (Input.GetKeyDown(KeyCode.Keypad2))
			{
				this.AddThrusters(spareList, this.tempThrusters);
			}

			if (Input.GetKeyDown(KeyCode.A))
			{
				User.SteelCount += 100;
			}

			if (Input.GetKeyDown(KeyCode.S))
			{
				User.FuelCount += 100;
			}
			Debug.Log(this.ShipBuild());
			Debug.Log(this.allParts.Count);
		}
	}
}