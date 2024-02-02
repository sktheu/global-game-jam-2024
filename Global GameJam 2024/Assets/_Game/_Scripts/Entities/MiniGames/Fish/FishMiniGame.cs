using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMiniGame : MonoBehaviour
{
    [Header("Configurações:")] 
    [SerializeField] private Moves[] targetDance;
    [SerializeField] private float movesInterval;

    private List<Moves> _curDance = new List<Moves>();
    private int _curMoveCount = 0;
    private enum Moves { Right, Left, Up, Down }

    private void Update()
    {
        if (_curMoveCount < targetDance.Length)
            GetDanceInput();
        else
            VerifyDance();

        if (Input.GetKeyDown(KeyCode.Escape))
            Stop();
    }

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
        _curDance.Add(move);
        _curMoveCount++;
        StartCoroutine(SetMovesInterval(movesInterval));
    }

    private void VerifyDance()
    {
        if (_curDance.Count > targetDance.Length)
            ClearDance();

        for (int i = 0; i < _curDance.Count; i++)
        {
            if (_curDance[i] != targetDance[i])
                ClearDance();
        }

        Complete();
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
}
