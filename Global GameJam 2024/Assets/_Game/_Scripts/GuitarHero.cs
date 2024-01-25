using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GuitarHero : MonoBehaviour
{
    #region Global Variables
    [SerializeField] public float speed = 6f;
    [SerializeField] private bool greenArea = false;
    private int id;

    private SpriteRenderer _spr;
    #endregion

    #region Unity Functions
    void Start()
    {
        _spr = GetComponent<SpriteRenderer>();

        //1 = Esquerda, 2 = Cima, 3 = Direita, 4 = Baixo
        id = Random.Range(1, 4);
        ChangeSprite();
    }

    private void Update()
    {
        PressKey();
    }

    private void FixedUpdate()
    {
        MoveLeft();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        greenArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        greenArea = false;
    }

    #endregion

    #region Custom Functions

    private void MoveLeft()
    {
        Vector3 actualPosition = transform.position;
        actualPosition.x -= speed * Time.deltaTime;
        transform.position = actualPosition;
    }


    private void PressKey()
    {
        if (greenArea && Input.GetKeyDown("space"))
        {
            Destroy(gameObject);
            /* Acrescentar pontos baseado se apertou a tecla certa
             * Talvez ir reduzindo a opacidade do passo aos poucos
             */
        }

    }

    private void ChangeSprite()
    {
        //Troca a sprite baseada na id do objeto
        switch (id)
        {
            case 1:
                //_spr.sprite = dançaesquerda;
                break;
            case 2:
                //cima
                break;
            case 3:
                //direita
                break;
            default:
                //baixo
                break;
        }
    }

    #endregion
}


