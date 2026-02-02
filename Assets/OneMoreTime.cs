using TMPro;
using UnityEngine;

public class OneMoreTime : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float timer = 0.0f; 
    public float time = 5.0f;
    public TextMeshProUGUI text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > time)
        {
            text.text = " ";
        }
    }
}
