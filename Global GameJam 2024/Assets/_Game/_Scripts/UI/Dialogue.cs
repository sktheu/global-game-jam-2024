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
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private float typeInterval;
    [SerializeField] private float enableDistance;

    [Header("Diálogos:")]
    [SerializeField] private string[] lines;

    // Componentes:
    private Image _panelImg;

    // Referências:
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
        _panelImg = GetComponent<Image>();
        _panelImg.enabled = false;
        tmp.enabled = false;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _playerTransform.position) <= enableDistance)
        {
            if (!_started)
            {
                _panelImg.enabled = true;
                tmp.enabled = true;

                tmp.text = String.Empty;
                StartDialogue();

                _started = true;
            }
        }
        else
        {
            _panelImg.enabled = false;
            tmp.enabled = false;

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
            tmp.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in lines[_dialogueIndex].ToCharArray())
        {
            tmp.text += c;
            yield return new WaitForSeconds(typeInterval);
        }
    }
    #endregion
}

