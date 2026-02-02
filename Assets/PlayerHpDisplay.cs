using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpDisplay : MonoBehaviour
{
    public TextMeshProUGUI hpText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var entity = gameObject.GetComponent<Entity>();
        hpText.text = $"HP:{entity.health}/{entity.maxHealth}";
    }
}
