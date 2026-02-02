using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoreUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cardsContainer;
    [SerializeField] private GameObject cardDisplayPrefab;
    
    private StoreManager storeManager;

    void Start()
    {
        storeManager = FindFirstObjectByType<StoreManager>();

        // Ensure the cardsContainer has a VerticalLayoutGroup to arrange cards top-to-bottom
        if (cardsContainer != null)
        {
            VerticalLayoutGroup layoutGroup = cardsContainer.GetComponent<VerticalLayoutGroup>();
            if (layoutGroup == null)
            {
                layoutGroup = cardsContainer.gameObject.AddComponent<VerticalLayoutGroup>();
                
                // Configure default settings for a vertical list
                layoutGroup.childAlignment = TextAnchor.UpperCenter;
                layoutGroup.childControlHeight = false; // Let cards define their own height
                layoutGroup.childControlWidth = true;  // Stretch cards to fit container width
                layoutGroup.childForceExpandHeight = false;
                layoutGroup.childForceExpandWidth = true;
                layoutGroup.spacing = 10f; // Add some spacing between cards
            }
        }
        
        if (storeManager != null)
        {
            // Subscribe to updates
            storeManager.OnShopUpdated += UpdateStoreUI;
            
            // Initial update
            UpdateStoreUI();
        }
        else
        {
            Debug.LogError("StoreUI: Could not find StoreManager in the scene.");
        }
    }

    void OnDestroy()
    {
        if (storeManager != null)
        {
            storeManager.OnShopUpdated -= UpdateStoreUI;
        }
    }

    public void UpdateStoreUI()
    {
        if (storeManager == null || cardsContainer == null || cardDisplayPrefab == null) return;

        // Clean up existing card slots
        // Note: Destroying and re-instantiating might be expensive if done frequently. 
        // For a simple shop, it's acceptable. For optimization, object pooling is recommended.
        foreach (Transform child in cardsContainer)
        {
            Destroy(child.gameObject);
        }

        // Spawn new card slots
        List<CardData> cards = storeManager.cardsInStore;
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject obj = Instantiate(cardDisplayPrefab, cardsContainer);
            CardDisplay display = obj.GetComponent<CardDisplay>();
            if (display != null)
            {
                display.Setup(cards[i], i, storeManager);
            }
        }
    }
}
