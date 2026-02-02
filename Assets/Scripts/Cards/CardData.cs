using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CardData", menuName = "Cards/Definition Card")]
public class CardData : ScriptableObject
{
    public string cardName;
    public Sprite cardImage;
    public int cardCost;

    [TextArea]
    public string cardDescription;

    public CardEffect cardEffects;

}
