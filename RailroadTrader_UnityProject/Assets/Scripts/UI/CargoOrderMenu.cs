using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoOrderMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject CargoOrderWindow;
    [SerializeField]
    private Button OrderButton;
    [SerializeField]
    private CargoTrainController CTC;
    [SerializeField]
    private Text MaxCapacityValue;    
    
    [SerializeField]
    private Text curFoodInShops;       
    [SerializeField]
    private Text curDrinksInShops;   
    [SerializeField]
    private Text curCargoInShops;    
    [SerializeField]
    private Text maxFoodInShops;       
    [SerializeField]
    private Text maxDrinksInShops;   
    [SerializeField]
    private Text maxCargoInShops;


    [System.Serializable]
    public class ValueInput
    {
        public Resource ressi;
        public Button increaseButton;
        public Button decreaseButton;
        public Text amountTxt;
    }

    public ValueInput[] arrowButtons = new ValueInput[3];

    void Start()
    {
        OnObjectClicked.OnCargoTrackClicked += OpenCargoWindow;
        BuildingManager.OnSupplyStoreCountChange += UpdateStockInShop;
        RessourceController.OnShopStockChange += ChangeStockInShops;
        OrderButton.onClick.AddListener(delegate { CheckOrder(); });
        //foreach(ValueInput v in arrowButtons)
        //{
        //        v.increaseButton.onClick.AddListener(delegate { ChangeAmountInTextfield(v.amountTxt, +1); });
        //        v.decreaseButton.onClick.AddListener(delegate { ChangeAmountInTextfield(v.amountTxt, -1); });
        //}
    }

    //Should add amount to the textfield value
    //TODO: Doesnt change amount in textfield - due to input field? research
    private void ChangeAmountInTextfield(Text text, int amount)
    {
        print("increasing by value " + amount);
        int curAmount;
        int.TryParse(text.text, out curAmount);
        curAmount += amount;
        print("new amount is "+ curAmount);
        text.text = curAmount.ToString();
    }

    private void OpenCargoWindow()
    {
        MaxCapacityValue.text = CTC.MaxCargoCapacity.ToString();
        CargoOrderWindow.SetActive(true);
    }

    private void ChangeStockInShops(Resource ressi, int amount)
    {
        switch (ressi)
        {
            case Resource.FOOD:
                curFoodInShops.text = amount.ToString();
                break;
            case Resource.BEVERAGE:
                curDrinksInShops.text = amount.ToString();

                break;
            case Resource.CARGO:
                curCargoInShops.text = amount.ToString();

                break;
            case Resource.STAFF:
                break;
            default:
                Debug.LogError("resource type not found: " + ressi.ToString());
                break;
        }
    }

    private void UpdateStockInShop(List<SupplyStores> allStores)
    {
        int food = 0;
        int maxFood = 0;
        int drinks = 0;
        int maxDrinks = 0;
        int cargo = 0;
        int maxCargo = 0;

        foreach(SupplyStores s in allStores)
        {
            food += s.FoodInStock();
            drinks += s.DrinksInStock();
            cargo += s.CargoInStock();
            maxFood += s.FoodMaxCapacity();
            maxDrinks += s.DrinksMaxCapacity();
            maxCargo += s.CargoMaxCapacity();
        }
        WriteCurShopInventory(food, drinks, cargo);
        WriteMaxShopInventory(maxFood, maxDrinks, maxCargo);
    }

    private void WriteCurShopInventory(int food, int drinks, int cargo)
    {
        curFoodInShops.text = food.ToString();
        curDrinksInShops.text = drinks.ToString();
        curCargoInShops.text = cargo.ToString();
    }

    private void WriteMaxShopInventory(int food, int drinks, int cargo)
    {
        maxFoodInShops.text = food.ToString();
        maxDrinksInShops.text = drinks.ToString();
        maxCargoInShops.text = cargo.ToString();
    }

    private void CheckOrder()
    {
        int food = 0;
        int drink = 0;
        int cargo = 0;

        foreach (ValueInput v in arrowButtons)
        {
            switch (v.ressi)
            {
                case Resource.FOOD:
                    int.TryParse(v.amountTxt.text, out food);
                    break;
                case Resource.BEVERAGE:
                    int.TryParse(v.amountTxt.text, out drink);
                    break;
                case Resource.CARGO:
                    int.TryParse(v.amountTxt.text, out cargo);
                    break;
                case Resource.STAFF:
                    break;
                default:
                    break;
            }
        }
        if (food + drink + cargo == 0)
        {
            print("cargo order is empty");
            return;
        }
        else if (food + drink + cargo > CTC.MaxCargoCapacity)
        {
            print("cargo order over capacity");
            return;
        }
        else
        {
            CTC.SaveOrder(food, drink, cargo);
            CargoOrderWindow.SetActive(false);
        }
    }
}
