using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDanceBehaviour : MonoBehaviour
{
    #region Variáveis Globais
    // Unity Inspector:
    [SerializeField] private float minDanceInterval;
    [SerializeField] private float maxDanceInterval;
    [SerializeField] private Dance[] possibleDances = new Dance[4];
    [SerializeField] private List<Dance.Moves> curTargetDanceMoves = new List<Dance.Moves>();
    [SerializeField] private int maxCompletedDancesCount;

    // Componentes:
    private Animator _anim;
    private PlayerMovement _playerMovement;

    private List<Dance.Moves> _curDanceMoves = new List<Dance.Moves>();

    private int _curCompletedDancesCount = 0;
    #endregion

    #region Funções Unity
    private void Start()
    {
        _anim = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().flipX = false;
        _curCompletedDancesCount = 0;
        ChooseNewDance();
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

        if (_curDanceMoves.Count == curTargetDanceMoves.Count)
            VerifyDance();
    }

    private void ClearDance()
    {
        for (int i = 0; i < _curDanceMoves.Count; i++)
            _curDanceMoves[i] = Dance.Moves.Empty;
    }

    private void VerifyDance()
    {
        for (int i = 0; i < _curDanceMoves.Count; i++)
        {
            if (_curDanceMoves[i] != curTargetDanceMoves[i])
                ClearDance();
        }

        if (_curCompletedDancesCount < maxCompletedDancesCount)
        {
            StopAllCoroutines();
            _curCompletedDancesCount++;
            ClearDance();
            ChooseNewDance();
        }
        else
        {
            CompleteDance();
        }
    }

    private void ChooseNewDance()
    {
        curTargetDanceMoves = possibleDances[Random.Range(0, possibleDances.Length)].DanceMoves;
        StartCoroutine(StartNewDanceTimer(Random.Range(minDanceInterval, maxDanceInterval)));
    }

    private IEnumerator StartNewDanceTimer(float t)
    {
        yield return new WaitForSeconds(t);
        _curCompletedDancesCount = 0;
        ClearDance();
        FailDance();
    }

    private void FailDance()
    {
        _playerMovement.enabled = true;
        this.enabled = false;
    }

    private void CompleteDance()
    {
        _playerMovement.enabled = true;
    }
    #endregion
}
