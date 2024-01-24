using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GuitarHero : MonoBehaviour
{
    public float speed = 6f;
    [SerializeField] private bool greenArea = false;


    void Start()
    {
        

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
        Debug.Log("entrou na área");
        greenArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        greenArea = false;
    }

}


