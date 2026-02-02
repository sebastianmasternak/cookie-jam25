using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI nameText;
    public Image cardImage;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI descriptionText;
    public Button buyButton;

    private StoreManager storeManager;
    private int cardIndex;

    public void Setup(CardData card, int index, StoreManager manager)
    {
        storeManager = manager;
        cardIndex = index;

        if (card != null)
        {
            // Populate UI
            if (nameText != null) nameText.text = card.cardName;
            if (costText != null) costText.text = $"{card.cardCost} G"; // Assuming G for Gold
            if (descriptionText != null) descriptionText.text = card.cardDescription;
            
            if (cardImage != null)
            {
                cardImage.sprite = card.cardImage;
                cardImage.enabled = card.cardImage != null;
            }
            
            if (buyButton != null)
            {
                buyButton.interactable = true;
                buyButton.onClick.RemoveAllListeners();
                buyButton.onClick.AddListener(OnBuyClicked);
            }
            
            // Ensure visible if it was hidden
            gameObject.SetActive(true);
        }
        else
        {
            // If the card is null (sold or empty), hide the slot or disable interactions
            // Option A: Hide completely
            // gameObject.SetActive(false); 
            
            // Option B: Show as sold
            if (nameText != null) nameText.text = "Sold";
            if (costText != null) costText.text = "-";
            if (cardImage != null) cardImage.enabled = false;
            if (buyButton != null) buyButton.interactable = false;
        }
    }

    public void OnBuyClicked()
    {
        if (storeManager != null)
        {
            Debug.Log("Buying card: " + cardIndex);
            storeManager.BuyCard(cardIndex);
            // The StoreManager will invoke OnShopUpdated, triggering the StoreUI to refresh this display
        }
    }
}
