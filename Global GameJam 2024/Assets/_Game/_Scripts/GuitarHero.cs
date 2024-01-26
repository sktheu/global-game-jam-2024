using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GuitarHero : MonoBehaviour
{
    #region Global Variables
    [Header("Configurações:")]
    [SerializeField] public float speed = 6f;
    [SerializeField] private int id;
    [SerializeField] private bool greenArea = false;

    private bool _pressed = false;
    public static int CurrentPoints;

    //public Sprite leftMove
    //public Sprite upMove
    //public Sprite rightMove
    //public Sprite downMove

    private SpriteRenderer _spr;
    private BoxCollider2D _boxCol;
    #endregion

    #region Unity Functions
    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        _boxCol = GetComponent<BoxCollider2D>();

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
        _boxCol.offset = new Vector2(-0.23f, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        greenArea = false;

        if (collision.CompareTag("AreaAcerto"))
        {
            if (!_pressed)
            {
                //som de erro
                Debug.Log("Saiu");
            }
        }

        if(collision.CompareTag("DeleteArea"))
        {
            Destroy(gameObject);
            Debug.Log("Destruído");
        }
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
        if (greenArea)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && id == 1)
            {
                //som de acerto
                //_spr.sprite = leftMove;
                CurrentPoints++;
                _pressed = true;
                Destroy(gameObject);
                
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && id == 2)
            {
                //som de acerto
                //_spr.sprite = upMove;
                CurrentPoints++;
                _pressed = true;
                Destroy(gameObject);
                
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && id == 3)
            {
                //som de acerto
                //_spr.sprite = rightMove;
                CurrentPoints++;
                _pressed = true;
                Destroy(gameObject);
                
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && id == 4)
            {
                //som de acerto
                //_spr.sprite = downMove;
                CurrentPoints++;
                _pressed = true;
                Destroy(gameObject);
                
            }
            else 
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow)  || Input.GetKeyDown(KeyCode.UpArrow) ||
                    Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    //som de derrota
                    //mudar sprite sapa triste (se tiver)
                    //destruir
                    Debug.Log("Errou");
                    _pressed = true;
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
                //Ou seta
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


