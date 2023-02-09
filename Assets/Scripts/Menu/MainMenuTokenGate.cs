using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class MainMenuTokenGate : MonoBehaviour
{
    private ThirdwebSDK sdk;
    public Text walletInfotext;
    public GameObject connectButtonsContainer;
    public GameObject walletInfoContainer;
    public Text resultText;


    void Start()
    {
    }

    void Update()
    {
    }

    public async void ConnectWallet()
    {
        string address = await sdk.wallet.Connect();

        await CheckBalance();
    }

    public async Task CheckBalance()
    {
        Contract contract = sdk.GetContract("0x021Ae3f484eb6D65B8fd6383391dFACAb41cea8f");
        Currency result = await contract.ERC20.Get();
        CurrencyValue currencyValue = await contract.ERC20.Balance();
        //resultText.text = result.symbol + " (" + currencyValue.displayValue + ")";

        float balanceFloat = float.Parse("await contract.ERC20.Balance()");

        if (balanceFloat == 3)
            
        resultText.text = result.symbol + " (" + currencyValue.displayValue + ")";
        return;

    }
}
