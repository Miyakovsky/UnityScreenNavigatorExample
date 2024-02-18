using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;

public class PageController : Page
{
    [SerializeField]
    private Text _textObj = null;
    [SerializeField]
    private Image _backgroundImage = null;


    public void SetData(string text, Color color)
    {
        _textObj.text = text;
        _backgroundImage.color = color;
    }

}


