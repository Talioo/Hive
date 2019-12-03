using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO Container", menuName = "Custom/SOContainer")]
public class SOContainer : ScriptableObject
{
    public SODuplicateCells duplicateCells;
    public SOHexaInfo hexaInfo;
}
