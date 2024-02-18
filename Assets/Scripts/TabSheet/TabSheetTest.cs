using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Sheet;

public class TabSheetTest : MonoBehaviour
{

    [SerializeField]
    private Button[] _tabButtons = null;
    [SerializeField]
    private int _firstSelectTab = 0;

    private SheetContainer _sheetContainer = null;

    private int _tabButtonLength = 0;
    private string[] _sheetIds = null;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _sheetContainer = SheetContainer.Find("SheetContainer");
        Assert.IsNotNull(_sheetContainer);

        _tabButtonLength = _tabButtons.Length;
        _sheetIds = new string[_tabButtonLength];
        for (int i = 0; i < _tabButtonLength; ++i)
        {
            int tabIndex = i;
            _tabButtons[i].onClick.AddListener(() => { OnClick_TabButton(tabIndex); });
            var buttonImage = _tabButtons[i].gameObject.GetComponentInChildren<Image>();
            var buttonText = _tabButtons[i].gameObject.GetComponentInChildren<Text>();
            yield return RegisterSheet(tabIndex, buttonImage.color, buttonText.text);
        }

        SeletcTabButton(_firstSelectTab);
        yield return ShowSheet(_firstSelectTab, false);
    }

    private IEnumerator RegisterSheet(int tabIndex, Color color, string text)
    {
        var registerHandle = _sheetContainer.Register(
                                "TabContentSheet",
                                onLoad: x => {
                                    var sheet = (TabContentSheet)x.sheet;
                                    sheet.SetData(tabIndex, color, text);
                                }
                                );
        yield return registerHandle;
        _sheetIds[tabIndex] = (string)registerHandle.Result;
    }
    private IEnumerator ShowSheet(int tabIndex, bool playAnimation)
    {
        if(_sheetContainer.ActiveSheetId == _sheetIds[tabIndex])
            yield break;

        yield return _sheetContainer.Show(_sheetIds[tabIndex], playAnimation);
    }

    private void OnClick_TabButton(int tabIndex)
    {
        if(_sheetContainer.Interactable == false)
            return;
        SeletcTabButton(tabIndex);
        StartCoroutine(ShowSheet(tabIndex, true));
    }

    private void SeletcTabButton(int tabIndex)
    {
        for(int i = 0; i < _tabButtonLength; ++i)
        {
            _tabButtons[i].interactable = (i != tabIndex);
        }
    }
}
