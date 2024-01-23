using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Global Variables
    [SerializeField] private float moveSpeed = 10f;

    //Components
    private Rigidbody2D _rb;
    private Animator _anim;

    Vector2 moveInput;

    #endregion

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + moveInput * moveSpeed * Time.deltaTime);
        
    }
}
