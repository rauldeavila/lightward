using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BookCurlPro;
using BookCurlPro.Examples;
using Sirenix.OdinInspector;

public class BookController : MonoBehaviour
{
    public IntValue CurrentPageIntValue;
    public List<GameObject> OurPages;
    public List<BoolValue> PageBoolObjects;
    public List<int> PagesWeHave;
    private AutoFlip _autoFlip;
    private BookPro _bookPro;
    private AddPagesDynamically _addPagesDynamically;
    private RectTransform _rectTransform;
    public static BookController Instance;

    private int _currentPaper = 0; // always the front one (the right one)
    private int _currentPage = 0;

    private int _firstPage;
    private int _lastPage;

    private bool _oddPageOnAwake = false;
    private bool _canChangePage = true;

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
        _addPagesDynamically = GetComponent<AddPagesDynamically>();
        _rectTransform = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        ClearPapersList();
        PopulateOurPagesList();
        EnablePages();
        UpdateFirstAndLastPage();

        // Find the first available page and set it as the current page in BookPro
        if (PagesWeHave.Count > 0)
        {
            // int firstAvailablePage = PagesWeHave.Min();
            // print("Current page = " + _currentPage);
            // int paperIndex = GetPaperFor(_currentPage);
            // _bookPro.SetCurrentPaper(paperIndex);
            // Debug.Log($"Paper set to {paperIndex}");
            // if(_oddPageOnAwake)
            // {
            //     _currentPage = _currentPage - 1;
            //     NavigateRight();
            //     // NavigateRightOnEnable();
            // }


            int firstAvailablePage = PagesWeHave.Min();
            // print("Current page = " + _currentPage);
            
            if(_oddPageOnAwake)
            {
                _currentPage = _currentPage - 1;
                int paperIndex = GetPaperFor(_currentPage);
                _bookPro.SetCurrentPaper(paperIndex);
                // Debug.Log($"Paper set to {paperIndex}");
                Invoke("NavigateRight", 0.1f);
                // Debug.Log($"The book opened on page {_currentPage + 1}.");
            }
            else
            {
                int paperIndex = GetPaperFor(_currentPage);
                _bookPro.SetCurrentPaper(paperIndex);
                // Debug.Log($"Paper set to {paperIndex}");
                Debug.Log($"The book opened on page {_currentPage}.");
            }


        }
        else
        {
            _currentPage = 0;
        }
        if(_currentPage % 2 == 0)
        {
            _rectTransform.anchoredPosition = new Vector3(-246f, _rectTransform.anchoredPosition.y);
        }
        else
        {
            _rectTransform.anchoredPosition = new Vector3(246f, _rectTransform.anchoredPosition.y);
        }
        // PrintAllPagesWeHave();
    }


    void OnDisable()
    {
        CurrentPageIntValue.runTimeValue = _currentPage;
        ClearPapersList();
    }

    [Button()]
    public void EnablePages()
    {
        // Loop through all the pages we have
        foreach (int pageIndex in PagesWeHave)
        {
            // Ensure the index is within bounds
            if (pageIndex < OurPages.Count)
            {
                // Make the page active
                OurPages[pageIndex].SetActive(true);
                if(pageIndex % 2 == 0)
                {
                    _addPagesDynamically.FrontPagePrefab = OurPages[pageIndex];
                    if(_addPagesDynamically.BackPagePrefab != null)
                    {
                        _addPagesDynamically.AddPaper(_bookPro);
                        _addPagesDynamically.FrontPagePrefab = null;
                        _addPagesDynamically.BackPagePrefab = null;
                    }
                }
                else
                {
                    _addPagesDynamically.BackPagePrefab = OurPages[pageIndex];
                    if(_addPagesDynamically.FrontPagePrefab != null)
                    {
                        _addPagesDynamically.AddPaper(_bookPro);
                        _addPagesDynamically.FrontPagePrefab = null;
                        _addPagesDynamically.BackPagePrefab = null;
                    }
                }
            }
        }

        // Set the first page we have as the _currentPage if PagesWeHave is not empty
        if (PagesWeHave.Count > 0)
        {
            int firstPageIndex = PagesWeHave[0];
            if(CurrentPageIntValue.runTimeValue != -1)
            {
                _currentPage = CurrentPageIntValue.runTimeValue;
                // Debug.Log($"Current page set to: {_currentPage} due to Scriptable Object not being '-1'.");
                if(_currentPage % 2 != 0)
                {
                    _oddPageOnAwake = true;
                    // Debug.Log("Odd page on awake!");
                }
            }
            else
            {
                if (firstPageIndex < OurPages.Count)
                {
                    _currentPage = firstPageIndex;
                    // Debug.Log($"Current page set to: {firstPageIndex}.");
                }
            }
        }

        foreach (GameObject obj in OurPages)
        {
            obj.SetActive(false);
        }
    }



    [Button()]
    public void ClearPapersList()
    {
        _bookPro.ClearPapersList();

    }

    void Update()
    {
        if(_canChangePage)
        {
            _canChangePage = false;
            if (Inputs.Instance.HoldingLeftArrow)
            {
                NavigateLeft();
            }

            if (Inputs.Instance.HoldingRightArrow)
            {
                NavigateRight();
            }
            Invoke("ResetCanChangePage", 0.2f);
        }
    }

    void ResetCanChangePage()
    {
        _canChangePage = true;
    }

    public void NavigateLeft()
    {
        if(_currentPage != _firstPage){
            if (_currentPage % 2 != 0) // Odd pages
            {
                _autoFlip.FlipLeftPage();
                int previousPage = _currentPage;
                _currentPage = GetPreviousAvailablePage();
                // Debug.Log($"Navigating backwards. From page {previousPage} to page {_currentPage}.");
                StartCoroutine(UpdatePageAfterFlip());
            }
            else
            {
                // print("Página par, não flipar.");
                int previousPage = _currentPage;
                _currentPage = GetPreviousAvailablePage();
                // Debug.Log($"Navigating backwards. From page {previousPage} to page {_currentPage}.");
                StartCoroutine(LerpPosition(_rectTransform.anchoredPosition.x, _currentPage % 2 == 0 ? -246 : 246, 0.5f));
            }
        }
    }

    public void NavigateRight()
    {
        if(_currentPage != _lastPage)
        {
            if(_currentPage % 2 == 0)
            {
                _autoFlip.FlipRightPage();
                int previousPage = _currentPage;
                _currentPage = GetNextAvailablePage();
                // Debug.Log($"Navigating forward! From page {previousPage} to page {_currentPage}.");
                StartCoroutine(UpdatePageAfterFlip());
            }
            else
            {
                // print("Página impar, não flipar.");
                int previousPage = _currentPage;
                _currentPage = GetNextAvailablePage();
                // Debug.Log($"Navigating forward! From page {previousPage} to page {_currentPage}.");
                StartCoroutine(LerpPosition(_rectTransform.anchoredPosition.x, _currentPage % 2 == 0 ? -246 : 246, 0.25f));
            }
        }
    }

    public void NavigateRightOnEnable()
    {
        if(_currentPage != _lastPage)
        {
            _autoFlip.FlipRightPage();
            StartCoroutine(SpecialUpdateForAwake());
        }
    }
    private IEnumerator UpdatePageAfterFlip()
    {
        yield return new WaitForSeconds(_autoFlip.PageFlipTime);
        _currentPaper = _bookPro.GetCurrentPageNumber();
        StartCoroutine(LerpPosition(_rectTransform.anchoredPosition.x, _currentPage % 2 == 0 ? -246 : 246, 0.25f));
    }

    private IEnumerator SpecialUpdateForAwake()
    {
        yield return new WaitForSeconds(_autoFlip.PageFlipTime);
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
        // print("PAPER #" + _bookPro.GetCurrentPageNumber());
        // print("Current page: " + _currentPage);

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
    public int GetPaperFor(int pageIndex)
    {
        // Ensure PagesWeHave is sorted
        PagesWeHave.Sort();

        // Create a dictionary to map pages to their respective paper indices
        Dictionary<int, int> pageToPaperIndex = new Dictionary<int, int>();
        int paperIndex = 0;

        // Populate the dictionary
        for (int i = 0; i < PagesWeHave.Count; i += 2)
        {
            pageToPaperIndex[PagesWeHave[i]] = paperIndex;
            pageToPaperIndex[PagesWeHave[i] + 1] = paperIndex;
            paperIndex++;
        }

        // Return the paper index for the given pageIndex
        if (pageToPaperIndex.TryGetValue(pageIndex, out int result))
        {
            return result;
        }

        throw new ArgumentException("Page index does not correspond to any available papers.");
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

    [Button()]
    public void PrintAllPagesWeHave()
    {
        string concatenatedString = "Papers we have: ";
        int number;
        foreach (var page in PageBoolObjects)
        {
            if(page.runTimeValue == true)
            {
                if (int.TryParse(page.name.Split('_').Last(), out number))
                {
                    concatenatedString += number + ", ";
                }
                // concatenatedString += page.name + ", ";
            }
        }
        concatenatedString = concatenatedString.TrimEnd(',', ' ');
        print(concatenatedString + ".");
    }

    [Button()]
    public void PrintFirstAndLastPage()
    {
        print($"First page: {_firstPage}, Last page: {_lastPage}");
    }

    public void UpdateFirstAndLastPage()
    {
        if (PagesWeHave.Count == 0)
        {
            print("No pages available.");
            return;
        }

        _firstPage = PagesWeHave.Min();
        _lastPage = PagesWeHave.Max();
        Debug.Log($"First page: {_firstPage}. Last page: {_lastPage}.");
    }
    
    public void PopulateOurPagesList()
    {
        int number;
        foreach (var page in PageBoolObjects)
        {
            if(page.runTimeValue == true)
            {
                if (int.TryParse(page.name.Split('_').Last(), out number))
                {
                    PagesWeHave.Add(GetPagesForPaper(number)[0]);
                    PagesWeHave.Add(GetPagesForPaper(number)[1]);
                }
            }
        }
    }
    
    [Button()]
    private int GetNextAvailablePage()
    {
        return PagesWeHave.FirstOrDefault(page => page > _currentPage);
    }

    [Button()]
    private int GetPreviousAvailablePage()
    {
        return PagesWeHave.LastOrDefault(page => page < _currentPage);
    }

}
