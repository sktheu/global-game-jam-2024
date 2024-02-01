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

    [Header("Sprites")]
    public Sprite leftMove;
    public Sprite upMove;
    public Sprite rightMove;
    public Sprite downMove;

    //[Header("Sfx")]
    //public AudioClip rightSfx, wrongSfx;

    //Components
    private SpriteRenderer _spr;
    private BoxCollider2D _boxCol;
    //private AudioSource _src;
    #endregion

    #region Unity Functions
    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        _boxCol = GetComponent<BoxCollider2D>();
        //_src = GetComponent<AudioSource>();

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
                //_src.clip = wrongSfx;
                //_src.Play();
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
                //_src.clip = rightSfx;
                //_src.Play();

                _spr.sprite = leftMove;
                CurrentPoints++;
                _pressed = true;
                Destroy(gameObject);
                
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && id == 2)
            {
                //_src.clip = rightSfx;
                //_src.Play();

                _spr.sprite = upMove;
                CurrentPoints++;
                _pressed = true;
                Destroy(gameObject);
                
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && id == 3)
            {
                //_src.clip = rightSfx;
                //_src.Play();

                _spr.sprite = rightMove;
                CurrentPoints++;
                _pressed = true;
                Destroy(gameObject);
                
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && id == 4)
            {
                //_src.clip = rightSfx;
                //_src.Play();

                _spr.sprite = downMove;
                CurrentPoints++;
                _pressed = true;
                Destroy(gameObject);
                
            }
            else 
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow)  || Input.GetKeyDown(KeyCode.UpArrow) ||
                    Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    //_src.clip = wrongSfx;
                    //_src.Play();
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
                _spr.sprite = leftMove;
                break;
            case 2:
                _spr.sprite = upMove;
                break;
            case 3:
                _spr.sprite = rightMove;
                break;
            default:
                _spr.sprite = downMove;
                break;
        }
    }

    #endregion
}


