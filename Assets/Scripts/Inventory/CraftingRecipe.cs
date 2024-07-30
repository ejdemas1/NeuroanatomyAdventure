using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
	public List<Item> Materials;
	public List<Item> Results;

	public bool CanCraft(Inventory inventory)
	{
		return HasMaterials(inventory);
	}

	private bool HasMaterials(Inventory inventory)
	{
		foreach (Item material in Materials)
		{
			if (inventory.GetItemCount(material.item_name) < material.quantity)
			{
				Debug.LogWarning("You don't have the required materials.");
				return false;
			}
		}
		return true;
	}

	public void Craft(Inventory inventory)
	{
		if (CanCraft(inventory))
		{
			RemoveMaterials(inventory);
			AddResults(inventory);
		}
	}

	private void RemoveMaterials(Inventory inventory)
	{
		foreach (Item material in Materials)
		{
            inventory.RemoveItem(material.item_name, material.quantity);
		}
	}

	private void AddResults(Inventory inventory)
	{
		foreach (Item result in Results)
		{
			inventory.GetItem(result);
		}
	}
}
