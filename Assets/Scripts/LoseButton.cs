using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseButton : MonoBehaviour
{

    public Button loseButton;
    
    public void Start()
    {
        loseButton.onClick.AddListener(LoseGame);
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    
}
