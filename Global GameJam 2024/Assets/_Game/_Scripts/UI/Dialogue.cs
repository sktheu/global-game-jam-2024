using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    #region Variáveis Globais
    // Unity Inspector:
    [Header("Configurações:")]
    [SerializeField] private float typeInterval;
    [SerializeField] private float enableDistance;

    [Header("Referências:")] 
    [SerializeField] private TextMeshProUGUI tempText;
    [SerializeField] private Image background;

    [Header("Diálogos:")]
    [SerializeField] private string[] lines;
    [SerializeField] private NPCTrigger _npcTrigger;

    private static Transform _playerTransform;

    private int _dialogueIndex;
    private bool _started = false;
    #endregion

    #region Funções Unity
    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        background.enabled = false;
        tempText.enabled = false;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _playerTransform.position) <= enableDistance)
        {
            if (!_started)
            {
                background.enabled = true;
                tempText.enabled = true;

                tempText.text = String.Empty;
                StartDialogue();

                _started = true;
            }
        }
        else
        {
            background.enabled = false;
            tempText.enabled = false;

            _started = false;
        }

        if (_started)
        {
            if (Input.GetButtonDown("Action"))
                NextLine();
        }
    }
    #endregion

    #region Funções Próprias
    private void StartDialogue()
    {
        _dialogueIndex = 0;
        StartCoroutine(TypeLine());
    }

    private void NextLine()
    {
        if (_dialogueIndex < lines.Length - 1)
        {
            _dialogueIndex++;
            tempText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            tempText.enabled = false;
            background.enabled = false;

            if (_npcTrigger != null)
                _npcTrigger.ActivateMiniGame();
        }
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in lines[_dialogueIndex].ToCharArray())
        {
            tempText.text += c;
            yield return new WaitForSeconds(typeInterval);
        }
    }
    #endregion
}

