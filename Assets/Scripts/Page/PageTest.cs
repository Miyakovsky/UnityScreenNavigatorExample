using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;
using static System.Net.Mime.MediaTypeNames;

public class PageTest : MonoBehaviour
{
    [SerializeField]
    private Button _backButtonObj = null;
    [SerializeField]
    private Button _nextButtonObj = null;
    [SerializeField]
    private int _maxPageCnt = 100;


    private PageContainer _pageContainer = null;
    private bool _canPushButton = true;
    private int _pageCnt = 0;

    void Start()
    {
        Assert.IsNotNull(_backButtonObj);
        Assert.IsNotNull(_nextButtonObj);

        _pageContainer = PageContainer.Find("PageContainer");
        Assert.IsNotNull(_pageContainer);

        _canPushButton = true;
        _backButtonObj.onClick.AddListener(
            () =>
            {
                if(_canPushButton)
                {
                    StartCoroutine(PopPage());
                }
            }
            );
        _nextButtonObj.onClick.AddListener(
            () =>
            {
                if(_canPushButton)
                {
                    StartCoroutine(PushPage());
                }
            }
            );

        UpdateActiveButton();

    }

    private void UpdateActiveButton()
    {
        var count = _pageContainer.Pages.Count;
        _backButtonObj.interactable = count > 0;
        _nextButtonObj.interactable = count <= _maxPageCnt;
    }

    private IEnumerator PushPage()
    {
        _canPushButton = false;
        ++_pageCnt;
        var handle = _pageContainer.Push("Page", true,
            onLoad: x => {
                var page = (PageController)x.page;
                page.SetData(
                    _pageCnt.ToString(),
                    new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f)
                    );
            });

        yield return handle;

        _canPushButton = true;
        UpdateActiveButton();
        yield break;
    }

    private IEnumerator PopPage()
    {
        _canPushButton = false;
        --_pageCnt;
        var handle = _pageContainer.Pop(true);
        yield return handle;

        _canPushButton = true;
        UpdateActiveButton();
        yield break;
    }

}
