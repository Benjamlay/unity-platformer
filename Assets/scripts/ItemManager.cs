using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private UnityEvent GameOver;
    
    private List<item> _itemsToPick = new List<item>();

    void Start()
    {
        LoadItems();
        GameOver = new UnityEvent();
    }

    private void LoadItems()
    {
        // item[] itemsArray = FindObjectsByType<item>(FindObjectsSortMode.None);
        // _itemsToPick = new List<item>(itemsArray);

        item[] itemsArray = GetComponentsInChildren<item>();
        _itemsToPick = new List<item>(itemsArray);
        
        foreach (item item in _itemsToPick)
        {
            item.Activate();
            item.OnPicked += RemoveItem;
        }
    }
    private void RemoveItem(item itemToRemove)
    {
        itemToRemove.OnPicked -= RemoveItem;
        _itemsToPick.Remove(itemToRemove);
        
        if (_itemsToPick.Count == 0)
        {
            GameOver?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
