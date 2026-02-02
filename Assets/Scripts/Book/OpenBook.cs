using UnityEngine;
using UnityEngine.UI;

public class OpenBook : MonoBehaviour
{

    public GameObject bookPanel;
    public Button openButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Upewnij się, że książka jest zamknięta na początku
        if (bookPanel != null)
        {
            bookPanel.SetActive(false);
        }
        
        if (openButton != null)
        {
            openButton.onClick.AddListener(OpenBookAction);
        }
    }

    private void Update()
    {
        // Otwórz książkę klawiszem B
        if (Input.GetKeyDown(KeyCode.B) && bookPanel != null && !bookPanel.activeSelf)
        {
            OpenBookAction();
        }
    }

    public void OpenBookAction()
    {
        bookPanel.SetActive(true);
    }
}