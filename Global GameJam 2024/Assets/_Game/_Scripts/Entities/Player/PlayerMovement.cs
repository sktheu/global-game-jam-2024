using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Global Variables
    [SerializeField] private float moveSpeed = 10f;

    //Components
    private Rigidbody2D _rb;
    private Animator _anim;
    private SpriteRenderer _spr;

    // References
    private DanceBehaviour _danceBehaviour;

    Vector2 moveInput;
    #endregion

    #region Unity Functions
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spr = GetComponent<SpriteRenderer>();
        _danceBehaviour = GetComponent<DanceBehaviour>();
    }

    private void Update()
    {
        if (!_danceBehaviour.isDancing)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();

            FlipX();
        }
    }

    private void FixedUpdate()
    {
        if (!_danceBehaviour)
            _rb.MovePosition(_rb.position + moveInput * moveSpeed * Time.deltaTime);
        else
            _rb.velocity = Vector2.zero;
    }

    private void FlipX()
    {
        if (moveInput.x < 0)
            _spr.flipX = true;
        else if (moveInput.x > 0)
            _spr.flipX = false;
    }
    #endregion
}

