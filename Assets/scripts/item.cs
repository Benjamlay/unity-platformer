using System;
using UnityEngine;

public class item : MonoBehaviour
{
    
    [SerializeField] private GameObject _pickable;
    [SerializeField] private GameObject _picked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public event Action<item> OnPicked;
    
    void Start()
    {
        _pickable.SetActive(true);
        _picked.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Deactivate();
            OnPicked?.Invoke(this);
        }
    }

    public void Activate()
    {
        _pickable.SetActive(true);
        _picked.SetActive(false);
    }

    public void Deactivate()
    {
        _pickable.SetActive(false);
        _picked.SetActive(true);
    }
}
