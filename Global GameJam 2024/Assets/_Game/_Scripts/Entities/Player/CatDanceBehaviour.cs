using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.UI;
using UnityEngine;
using UnityEngine.UI;

public class CatDanceBehaviour : MonoBehaviour
{
    #region Variáveis Globais
    // Unity Inspector:
    [Header("Configurações:")]
    [SerializeField] private float minDanceInterval;
    [SerializeField] private float maxDanceInterval;
    [SerializeField] private Dance[] possibleDances = new Dance[4];
    [SerializeField] private List<Dance.Moves> curTargetDanceMoves = new List<Dance.Moves>();
    [SerializeField] private int maxCompletedDancesCount;

    [Header("Referências:")]
    [SerializeField] private GameObject miniGameParent;
    [SerializeField] private Image[] imgMoves = new Image[4];
    [SerializeField] private Sprite[] imgMovesSprites = new Sprite[4];
    [SerializeField] private RectTransform rectTimeBar;

    [Header("Diálogos:")] 
    [SerializeField] private Dialogue initialDialogue;
    [SerializeField] private Dialogue winDialogue;

    [Header("Posição:")] 
    [SerializeField] private Transform miniGamePoint;

    // Componentes:
    private Animator _playerAnim;
    private PlayerMovement _playerMovement;

    // Dança
    private List<Dance.Moves> _curDanceMoves = new List<Dance.Moves>();
    private int _curCompletedDancesCount = 0;

    // Barra de tempo
    private float _initialTime = 0;
    private float _initialWidth = 0;
    private float _curTime = 0;

    // Completou
    private static bool _win = false;
    #endregion

    #region Funções Unity
    private void Start()
    {
        miniGameParent.SetActive(true);
        transform.position = miniGamePoint.position;

        if (_win)
        {
            winDialogue.enabled = true;
            this.enabled = false;
        }

        //_playerAnim = GetComponent<Animator>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _playerMovement.enabled = false;
        _playerMovement.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _playerMovement.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        _curCompletedDancesCount = 0;
        ChooseNewDance();
    }
      
    private void Update()
    {
        // Input Dança
        if (Input.GetKeyDown(KeyCode.RightArrow))
            AddNewDanceMove(Dance.Moves.Right);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            AddNewDanceMove(Dance.Moves.Left);
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            AddNewDanceMove(Dance.Moves.Up);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            AddNewDanceMove(Dance.Moves.Down);

        // Mostrar Tempo para completar
        rectTimeBar.sizeDelta = new Vector2(_curTime * _initialWidth / _initialTime, rectTimeBar.sizeDelta.y);
        _curTime -= Time.deltaTime;
    }
    #endregion

    #region Funções Próprias
    private void AddNewDanceMove(Dance.Moves newMove)
    {
        for (int i = 0; i < _curDanceMoves.Count; i++)
        {
            if (_curDanceMoves[i] == Dance.Moves.Empty)
            {
                _curDanceMoves[i] = newMove;
                SetNewMoveImg(i, newMove);
            }
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
        var newTime = Random.Range(minDanceInterval, maxDanceInterval);
        _curTime = newTime;
        StartCoroutine(StartNewDanceTimer(newTime));
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
        _curTime = 0;
        _playerMovement.enabled = true;
        this.enabled = false;
        initialDialogue.enabled = true;
    }

    private void CompleteDance()
    {
        _playerMovement.enabled = true;
        winDialogue.enabled = true;
        _win = true;
    }

    private void SetNewMoveImg(int index, Dance.Moves direction)
    {
        // 0 => Passo Direita, 1 => Passo Esquerda, 2 => Passo Cima, 3 => Passo Baixo

        switch (direction)
        {
            case Dance.Moves.Right:
                imgMoves[index].sprite = imgMovesSprites[0];
                break;

            case Dance.Moves.Left:
                imgMoves[index].sprite = imgMovesSprites[1];
                break;

            case Dance.Moves.Up:
                imgMoves[index].sprite = imgMovesSprites[2];
                break;

            case Dance.Moves.Down:
                imgMoves[index].sprite = imgMovesSprites[3];
                break;
        }
    }
    #endregion
}
