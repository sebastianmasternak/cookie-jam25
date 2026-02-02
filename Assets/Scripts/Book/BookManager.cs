using UnityEngine;
using UnityEngine.UI; 

public class BookManager : MonoBehaviour
{
    public GameObject bookPanel;
    public Button leftButton;
    public Button rightButton;

    public GameObject[] leftPages; 
    public GameObject[] rightPages; 

    private int currentPageIndex = 0;
    private int totalPages; 

    private void OnEnable()
    {
        totalPages = leftPages.Length;

        if (leftButton != null)
        {
            leftButton.onClick.RemoveListener(MoveLeft);
            leftButton.onClick.AddListener(MoveLeft);
        }

        if (rightButton != null)
        {
            rightButton.onClick.RemoveListener(MoveRight);
            rightButton.onClick.AddListener(MoveRight);
        }
        UpdatePageDisplay();
        UpdateButtons();
    }

    private void Update()
    {
        // Zamknij książkę klawiszem ESC
        if (Input.GetKeyDown(KeyCode.Escape) && bookPanel != null && bookPanel.activeSelf)
        {
            CloseBook();
        }
    }

    private void UpdatePageDisplay()
    {
        int count = Mathf.Min(leftPages.Length, rightPages.Length);

        for (int i = 0; i < count; i++)
        {
           
            bool isActive = (i == currentPageIndex);
            
            // --- LOGIKA OBSŁUGI LEWEJ STRONY ---
            if (leftPages[i] != null)
            {
                leftPages[i].SetActive(isActive);
            }
            
            // --- LOGIKA OBSŁUGI PRAWEJ STRONY ---
            if (rightPages[i] != null)
            {
                rightPages[i].SetActive(isActive);
            }
        }
    }

    private void UpdateButtons()
    {
        if (totalPages <= 1)
        {
            leftButton.interactable = false;
            rightButton.interactable = false;
            return;
        }

 
        leftButton.interactable = currentPageIndex > 0;
        rightButton.interactable = currentPageIndex < totalPages - 1;
    }

    public void MoveLeft()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            UpdatePageDisplay(); 
            UpdateButtons();    
        }
    }

    public void MoveRight()
    {
        if (currentPageIndex < totalPages - 1)
        {
            currentPageIndex++;
            UpdatePageDisplay(); 
            UpdateButtons();    
        }
    }

    public void CloseBook()
    {
        if (bookPanel != null)
        {
            bookPanel.SetActive(false);
            // Reset do pierwszej strony przy ponownym otwarciu
            currentPageIndex = 0;
            UpdatePageDisplay();
            UpdateButtons();
        }
    }

    public void OpenBook()
    {
        if (bookPanel != null)
        {
            bookPanel.SetActive(true);
            currentPageIndex = 0;
            UpdatePageDisplay();
            UpdateButtons();
        }
    }
}