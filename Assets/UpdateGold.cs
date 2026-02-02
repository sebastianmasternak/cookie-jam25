using TMPro;
using UnityEngine;

public class UpdateGold : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateGoldValue(int gold)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Złoto :" + gold;
    }
}
