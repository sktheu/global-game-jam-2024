using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDanceBehaviour : MonoBehaviour
{
    #region Variáveis Globais
    // Unity Inspector:
    [Header("Configurações:")]
    [SerializeField] private float stopDanceTime;
    [SerializeField] private List<Dance.Moves> targetDanceMoves = new List<Dance.Moves>();

    [Header("Diálogos:")] 
    [SerializeField] private Dialogue initialDialogue;
    [SerializeField] private Dialogue winDialogue;

    // Componentes:
    private Animator _anim;
    private PlayerMovement _playerMovement;

    private List<Dance.Moves> _curDanceMoves = new List<Dance.Moves>();

    private static bool _win = false;
    #endregion

    #region Funções Unity
    private void Start()
    {
        if (_win)
        {
            winDialogue.enabled = true;
            this.enabled = false;
        }

        _anim = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().flipX = false;
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

    private void VerifyDance()
    {
        for (int i = 0; i < _curDanceMoves.Count; i++)
        {
            if (_curDanceMoves[i] != targetDanceMoves[i])
            {
                ClearDance();
                FailDance();
            }
        }
        
        CompleteDance();
    }

    private void FailDance()
    {
        _playerMovement.enabled = true;
        this.enabled = false;
        initialDialogue.enabled = true;
    }

    private void CompleteDance()
    {
        _playerMovement.enabled = true;
        winDialogue.enabled = true;
    }
    #endregion
}
