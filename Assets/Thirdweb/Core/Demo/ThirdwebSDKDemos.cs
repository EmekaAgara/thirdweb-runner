using UnityEngine;
using Thirdweb;
using System.Collections.Generic;
using UnityEngine.UI;

public class ThirdwebSDKDemos : MonoBehaviour
{
    private ThirdwebSDK sdk;
    private int count;
    public Text walletInfotext;
    public GameObject connectButtonsContainer;
    public GameObject walletInfoContainer;
    public Text resultText;

    void Start()
    {
        sdk = new ThirdwebSDK("goerli");
        InitializeState();
    }

    private void InitializeState()
    {
        connectButtonsContainer.SetActive(true);
        walletInfoContainer.SetActive(false);
    }

    void Update()
    {
    }

    public void MetamaskLogin()
    {
        ConnectWallet(WalletProvider.MetaMask);
    }

    public void CoinbaseWalletLogin()
    {
        ConnectWallet(WalletProvider.CoinbaseWallet);
    }

    public void WalletConnectLogin()
    {
        ConnectWallet(WalletProvider.WalletConnect);
    }


    public async void DisconnectWallet()
    {
        await sdk.wallet.Disconnect();
        connectButtonsContainer.SetActive(true);
        walletInfoContainer.SetActive(false);
    }

    private async void ConnectWallet(WalletProvider provider)
    {
        connectButtonsContainer.SetActive(false);
        walletInfoContainer.SetActive(true);
        walletInfotext.text = "Connecting...";
        try
        {
            string address = await sdk.wallet.Connect(new WalletConnection()
            {
                provider = provider,
                chainId = 5 // Switch the wallet Goerli on connection
            });
            walletInfotext.text = "Connected as: " + address;
        }
        catch (System.Exception e)
        {
            walletInfotext.text = "Error (see console): " + e.Message;
        }
    }

    public async void OnBalanceClick()
    {
        resultText.text = "Loading...";
        CurrencyValue balance = await sdk.wallet.GetBalance();
        resultText.text = "Balance: " + balance.displayValue.Substring(0, 5) + " " + balance.symbol;
    }


    public async void GetERC20()
    {
        var contract = sdk.GetContract("0x021Ae3f484eb6D65B8fd6383391dFACAb41cea8f"); // Token
        resultText.text = "Fetching Token info";
        Currency result = await contract.ERC20.Get();
        CurrencyValue currencyValue = await contract.ERC20.TotalSupply();
        resultText.text = result.name + " (" + currencyValue.displayValue + ")";
    }

    public async void GetRUTBalance()
    {
        string address = await sdk.wallet.Connect();
        var contract = sdk.GetContract("0x021Ae3f484eb6D65B8fd6383391dFACAb41cea8f"); // Token
        resultText.text = "Fetching RUT Balance";
        Currency result = await contract.ERC20.Get();
        CurrencyValue currencyValue = await contract.ERC20.Balance();
        resultText.text = result.symbol + " (" + currencyValue.displayValue + ")";
    }

    public async void BuyRutTokens(string amount)
    {
        //  First, connect the user's wallet.
        //string address = await sdk.wallet.Connect();
        resultText.text = "Buying RUT Tokens kindly approve transaction";
        string address = await sdk.wallet.Connect();
        // Now, let's connect to the Token Drop smart contract.
        // We can get the smart contract's address from the dashboard.
        Contract contract =
        sdk.GetContract("0x021Ae3f484eb6D65B8fd6383391dFACAb41cea8f");

        // We've got a wallet and a smart contract, so let's mint some tokens!
        await contract.ERC20.Claim(amount);
        resultText.text = "Congratulations 1 RUT Received !!";

    }

    public async void ClaimRutTokens()
    {
        resultText.text = "Claiming RUT Tokens";
        string address = await sdk.wallet.Connect();
        Contract contract = sdk.GetContract("0x021Ae3f484eb6D65B8fd6383391dFACAb41cea8f");
        var amount = "1.5";
        await contract.ERC20.Claim(amount);
        resultText.text = "Congratulations You have claimed your reward !!";
    }

}
