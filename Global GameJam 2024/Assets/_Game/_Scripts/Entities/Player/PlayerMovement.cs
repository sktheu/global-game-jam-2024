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
    private SpriteRenderer _spr;

    Vector2 moveInput;
    #endregion

    #region Unity Functions
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        FlipX();
    }

    private void FixedUpdate() => _rb.MovePosition(_rb.position + moveInput * moveSpeed * Time.deltaTime);

    private void FlipX()
    {
        if (moveInput.x < 0)
            _spr.flipX = true;
        else if (moveInput.x > 0)
            _spr.flipX = false;
    }
    #endregion
}

