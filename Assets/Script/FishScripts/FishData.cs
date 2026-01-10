using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "Fish/FishData")]
public class FishData : ScriptableObject
{
    public string fishName;
    [TextArea] public string description;
    public Sprite image;
    public bool isDiscovered;
}
