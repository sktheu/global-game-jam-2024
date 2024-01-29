using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dance", menuName = "Scriptable Objects/Dance")]
public class Dance : ScriptableObject
{
    #region Vari�veis Globais
    public List<Moves> DanceMoves = new List<Moves>();

    public enum Moves
    {
        Empty,
        Right,
        Left,
        Up,
        Down
    }
    #endregion
}
