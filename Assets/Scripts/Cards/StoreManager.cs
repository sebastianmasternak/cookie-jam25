using UnityEngine;
using System.Collections.Generic;

public class StoreManager : MonoBehaviour
{
    public int numberOfUniqueCards = 18;
    public int maximumCardsInStore = 5;
    
    public List<CardData> cards; // Pool of all available cards
    public List<CardData> cardsInStore; // Current cards for sale
    
    public System.Action OnShopUpdated;

    private GameManger gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManger>();


        if (cardsInStore == null)
            cardsInStore = new List<CardData>();

        RefreshShop();
    }

    public void RefreshShop()
    {
        cardsInStore.Clear();
        if (cards == null || cards.Count == 0)
        {
            Debug.LogWarning("StoreManager: No cards in the pool to generate shop.");
            return;
        }

        for (var i = 0; i < maximumCardsInStore; i++)
        {
            CardData randomCard = cards[Random.Range(0, cards.Count)];
            cardsInStore.Add(randomCard);
        }
        
        OnShopUpdated?.Invoke();
    }

    private float regenerationTimer = 0f;
    private const float REGENERATION_INTERVAL = 5f;

    void Update()
    {
        regenerationTimer += Time.deltaTime;
        if (regenerationTimer >= REGENERATION_INTERVAL)
        {
            regenerationTimer = 0f;
            ReplenishEmptySlots();
        }
    }

    private void ReplenishEmptySlots()
    {
        bool updated = false;
        for (int i = 0; i < cardsInStore.Count; i++)
        {
            if (cardsInStore[i] == null)
            {
                if (cards != null && cards.Count > 0)
                {
                    cardsInStore[i] = cards[Random.Range(0, cards.Count)];
                    updated = true;
                }
            }
        }

        if (updated)
        {
            OnShopUpdated?.Invoke();
        }
    }

    public void BuyCard(int index)
    {
        if (index < 0 || index >= cardsInStore.Count)
        {
            Debug.LogError("StoreManager: Invalid card index.");
            return;
        }

        CardData cardToBuy = cardsInStore[index];
        if (cardToBuy == null)
        {
            Debug.Log("StoreManager: Slot is empty.");
            return;
        }

        if (gameManager == null)
        {
            Debug.LogError("StoreManager: Missing dependencies (GameManager or Player).");
            return;
        }

        if (gameManager.GetCoins() >= cardToBuy.cardCost)
        {
            // Transaction
            gameManager.RemoveCoins(cardToBuy.cardCost);
            gameManager.AddCard(cardToBuy);
            
            Debug.Log($"Purchased {cardToBuy.cardName} for {cardToBuy.cardCost}.");

            // Remove from store (mark as sold/null or remove from list)
            // Strategy: Set to null to keep index positions stable if using UI buttons
            cardsInStore[index] = null;
            
            OnShopUpdated?.Invoke();
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}
