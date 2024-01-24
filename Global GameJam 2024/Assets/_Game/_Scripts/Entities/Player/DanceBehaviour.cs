using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceBehaviour : MonoBehaviour
{
    #region Variáveis Globais
    // Unity Inspector:
    [SerializeField] private float stopDanceTime;

    // Componentes:
    private Animator _anim;

    public bool isDancing = false;
    private List<DanceMoves> _curDanceMoves = new List<DanceMoves>();

    public enum DanceMoves
    {
        Right = 1,
        Left = 2,
        Up = 3,
        Down = 4
    }
    #endregion

    #region Funções Unity
    private void Start()
    {
        _anim = GetComponent<Animator>();
        ClearDance();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            AddNewDanceMove(DanceMoves.Right);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            AddNewDanceMove(DanceMoves.Left);
        else if (Input.GetKeyDown(KeyCode.UpArrow)) 
            AddNewDanceMove(DanceMoves.Up);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            AddNewDanceMove(DanceMoves.Down);
    }
    #endregion

    #region Funções Próprias
    private void AddNewDanceMove(DanceMoves newMove)
    {
        for (int i = 0; i < _curDanceMoves.Count; i++)
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
        for (int i = 0; i < _curDanceMoves.Count; i++)
            _curDanceMoves[i] = 0;
    }

    public bool VerifyDance(Dance targetDance)
    {
        if (_curDanceMoves.Count < targetDance.danceMoves.Length)
            return false;

        for (int i = 0; i < _curDanceMoves.Count; i++)
        {
            if (_curDanceMoves[i] != targetDance.danceMoves[i])
                return false;
        }

        return true;
    }
    #endregion
}
