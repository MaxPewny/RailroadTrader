using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoOrderMenu : MonoBehaviour
{
    public Button OrderButton;
    [SerializeField]
    private CargoTrainController CTC;

    void Start()
    {
        OrderButton.onClick.AddListener(delegate { CTC.ConfirmOrder(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
