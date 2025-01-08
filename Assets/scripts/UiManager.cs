using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UiManager : MonoBehaviour
{

    [FormerlySerializedAs("_gameOverTexte")] [SerializeField] private TextMeshProUGUI _gameOverText;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideGameOver();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideGameOver()
    {
        _gameOverText.enabled = false;
    }

    public void ShowGameOver()
    {
        _gameOverText.enabled = true;
    }
    
}
