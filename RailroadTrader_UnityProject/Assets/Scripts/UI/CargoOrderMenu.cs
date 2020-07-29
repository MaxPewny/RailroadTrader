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

    //[SerializeField]
    //private Text foodOrderAmount;
    //[SerializeField]
    //private Text drinkOrderAmount;
    //[SerializeField]
    //private Text cargoOrderAmount;

    [System.Serializable]
    public class ValueChanger
    {
        public Resource ressi;
        public Button increaseButton;
        public Button decreaseButton;
        public Text amountTxt;
    }

    public ValueChanger[] arrowButtons = new ValueChanger[3];

    void Start()
    {
        OnObjectClicked.OnCargoTrackClicked += OpenCargoWindow;
        OrderButton.onClick.AddListener(delegate { CheckOrder(); });
        foreach(ValueChanger v in arrowButtons)
        {
                v.increaseButton.onClick.AddListener(delegate { ChangeAmountInTextfield(v.amountTxt, +1); });
                v.decreaseButton.onClick.AddListener(delegate { ChangeAmountInTextfield(v.amountTxt, -1); });
        }
    }

    //Adds amount to the textfield value
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

    private void CheckOrder()
    {
        int food = 0;
        int drink = 0;
        int cargo = 0;

        foreach (ValueChanger v in arrowButtons)
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
