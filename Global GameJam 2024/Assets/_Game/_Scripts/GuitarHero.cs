using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GuitarHero : MonoBehaviour
{
    public float speed = 6f;
    [SerializeField] private bool greenArea = false;


    void Start()
    {


    }

    private void Update()
    {
        PressKey();
    }

    private void FixedUpdate()
    {
        MoveLeft();
    }

    private void MoveLeft()
    {
        Vector3 actualPosition = transform.position;
        actualPosition.x -= speed * Time.deltaTime;
        transform.position = actualPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        greenArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        greenArea = false;
    }

    private void PressKey()
    {
        if (greenArea && Input.GetKeyDown("space"))
        {
            Destroy(gameObject);
            /* Acrescentar pontos baseado se apertou a tecla certa
             * Talvez ir reduzindo a opacidade aos poucos do passo
             */
        }

    }

}


