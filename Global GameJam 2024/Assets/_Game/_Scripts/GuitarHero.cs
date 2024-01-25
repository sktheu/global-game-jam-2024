using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GuitarHero : MonoBehaviour
{
    #region Global Variables
    [SerializeField] public float speed = 6f;
    [SerializeField] private int id;
    [SerializeField] private bool greenArea = false;
    private int points;
    private bool pressed = false;

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
        //som de erro
        if (!pressed)
            Debug.Log("Saiu");
    }

    #endregion

    #region Custom Functions

    private void MoveLeft()
    {
        Vector3 actualPosition = transform.position;
        actualPosition.x -= speed * Time.deltaTime;
        transform.position = actualPosition;
    }

    /* Acrescentar pontos baseado se apertou a tecla certa
     * Talvez ir reduzindo a opacidade do passo aos poucos
     * Som de acerto e erro
     */
    private void PressKey()
    {
        if (greenArea)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && id == 1)
            {
                //som de acerto
                points++;
                pressed = true;
                Destroy(gameObject);
                
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && id == 2)
            {
                //som de acerto
                points++;
                pressed = true;
                Destroy(gameObject);
                
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && id == 3)
            {
                //som de acerto
                points++;
                pressed = true;
                Destroy(gameObject);
                
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && id == 4)
            {
                //som de acerto
                points++;
                pressed = true;
                Destroy(gameObject);
                
            }
            else 
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow)  || Input.GetKeyDown(KeyCode.UpArrow) ||
                    Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    //som de derrota
                    //destruir
                    Debug.Log("Errou");
                    pressed = true;
                }
            }

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


