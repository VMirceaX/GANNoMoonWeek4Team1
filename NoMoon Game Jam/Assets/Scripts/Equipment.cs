using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Equipment : ScriptableObject
{
    public string itemName;
    public float weight;
    public Sprite image;
    public Mesh mesh;
}
