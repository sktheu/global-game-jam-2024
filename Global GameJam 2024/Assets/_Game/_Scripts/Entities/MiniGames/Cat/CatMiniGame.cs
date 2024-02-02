using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMiniGame : MonoBehaviour
{
    #region Variáveis Globais
    [Header("Configurações:")] 
    [SerializeField] private int minMovesTarget;
    [SerializeField] private int maxMovesTarget;
    [SerializeField] private float movesInterval;
    [SerializeField] private int dancesMaxCount;
    [SerializeField] private int targetDanceInterval;

    private List<Moves> _curDance = new List<Moves>();
    private int _curMoveCount = 0;
    private enum Moves { Right = 1, Left = 2, Up = 3, Down = 4 }

    private Moves[] _targetDance;

    private int _curDancesCount = 0;
    #endregion

    #region Funções Unity
    private void Start() => SetNewTargetDance();

    private void Update()
    {
        if (_curMoveCount < _targetDance.Length)
            GetDanceInput();
        else
            VerifyDance();

        if (Input.GetKeyDown(KeyCode.Escape))
            Stop();
    }
    #endregion

    #region Funções Próprias
    private void GetDanceInput()
    {
        if (Input.GetKeyDown(KeyCode.D))
            AddNewMove(Moves.Right);
        else if (Input.GetKeyDown(KeyCode.A))
            AddNewMove(Moves.Left);
        else if (Input.GetKeyDown(KeyCode.W))
            AddNewMove(Moves.Up);
        else if (Input.GetKeyDown(KeyCode.S))
            AddNewMove(Moves.Down);
    }

    private void AddNewMove(Moves move)
    {
        StopCoroutine(SetMovesInterval(movesInterval));
        _curDance.Add(move);
        _curMoveCount++;
        StartCoroutine(SetMovesInterval(movesInterval));
    }

    private void VerifyDance()
    {
        StopAllCoroutines();
        if (_curDance.Count > _targetDance.Length)
            ClearDance();

        for (int i = 0; i < _curDance.Count; i++)
        {
            if (_curDance[i] != _targetDance[i])
            {
                ClearDance();
                SetNewTargetDance();
            }
        }

        if (_curDancesCount < dancesMaxCount)
        {
            ClearDance();
            SetNewTargetDance();
            _curDancesCount++;
        }
        else
        {
            Complete();
        }
    }

    private void ClearDance()
    {
        _curDance = new List<Moves>();
        _curMoveCount = 0;
    }

    private IEnumerator SetMovesInterval(float t)
    {
        yield return new WaitForSeconds(t);
        ClearDance();
    }

    private void Complete()
    {
        // TODO: Ativar Flag 
    }

    private void Stop()
    {
        // TODO: Voltar a floresta
    }

    private void SetNewTargetDance()
    {
        _targetDance = new Moves[Random.Range(minMovesTarget, maxMovesTarget+1)];

        for (int i = 0; i < _targetDance.Length; i++)
            _targetDance[i] = (Moves)Random.Range(1, 4+1);

        StartCoroutine(SetTargetDanceInterval(targetDanceInterval));
    }

    private IEnumerator SetTargetDanceInterval(float t)
    {
        yield return new WaitForSeconds(t);
        ClearDance();
        SetNewTargetDance();
    }
    #endregion
}
