using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Game : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private GameObject _startCanvas;
    [SerializeField] private GameObject _gameCanvas;

    [SerializeField] private Transform _keyContainer;
    [SerializeField] private TMP_Text _keyPrefab;
    [SerializeField] private string _word;
    [SerializeField] private TMP_Text _visibleWord;
    
    public string Word => _word;
    private bool _playing;

    private int keyStart = 'a';
    private int keyEnd = 'z';

    private string _attempted = "";

    private Hangman _hangman;

    private void Start()
    {
        _hangman = FindAnyObjectByType<Hangman>();
    }

    public void OnStartPressed()
    {
        _word = _inputField.text;
        _startCanvas.SetActive(false);
        _gameCanvas.SetActive(true);
        _playing = true;
        GenerateKeyGrid();
        UpdateVisibleWord();
    }

    private void GenerateKeyGrid()
    {
        for (int i = keyStart; i <= keyEnd; i++)
        {
            var key = Instantiate(_keyPrefab, _keyContainer);
            key.text = ((char)i).ToString();
            key.gameObject.SetActive(true);
        }
    }

    public void TryKey(TMP_Text text)
    {
        char key = text.text[0];
        
        if (_attempted.Contains(key))
            return;

        _attempted = _attempted + key;
        
        if (_word.ToLower().Contains(key))
        {
            Debug.Log("Nice!");
            text.color = Color.green;
        }
        else
        {
            Debug.Log("Not nice!");
            text.color = Color.red;
            _hangman.AddNextPart();
        }
        
        UpdateVisibleWord();
    }

    private void UpdateVisibleWord()
    {
        string visibleWord = _word.Select((letter) => _attempted.ToLower().Contains(letter) ? letter : '_').ToArray()
            .ArrayToString();
        _visibleWord.text = visibleWord;
    }
}
