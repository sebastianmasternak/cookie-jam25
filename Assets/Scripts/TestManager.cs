using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    public WaveManager[] levels;
    public int currentLevel = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levels[0].StartWaves();
    }

    // Update is called once per frame
    void Update()
    {
        if (levels[currentLevel].IsActive() == false)
        {
            currentLevel++;
            if(currentLevel < levels.Length)
            {
                levels[currentLevel].StartWaves();
            }
            else
            {
                Debug.Log("All levels completed!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
