using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    #region Vari�veis Globais
    // Unity Inspector:
    [Header("Configura��es:")]
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private float typeInterval;
    [SerializeField] private Image panelImg;
    [SerializeField] private float enableDistance;
    [SerializeField] private MonoBehaviour nextScript;

    [Header("Di�logos:")]
    [SerializeField] private string[] lines;

    // Refer�ncias:
    private static Transform _playerTransform;

    private int _dialogueIndex;
    private bool _started = false;
    #endregion

    #region Fun��es Unity
    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        panelImg.enabled = false;
        tmp.enabled = false;
    }

    private void Update()
    {
        if (_started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                NextLine();
        }

        if (Vector3.Distance(transform.position, _playerTransform.position) <= enableDistance)
        {
            if (!_started)
            {
                panelImg.enabled = true;
                tmp.enabled = true;

                tmp.text = String.Empty;
                StartDialogue();

                _started = true;
            }
        }
        else
        {
            panelImg.enabled = false;
            tmp.enabled = false;

            _started = false;
        }
    }
    #endregion

    #region Fun��es Pr�prias
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
        else
        {
            panelImg.enabled = false;
            tmp.enabled = false;

            _started = false;
            nextScript.enabled = true;
            this.enabled = false;
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

