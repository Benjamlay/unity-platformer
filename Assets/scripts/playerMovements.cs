using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class playerMovements : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _speedForce = 10f;
    [SerializeField] ContactDetector _isGrounded;
    [SerializeField] laddersContact _ladder_contact;
    [SerializeField] private UnityEvent GameOver;
    [SerializeField] private UnityEvent YouWon;
    
    public float _invincibilityDuration = 2f;
    public float flashDuration = 0.1f;
     private int _PV;
    
    private float _horizontalInput;
    private float _verticalInput;
    private Rigidbody2D _rb;

    private bool _is_Jumping;
    private bool _ladder_contacted;
    private bool _isInvincible = false; 

    private bool _IsInWater = false;
    
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    public Color _underWaterColor = new Color(0.5f, 0.7f, 1f, 1f);
    
    public int _coins = 0;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _PV = 3;
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (_spriteRenderer != null)
        {
            _originalColor = _spriteRenderer.color;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_is_Jumping && _isGrounded.ContactDetected)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        _rb.linearVelocityX = _horizontalInput * _speedForce;

        if(_ladder_contact && _ladder_contact._ContactLadderDetected)
        { 
            _rb.linearVelocityY = _verticalInput * _speedForce;
        }
    }

    public void OnMoveLeftRight(InputAction.CallbackContext context)
    {
        _horizontalInput = context.ReadValue<float>();
        //Debug.Log("On Run : " + _horizontalInput);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _is_Jumping = true;
        }
        if (context.canceled)
        {
            _is_Jumping = false;
        }
    }
    public void OnLadderContact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _ladder_contacted = true;
        }
        if (context.canceled)
        {
            _ladder_contacted = false;
            _verticalInput = 0;
        }
        if (_ladder_contacted)
        {
            _verticalInput = context.ReadValue<float>();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water") && _spriteRenderer != null)
        {
            _IsInWater = true;
            _rb.linearDamping = 0.5f;
            _spriteRenderer.color = _underWaterColor;
        }

        if (other.CompareTag("coin"))
        {
            _coins++;
            Debug.Log(" coin : " + _coins);
        }

        if (other.CompareTag("Enemy") && !_isInvincible)
        {
            _PV--;
            
            Debug.Log("Player hit by an enemy!");
            StartCoroutine(HandleInvincibility());
            
            if (_PV == 0)
            {
                GameOver.Invoke();
            }
        }

        if (other.CompareTag("End"))
        {
            YouWon.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water") && _spriteRenderer != null)
        {
            _IsInWater = false;
            _rb.linearDamping = 0;
            _spriteRenderer.color = _originalColor;
        }
    }
    
    private IEnumerator HandleInvincibility()
    {
        _isInvincible = true; // Active l'invincibilité
        float elapsed = 0f;

        while (elapsed < _invincibilityDuration)
        {
            // Fait clignoter le sprite
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(flashDuration);
            elapsed += flashDuration;
        }

        // Restaure l'état initial
        _spriteRenderer.enabled = true;
        _isInvincible = false;
    }
}
