/* Simone Siragusa 306067 @ PoliTO | Game Design & Gamification
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Globals;

public class StoreItem : MonoBehaviour
{
    // COMPONENT ATTRIBUTES ____________________________________________________ COMPONENT ATTRIBUTES
    [Header("UI elements:")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI SLPriceText;
    [SerializeField] private TextMeshProUGUI CCPriceText;

    [Header("Item info:")]
    [SerializeField] private string itemName;
    [SerializeField][TextArea] private string itemDescription;
    [SerializeField] private int levelAvailability;
    [SerializeField] private int SLPrice;
    [SerializeField] private int CCPrice;

    [Header("Item properties:")]
    [SerializeField] private bool isIncremental = false;
    [SerializeField] private int health;
    [SerializeField] private int armor;
    [SerializeField] [Range(.01f, 1f)] private float defHFactor;
    [SerializeField] [Range(.01f, 1f)] private float defAFactor;

    // COMPONENT LIFECYCLE METHODS _____________________________________________ COMPONENT LIFECYCLE METHODS

    private void OnEnable()
    {
        CheckAvailability();
    }

    private void Start()
    {
        nameText.text = itemName;
        descriptionText.text = itemDescription;
        if (itemName != "RAINBOW SPRINKLED ICING")
        {
            SLPriceText.text = SLPrice.ToString();
            CCPriceText.text = CCPrice.ToString();
        }
        else
        {
            SLPriceText.text = "???";
            CCPriceText.text = "???";
        }
    }

    // COMPONENT METHODS _______________________________________________________ COMPONENT METHODS

    private void CheckAvailability()
    {
        PlayerData current = DataManager.Instance.GetCurrentPlayerData();

        bool? flag = false;


        if (SLPrice < current.sl && CCPrice < current.cc && levelAvailability <= current.level)
        {

            if (health != 0 && armor == 0 && current.health < current.maxHealth)
            {
                flag = true;
            }


            if (armor != 0 && health == 0 && current.armor < current.maxArmor)
            {
                flag = true;
            }
        }
        else
        {
            flag = false;
        }

        button.interactable = (bool)flag;
    }

    public void PurchaseItem()
    {
        StoreTransaction transaction = new StoreTransaction(SLPrice, CCPrice, isIncremental, health, armor, defHFactor, defAFactor);

        OnPurchase(transaction);
    }

    // PROVIDED EVENTS _________________________________________________________ PROVIDED EVENTS
    public delegate void PurchaseEvent(StoreTransaction transaction);
    public static PurchaseEvent OnPurchase;


}
