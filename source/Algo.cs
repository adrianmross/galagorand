using Algorand;
using Algorand.Unity;
using Algorand.Unity.Indexer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;
using Cysharp.Threading.Tasks.CompilerServices;

public class Algo : MonoBehaviour
{
    public static Algo instance;

    private AlgodClient algod = new AlgodClient("https://testnet-api.algonode.cloud/");
    private IndexerClient indexer = new IndexerClient("https://testnet-idx.algonode.cloud/");
    private string algodHealth;
    private string indexerHealth;

    private Algorand.Unity.Account account;
    private string txnStatus;

    private Asset asset;

    int balance = 0;
    Text balanceText;

    Image connectionColor;
    TextMeshProUGUI connectionText;


    ulong assetId = 0 // REPLACE with GameToken ID from Step 5

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            balanceText = GameObject.Find("BalanceText").GetComponent<Text>();

            // connection
            connectionColor = GameObject.Find("ConnectionButton").GetComponent<Image>();
            connectionText = GameObject.Find("ConnectionText").GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");

        balanceText = GameObject.Find("BalanceText").GetComponent<Text>();

        // connection
        connectionColor = GameObject.Find("ConnectionButton").GetComponent<Image>();
        connectionText = GameObject.Find("ConnectionText").GetComponent<TextMeshProUGUI>();

        connectionColor.color = Color.red;
        connectionText.text = "NOT CONNECTED";

        // if account not connected, generate new account

        if (PlayerPrefs.GetString("users_local_mnemonic") == "")
        {
            Debug.Log("Generating new account"); 
            var (privateKey, address) = Algorand.Unity.Account.GenerateAccount();
            account = new Algorand.Unity.Account(privateKey);
            Debug.Log($"My address: {account.Address}");
            var mnemonic = privateKey.ToMnemonic();
            Debug.Log($"My mnemonic: {mnemonic}");
            UnityEngine.Application.OpenURL($"https://dispenser.testnet.aws.algodev.network/?account={account.Address}");
            PlayerPrefs.SetString("users_local_mnemonic", mnemonic.ToString());
            PlayerPrefs.Save(); // is this secure storage?

            // AssetAccept
            AcceptAsset().Forget();
        }
        else
        {
            Debug.Log("Account already exists");
            Mnemonic mn = Mnemonic.FromString(PlayerPrefs.GetString("users_local_mnemonic"));
            account = new Algorand.Unity.Account(mn.ToPrivateKey());
        }

        CheckAlgodStatus().Forget();
        CheckIndexerStatus().Forget();

        WrappedConnection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WrappedSendAsset()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        GetAsset().Forget();

        //Wait for 1 second(s)
        yield return new WaitForSeconds(1);
        
        Address assetCreator = asset.Params.Creator;
        ulong assetAmount = ulong.Parse(asset.Params.Total.ToString());

        Debug.Log($"Asset amount: {assetAmount}");
        // reward amount
        ulong rewardAmount = 10;
        if (assetAmount - rewardAmount < 0)
        {
            Debug.Log("Not enough ASA to send");
        }
        // send asa to user
        SendAsset(account.Address, assetCreator, 10).Forget();
    }

    public void WrappedConnection()
    {
        try
        {
            GetAsset().Forget();
        }
        finally 
        {
        StartCoroutine(getConnection());
        }
    }

    IEnumerator getConnection()
    {
        yield return new WaitForSeconds(2);
        try
        {
            CheckAlgodStatus().Forget();
            CheckIndexerStatus().Forget();

            connectionColor.color = new Color(0.3f, 0.85f, 0.3f, 1f);
            connectionText.text = "CONNECTED";
        }
        catch
        {
            connectionColor.color = Color.red;
            connectionText.text = "NOT CONNECTED";
        }
        
    }

    public async UniTaskVoid CheckAlgodStatus()
    {
        var response = await algod.HealthCheck();
        if (response.Error) algodHealth = response.Error;
        else algodHealth = "Connected";
    }

    public async UniTaskVoid CheckIndexerStatus()
    {
        var response = await indexer.MakeHealthCheck();
        if (response.Error) indexerHealth = response.Error;
        else indexerHealth = "Connected";
    }

    public async UniTaskVoid GetAsset()
    {
        var apiResponse = await indexer.LookupAssetByID(assetId);
        if (apiResponse.Error) Debug.LogError(apiResponse.Error);
        else {
            Debug.Log("Successfully retrieved asset");
            Debug.Log($"Asset: {apiResponse.GetText()}");
            AssetResponse assetResponse = apiResponse.Payload;
            asset = assetResponse.Asset;
        }
        Debug.Log($"Asset: {asset.Params.Creator.ToString()}");
    }

    // Opt into the asset
    public async UniTaskVoid AcceptAsset()
    {
        var opter = $"{account.Address}";
        var algod = new AlgodClient("https://node.testnet.algoexplorerapi.io");
        var (txnParamsError, txnParams) = await algod.TransactionParams();
        if (txnParamsError)
        {
            Debug.LogError(txnParamsError);
            txnStatus = $"error: {txnParamsError}";
            return;
        }
        var xferAsset = assetId;
        var optinTxn = Algorand.Unity.Transaction.AssetAccept(opter, txnParams: txnParams, xferAsset);
        var signedTxn = account.SignTxn(optinTxn);

        // Send the transaction
        var (sendTxnError, txid) = await algod.SendTransaction(signedTxn);
        if (sendTxnError)
        {
            Debug.LogError(sendTxnError);
            txnStatus = $"error: {sendTxnError}";
            return;
        }
        else
        {
            Debug.Log("Sender Account = " + account.Address);
            Debug.Log("Transaction sent, TxID = " + txid.TxId.ToString());
        }

        // Wait for the transaction to be confirmed
        var (confirmErr, confirmed) = await algod.WaitForConfirmation(txid.TxId);
        if (confirmErr)
        {
            Debug.LogError(confirmErr);
            txnStatus = $"error: {confirmErr}";
            return;
        }
    }

    // Send the asset to the user
    public async UniTaskVoid SendAsset(string receiver, Address sender, ulong amount)
    {
        // make sender account from mnemonic
        Mnemonic senderMnemonic = Mnemonic.FromString("...");
        Algorand.Unity.Account senderAccount = new Algorand.Unity.Account(senderMnemonic.ToPrivateKey());

        var algod = new AlgodClient("https://node.testnet.algoexplorerapi.io");
        var (txnParamsError, txnParams) = await algod.TransactionParams();
        if (txnParamsError)
        {
            Debug.LogError(txnParamsError);
            txnStatus = $"error: {txnParamsError}";
            return;
        }
        var xferAsset = assetId;

        var xferTxn = Algorand.Unity.Transaction.AssetTransfer(sender,txnParams, xferAsset, amount, receiver);
        var signedTxn = senderAccount.SignTxn(xferTxn);

        // Send the transaction
        var (sendTxnError, txid) = await algod.SendTransaction(signedTxn);
        if (sendTxnError)
        {
            Debug.LogError(sendTxnError);
            txnStatus = $"error: {sendTxnError}";
            return;
        }
        else
        {
            Debug.Log("Sender Account = " + account.Address);
            Debug.Log("Transaction sent, TxID = " + txid.TxId.ToString());
        }

        // Wait for the transaction to be confirmed
        var (confirmErr, confirmed) = await algod.WaitForConfirmation(txid.TxId);
        if (confirmErr)
        {
            Debug.LogError(confirmErr);
            txnStatus = $"error: {confirmErr}";
            return;
        }

        // update balance
        balance += (int)amount;
        balanceText.text = balance.ToString();
    }
}
