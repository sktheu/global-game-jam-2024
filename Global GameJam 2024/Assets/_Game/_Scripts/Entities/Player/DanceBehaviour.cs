using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceBehaviour : MonoBehaviour
{
    #region Variáveis Globais
    // Unity Inspector:
    [SerializeField] private float stopDanceTime;

    public bool isDancing = false;
    private DanceMoves[] _curDanceMoves = new DanceMoves[4];

    private enum DanceMoves
    {
        Right = 1,
        Left = 2,
        Up = 3,
        Down = 4
    }
    #endregion

    #region Funções Unity
    private void Start() => ClearDance();

    private void Update()
    {
        if (Input.GetKeyDown("Right"))
            AddNewDanceMove(DanceMoves.Right);
        else if (Input.GetKeyDown("Left"))
            AddNewDanceMove(DanceMoves.Left);
        else if (Input.GetKeyDown("Up")) 
            AddNewDanceMove(DanceMoves.Up);
        else if (Input.GetKeyDown("Down"))
            AddNewDanceMove(DanceMoves.Down);
    }
    #endregion

    #region Funções Próprias
    private void AddNewDanceMove(DanceMoves newMove)
    {
        for (int i = 0; i < _curDanceMoves.Length; i++)
        {
            if (_curDanceMoves[i] == 0)
                _curDanceMoves[i] = newMove;
        }

        isDancing = true;
        StartCoroutine(StopDanceInterval(stopDanceTime));
    }
    
    private IEnumerator StopDanceInterval(float t)
    {
        yield return new WaitForSeconds(t);
        ClearDance();
        isDancing = false;
    }

    private void ClearDance()
    {
        for (int i = 0; i < _curDanceMoves.Length; i++)
            _curDanceMoves[i] = 0;
    }
    #endregion
}
