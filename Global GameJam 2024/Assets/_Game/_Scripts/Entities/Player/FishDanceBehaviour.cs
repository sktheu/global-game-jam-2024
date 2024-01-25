using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDanceBehaviour : MonoBehaviour
{
    #region Variáveis Globais
    // Unity Inspector:
    [SerializeField] private float stopDanceTime;
    [SerializeField] private List<Dance.Moves> targetDanceMoves = new List<Dance.Moves>();

    // Componentes:
    private Animator _anim;

    private List<Dance.Moves> _curDanceMoves = new List<Dance.Moves>();
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
            AddNewDanceMove(Dance.Moves.Right);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            AddNewDanceMove(Dance.Moves.Left);
        else if (Input.GetKeyDown(KeyCode.UpArrow)) 
            AddNewDanceMove(Dance.Moves.Up);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            AddNewDanceMove(Dance.Moves.Down);
    }
    #endregion

    #region Funções Próprias
    private void AddNewDanceMove(Dance.Moves newMove)
    {
        for (int i = 0; i < _curDanceMoves.Count; i++)
        {
            if (_curDanceMoves[i] == Dance.Moves.Empty)
                _curDanceMoves[i] = newMove;
        }

        StartCoroutine(StopDanceInterval(stopDanceTime));

        if (_curDanceMoves.Count == targetDanceMoves.Count)
            VerifyDance();
    }
    
    private IEnumerator StopDanceInterval(float t)
    {
        yield return new WaitForSeconds(t);
        ClearDance();
    }

    private void ClearDance()
    {
        for (int i = 0; i < _curDanceMoves.Count; i++)
            _curDanceMoves[i] = Dance.Moves.Empty;
    }

    private bool VerifyDance()
    {
        for (int i = 0; i < _curDanceMoves.Count; i++)
        {
            if (_curDanceMoves[i] != targetDanceMoves[i])
                return false;
        }

        return true;

        //TODO: Concluir e Reiniciar
    }
    #endregion
}
