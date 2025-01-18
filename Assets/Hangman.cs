using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hangman : MonoBehaviour
{
    private List<Transform> _parts;

    public Action OnHangmanComplete;

    void Start()
    {
        _parts = new List<Transform>();
        foreach (Transform t in transform)
        {
            _parts.Add(t);
        }
    }

    public void AddNextPart()
    {
        var firstPart = _parts.FirstOrDefault((t) => t.gameObject.activeInHierarchy == false);
        
        if (firstPart)
            firstPart.gameObject.SetActive(true);
    }
}