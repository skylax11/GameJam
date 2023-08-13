using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName = "Items/Create Item")]
public class Item : ScriptableObject
{
    public new string name;
    public int id;
    public Sprite sprite;
    public GameObject Object;
}
