using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dance", menuName = "Scriptable Objects/Dance")]
public class Dance : ScriptableObject
{
    public DanceBehaviour.DanceMoves[] danceMoves = new DanceBehaviour.DanceMoves[4];
}
