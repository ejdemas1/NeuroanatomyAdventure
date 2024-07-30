using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string item_name;
    public string description;
    public string recipe;
    public Sprite icon;
    public int quantity = 1;
}
