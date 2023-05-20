using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algorand.Unity;
using System.Threading.Tasks;

public class PowerUp : MonoBehaviour
{
    public int pointValue = 1000;
    public bool activateShield;
    public bool addGuns;
    public bool increaseSpeed;

    // private Account yourAccount;
    // private AlgodClient algod = new AlgodClient("https://node.testnet.algoexplorerapi.io");

    // private string smartContractAddress = "YOUR_SMART_CONTRACT_ADDRESS_HERE";
    // private string yourAlgorandAddress = "YOUR_ALGORAND_ADDRESS_HERE";

    // Start is called before the first frame update
    void Start()
    {
        // CheckAlgodStatus().Forget();
        // var maxAlgoAmount = 10_000_000_000 * MicroAlgos.PerAlgo;

        // // restore an account object
        // string creatorMnemonic = "word word word word";
        // var mnemonic = Mnemonic.FromString(creatorMnemonic);
        // var sk = mnemonic.ToPrivateKey();
        // yourAccount = new Account(sk);

        // recipient = "HZ57J3K46JIJXILONBBZOHX6BKPXEM2VVXNRFSUED6DKFD5ZD24PMJ3MVA";
        // payAmount = 1000;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // private async Task MintPowerup()
    // {
    //     Account yourAccount = new Account(yourAlgorandPrivateKey);

    //     TransactionParametersResponse txnParams = await algodApiInstance.TransactionParamsAsync().Do();
    //     var suggestedFeePerByte = await algodApiInstance.SuggestedFeeAsync().Do();
    //     long suggestedFee = suggestedFeePerByte.Fee * txnParams.SuggestedParams.Fee;
        
    //     Transaction powerupTxn = new Transaction(
    //         yourAlgorandAddress,
    //         txnParams,
    //         smartContractAddress,
    //         0,
    //         new byte[0],
    //         suggestedFee,
    //         suggestedFeePerByte.Fee,
    //         suggestedFeePerByte.LastRound
    //     );
        
    //     powerupTxn.Sign(yourAccount);

    //     try
    //     {
    //         var rawTx = Utils.EncodeToJson(powerupTxn);
    //         var rawResponse = await algodApiInstance.RawTransactionAsync(rawTx).Do();
    //         var response = Utils.DecodeJson<SendRawTransactionResponse>(rawResponse.RawBytes);
    //         Debug.Log("Transaction ID: " + response.TxId);
    //     }
    //     catch (ApiException e)
    //     {
    //         Debug.Log("Exception when calling algod#rawTransaction: " + e.Message);
    //     }
    // }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         // Mint a new powerup token
    //         Task.Run(MintPowerup);

    //         // Update the UI elements for the collected powerups
    //         if (activateShield)
    //         {
    //             // Increment the shield powerup counter and update the UI
    //         }
    //         else if (addGuns)
    //         {
    //             // Increment the guns powerup counter and update the UI
    //         }
    //         else if (increaseSpeed)
    //         {
    //             // Increment the speed powerup counter and update the UI
    //         }

    //         // Destroy the powerup object
    //         Destroy(gameObject);
    //     }
    // }
}
