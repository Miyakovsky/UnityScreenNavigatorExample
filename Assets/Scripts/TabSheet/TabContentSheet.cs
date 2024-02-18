using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Sheet;

public class TabContentSheet : Sheet
{
    [SerializeField]
    private Image _backgrundImage = null;
    [SerializeField]
    private Text _text = null;

    public int TabIndex { private set; get; }


    public void SetData(int tabIndex, Color color, string text)
    {
        _backgrundImage.color = color;
        _text.text = text;
        TabIndex = tabIndex;
    }


}
