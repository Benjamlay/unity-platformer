using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UiManager : MonoBehaviour
{

    [FormerlySerializedAs("_gameOverTexte")] [SerializeField] private TextMeshProUGUI _gameOverText;
    [FormerlySerializedAs("_YouWonTexte")] [SerializeField] private TextMeshProUGUI _YouWonText;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideGameOver();
        HideYouWon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideGameOver()
    {
        _gameOverText.enabled = false;
    }

    public void HideYouWon()
    {
        _YouWonText.enabled = false;
    }
    
    public void ShowGameOver()
    {
        _gameOverText.enabled = true;
    }
    
    public void ShowYouWon()
    {
        _YouWonText.enabled = true;
    }
}
