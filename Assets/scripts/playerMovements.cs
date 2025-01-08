using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovements : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _speedForce = 10f;
    [SerializeField] ContactDetector _isGrounded;
    [SerializeField] laddersContact _ladder_contact;
    
    
    private float _horizontalInput;
    private float _verticalInput;
    private Rigidbody2D _rb;

    private bool _is_Jumping;
    private bool _ladder_contacted;

    private bool _IsInWater = false;
    
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    public Color _underWaterColor = new Color(0.5f, 0.7f, 1f, 1f);

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
}
