using System.Collections;
using UnityEngine;
using BookCurlPro;
using Sirenix.OdinInspector;

public class BookController : MonoBehaviour
{
    private AutoFlip _autoFlip;
    private BookPro _bookPro;
    public static BookController Instance;
    private RectTransform _rectTransform;


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
                UpdatePositionBasedOnPage();
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
                UpdatePositionBasedOnPage();
            }
        }
    }

    private IEnumerator UpdatePageAfterFlip()
    {
        yield return new WaitForSeconds(_autoFlip.PageFlipTime);
        _currentPaper = _bookPro.GetCurrentPageNumber();
        _currentPage += 1;
        UpdatePositionBasedOnPage();
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
        print(_bookPro.GetCurrentPageNumber());
    }

    // Method to update the book's position based on the current page number
    private void UpdatePositionBasedOnPage()
    {
        print("PAPER #" + _bookPro.GetCurrentPageNumber());
        print("Current page: " + _currentPage);

        if (_rectTransform != null)
        {
            Vector3 anchoredPosition = _rectTransform.anchoredPosition;

            if (_currentPage % 2 == 0) // Even pages
            {
                anchoredPosition.x = -246;
            }
            else // Odd pages
            {
                anchoredPosition.x = 246;
            }

            _rectTransform.anchoredPosition = anchoredPosition;
        }
    }

    // [Button()]
    // // Method to show the current front and back pages
    // private void ShowCurrentPages()
    // {
    //     GameObject frontPage = GetCurrentFrontPage();
    //     GameObject backPage = GetCurrentBackPage();
    //     print("current front page: " + frontPage + " | current back page: " + backPage);

    //     if (frontPage != null)
    //     {
    //         _bookPro.ShowPage(frontPage);
    //     }

    //     if (backPage != null)
    //     {
    //         _bookPro.ShowPage(backPage);
    //     }
    // }
}
