using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BookCurlPro;
using Sirenix.OdinInspector;

public class BookController : MonoBehaviour
{
    public List<BoolValue> PageBoolObjects;
    private AutoFlip _autoFlip;
    private BookPro _bookPro;
    private RectTransform _rectTransform;
    public static BookController Instance;

    private int _currentPaper = 0; // always the front one (the right one)
    private int _currentPage = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        PageBoolObjects = Resources.LoadAll<BoolValue>("ScriptableObjects/InstructionBook").ToList();
        _autoFlip = GetComponent<AutoFlip>();
        _bookPro = GetComponent<BookPro>();
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_currentPage % 2 != 0) // Odd pages
            {
                _autoFlip.FlipLeftPage();
                StartCoroutine(UpdatePageAfterFlip());
            }
            else
            {
                print("Página par, não flipar.");
                _currentPage -= 1;
                StartCoroutine(LerpPosition(_rectTransform.anchoredPosition.x, _currentPage % 2 == 0 ? -246 : 246, 0.5f));
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(_currentPage % 2 == 0)
            {
                _autoFlip.FlipRightPage();
                StartCoroutine(UpdatePageAfterFlip());
            }
            else
            {
                print("Página impar, não flipar.");
                _currentPage += 1;
                StartCoroutine(LerpPosition(_rectTransform.anchoredPosition.x, _currentPage % 2 == 0 ? -246 : 246, 0.25f));
            }
        }
    }

    private IEnumerator UpdatePageAfterFlip()
    {
        yield return new WaitForSeconds(_autoFlip.PageFlipTime);
        _currentPaper = _bookPro.GetCurrentPageNumber();
        _currentPage += 1;
        StartCoroutine(LerpPosition(_rectTransform.anchoredPosition.x, _currentPage % 2 == 0 ? -246 : 246, 0.25f));
    }

    private IEnumerator LerpPosition(float start, float end, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t); // Ease in out
            _rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(start, end, t), _rectTransform.anchoredPosition.y);
            time += Time.deltaTime;
            yield return null;
        }
        _rectTransform.anchoredPosition = new Vector2(end, _rectTransform.anchoredPosition.y);
    }

    // Method to get the current front page
    public GameObject GetCurrentFrontPage()
    {
        return _bookPro.GetCurrentFrontPage();
    }

    // Method to get the current back page
    public GameObject GetCurrentBackPage()
    {
        return _bookPro.GetCurrentBackPage();
    }

    public int GetCurrentFrontPageNumber()
    {
        GameObject frontPage = _bookPro.GetCurrentFrontPage();
        if (frontPage != null && int.TryParse(frontPage.name, out int pageNumber))
        {
            return pageNumber;
        }
        return -1;
    }

    // Method to get the current back page number
    public int GetCurrentBackPageNumber()
    {
        GameObject backPage = _bookPro.GetCurrentBackPage();
        if (backPage != null && int.TryParse(backPage.name, out int pageNumber))
        {
            return pageNumber;
        }
        return -1;
    }

    [Button()]
    public void PrintCurrentPage()
    {
        print(_currentPage);
    }


    private void UpdatePositionBasedOnPage()
    {
        print("PAPER #" + _bookPro.GetCurrentPageNumber());
        print("Current page: " + _currentPage);

        if (_rectTransform != null)
        {
            float targetX = _currentPage % 2 == 0 ? -246 : 246;
            StartCoroutine(LerpPosition(_rectTransform.anchoredPosition.x, targetX, 0.1f));
        }
    }
    
    [Button()]
    public int[] GetPagesForPaper(int paperIndex)
    {
        int firstPage = paperIndex * 2;
        int secondPage = firstPage + 1;
        return new int[] { firstPage, secondPage };
    }

    [Button()]
    public int GetPaperForPage(int pageIndex)
    {
        return pageIndex / 2;
    }

    [Button()]
    public bool CheckIfPlayerHasPage(int pageNumber)
    {
        int paperInQuestion = GetPaperForPage(pageNumber);
        int number;
        foreach (var boolValue in PageBoolObjects)
        {
            if (int.TryParse(boolValue.name.Split('_').Last(), out number))
            {
                if(number == paperInQuestion)
                {
                    if(boolValue.runTimeValue == true)
                    {
                        print($"The player has the paper {paperInQuestion} that contains the {pageNumber}.");
                        return true;
                    }
                    else
                    {
                        print($"Missing paper {paperInQuestion} that contains the page {pageNumber}.");
                        return false;
                    }
                }
            }
        }
        return false;
    }

    [Button()]
    public void PrintAllBooleans()
    {
        foreach (var boolValue in PageBoolObjects)
        {
            Debug.Log($"{boolValue.name} is {boolValue.runTimeValue}");
        }
    }
}
