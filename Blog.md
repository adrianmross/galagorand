# SHMUP Unity Game with Algorand SDK

**Author:** Adrian Michael Ross

**Acknowledgements:** 
- Russ Fusitino, Algorand Foundation Developer Advocate who served as a mentor and played a major part in this project.
- Professor Hank Korth, Lehigh University Professor who acted as advisor for this project.
- Justin Fletcher, Unity Developer and YouTuber for the Gradius-like Demo.
- Jason Bou Kheir, Community Developer and Algorand SDK for Unity author, who guided me through the SDK.
- Gabriel Coleman, Community Developer, who helped me getting started with Unity.
- Frank Szendzielarz, Community Developer, who wrote the Algorand .NET SDK, as well as the Visual Studio extension and aided me in working with both.

Finished Unity Demo **repo:** https://github.com/adrianmross/galagorand
If you found this tutorial interesting or helpful and want to see more please leave a star!

**Disclaimer:** I’m not affiliated with Unity. This project is not audited and should not be used in a production environment. The Unity demo is a modified version of a [youtube tutorial](https://www.youtube.com/watch?v=rH9IHdp1dyU&list=PL6ynPcXXvDY_9nYW_S-nAliA1w1mARm8x&pp=iAQB) by Justin Fletcher. I highly recommend checking out his channel to learn more about Unity and game development. Consider supporting him too!

## Abstract

This document is a tutorial that will walk you through the process of building a Unity SHMUP game that uses the Algorand SDK to reward players with ASA Game Tokens. It provides functionality to generate Algorand accounts, check supporting clients, retrieve asset metadata, and interact with assets. The script also includes some additional functionality related to the game, such as new UI and refreshed animations.

## Requirements

1. An IDE to code. Visual Studio for Windows is recommended because of its integration with Unity and other tools in the ecosystem (like Algorand for Visual Studio "AlgoStudio" extension which comes in handy for smart contracts). Visual Studio Code also works with Unity. It is what I used, but requires an extensive setup.
2. Unity Hub and Unity, in addition to a basic understanding of how to develop Unity projects.
3. Algorand SDK for Unity. This is a Unity package that allows you to interact with the Algorand blockchain from within Unity. It is a wrapper around the Algorand SDK for C#. It is now downloadable from the [Unity Asset Store](https://assetstore.unity.com/packages/decentralization/infrastructure/algorand-sdk-247704).
4. Indexer such as AlgoExplorer or DappFlow to see the transactions on the blockchain.
5. Docker - used in running a testnet node. This is optional, but recommended. You can also use a node from a third party provider like AlgoExplorer.

## Background

### The Potential of ASAs

NFTs have taken the last few years by storm. They are a way to represent ownership of digital assets. They are unique, non-interchangeable, and can be used to represent objects outside of a standard token. However, recently the hype has stagnated due to a commonnly held perception that they are nothing more than 2d variations of an ape. While this is one use case, NFTs have the potential to be so much more. They can be used to represent ownership of physical assets, like a house or a car. They can be used to represent ownership of other digital asset, like a video game item or a song. They facilitate asset transfering, enable fractional ownership, and enable other possibilities like ongoing royalty payments and automated rental agreement.

Algorand is the perfect blockchain to build these NFTs on. Not only is it a fast, secure, and scalable blockchain, but it has a built-in feature that makes it easy to create and manage NFTs. It is called Algorand Standard Assets (ASA), which provides a standardized, Layer-1 mechanism to represent any type of asset on the Algorand blockchain.

### Scrolling SHMUP Game

The game we will be building is a scrolling shoot-em-up (SHMUP) game. The player controls a spaceship that can move up and down and shoot blaster beams. The goal of the game is to shoot down as many enemy spaceships as possible without getting hit. The player has three lives, and the game ends when the player loses all three lives. The player can collect power ups to increase their firepower and score. Read more about what makes a good [SHMUP game](https://www.racketboy.com/retro/shmups-101-a-beginners-guide-to-2d-shooters).

All assets and most of the Unity code was developed by Justin Fletcher. He has a step-by-step series that you can find [here](https://www.youtube.com/watch?v=rH9IHdp1dyU&list=PL6ynPcXXvDY_9nYW_S-nAliA1w1mARm8x&pp=iAQB).

This tutorial covers the building of a script using the Algorand SDK to reward players with ASA Game Tokens. The script is written in C# and is used in a Unity game to interact with the Algorand blockchain. It provides functionality to generate a new Algorand account, check the status of the Algod and Indexer clients, retrieve an asset from the blockchain, opt into the asset, and send the asset to another account. The script also includes some additional functionality related to the game, such as updating the balance and displaying it on the UI.

## Steps
1. Setup
2. Build the Algo UI
3. Starting the Algo.cs Script
4. Awake Method
5. Start Method
6. Check Status Methods
7. Get Asset Method
8. Opting Into the Asset Method
9. Sending the Asset Methods
10. Usage
11. Done!
12. A Building Block to Much More

1. Setup

### Help on Prerequisites

To get started, you need to have Unity installed. You can download Unity from [here](https://unity3d.com/get-unity/download). You also need to have the Algorand SDK for Unity installed. You can download it from the Unity Asset Store [here](https://assetstore.unity.com/packages/decentralization/infrastructure/algorand-sdk-247704).

#### Algorand SDK Pre-release Versions

Alternatively, to access the source code or pre-release versions, you can download the package from the [GitHub repository](https://github.com/CareBoo/unity-algorand-sdk).

Once cloned or downloaded, in the Unity editor, navigate to Assets > Import Package > Custom Package and select the Algorand SDK for Unity package. Take a look at the [documentation](https://github.com/CareBoo/unity-algorand-sdk#readme) to see how to configure the scope.

#### Visual Studio or Visual Studio Code for Unity

Unity uses the Mono runtime to run C# scripts. Mono is an open-source implementation of the .NET framework. Unity also provides a built-in code editor called MonoDevelop. However, it's recommended to use Visual Studio or Visual Studio Code for Unity development. To learn how to set up Visual Studio or Visual Studio Code for Unity development, check out the [documentation for VS](https://visualstudio.microsoft.com/vs/unity-tools/) or [VS Code](https://code.visualstudio.com/docs/other/unity).

### Open the Project

Next, we need the demo project. You can either clone this repository for the needed assets or download the unedited version from Justin's [project](https://ko-fi.com/s/599bd0fd0b). Please support him if you can!

Once you have the project, open it in Unity using the Unity Hub. Navigate to the Algorand SDK for Unity folder in the Project window. Follow the AlgorandSDK CreateAsas Sample to create a new creator account and game token. Make sure to record the asset ID and creator account mnemonic as we will need this later.

2. Build the Algo UI

The best way to add the Algo script to your Unity project is to create a Game Object that is persistent throughout the game. My recommendation would be to use a UI element that gets updated throughout the game. 

Create a prefab of a UI element that will be used to display the balance of the asset. We first need a canvas to hold the UI elements. Right click in the hierarchy and select UI > Canvas. Then right click on the canvas and select UI > Text. This will create a text element that will be used to display the balance. You can change the text to whatever you want, but make sure to keep the name as "BalanceText". You can also change the font, font size, and color.

3. Starting the Algo.cs Script

### Dependencies

The script requires the following dependencies:

- Algorand.Unity: A library that provides Algorand blockchain integration for Unity.
- Algorand.Unity.Indexer: A library that provides Algorand indexer integration for Unity.
- System.Collections: A standard library for working with collections in C#.
- UnityEngine: The Unity engine library.
- Cysharp.Threading.Tasks: A library for asynchronous programming in C#.
- UnityEngine.UI: A library for working with UI elements in Unity.

Based on all the prerequisites, you should be able to use all of these right away.

### Vulnerabilities

The most serious flaw is the use of local account generation and storing mnemonic in PlayerPrefs. This is a temporary solution, and NOT secure and should only be used for testing as PlayerPrefs are not secure storage and all local account objects can be reverse engineered. The alternative approach would be to use WalletConnect sessions.

The only place to hold the asset creator's private key is on a game server. Therefore, the `SendAsset` method should send a request to the server to send the asset. The server should then create the transaction and send it to the Algorand blockchain.

We can further use a smart contract and by default have the ASA as frozen to manage the game token. This will allow us to create a leaderboard and other game logic. We can also use the smart contract to manage the game token and ensure that the game token is not sent to an account that is not a player.

### Class Structure

The `Algo` class extends the `MonoBehaviour` class, which is the base class for Unity scripts. It includes various fields, methods, and lifecycle callbacks to interact with the Algorand blockchain and handle game-related functionality.

A base Unity script has a `Start()` method that is called before the first frame update and an `Update()` method that is called once per frame. However, since we are using the indexer, we need to use asynchronous methods that perform outside of the main thread as may need to wait due to network calls. Therefore, we will be using the `Awake()` method to initialize the singleton instance and the `Start()` method to initialize the Algorand account and check the status of the Algod and Indexer clients. 

We will also be using the `Update()` method to check when the player finishes all the levels and call the `WrappedSendAsset()` method to initiate the process of sending the asset to another account. The `WrappedSendAsset()` method starts a coroutine named `waiter` that waits for 2 seconds before sending the asset. A coroutine is a function that can suspend its execution until the given yield instruction completes. This is to ensure that the asset is created before sending it.

As our opt in and send asset methods are asynchronous, we will be using the `async` and `await` keywords to make the methods asynchronous and wait for the response before proceeding. We will also be using the `UniTask` class to make the methods asynchronous. The `UniTask` class is a library that provides a unified task API for Unity. It is a wrapper around the `Task` class and provides a more efficient way to work with tasks in Unity.

### Fields

- `instance`: A static field representing the singleton instance of the `Algo` class.
- `algod`: An instance of the `AlgodClient` class used for interacting with the Algod client.
- `indexer`: An instance of the `IndexerClient` class used for interacting with the Indexer client.
- `algodHealth`: A string representing the health status of the Algod client.
- `indexerHealth`: A string representing the health status of the Indexer client.
- `account`: An instance of the `Algorand.Unity.Account` class representing the Algorand account.
- `txnStatus`: A string representing the status of a transaction.
- `asset`: An instance of the `Asset` class representing an asset on the Algorand blockchain.
- `balance`: An integer representing the balance of the asset.
- `balanceText`: A `Text` component representing the UI element to display the balance.
- `assetId`: An unsigned long representing the ID of the game token ASA (Algorand Standard Asset) on the testnet. **Update** this value with the asset ID of the game token ASA you created earlier.

4. Awake Method

```csharp
// Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            balanceText = GameObject.Find("BalanceText").GetComponent<Text>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
```

 `Awake()`: This method is called when the script instance is being loaded. It initializes the singleton instance, sets the `balanceText` field by finding the corresponding UI element, and generates or retrieves the Algorand account based on whether the account mnemonic is stored in PlayerPrefs.

5. Start Method

```csharp
// Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        // if account not connected, generate new account
        // this is a temporary solution, and NOT secure and should only be used for testing. wallet connect will be implemented in the future
        if (PlayerPrefs.GetString("users_local_mnemonic") == "")
        {
            Debug.Log("Generating new account");
            var (privateKey, address) = Algorand.Unity.Account.GenerateAccount();
            account = new Algorand.Unity.Account(privateKey);
            Debug.Log($"My address: {account.Address}");
            var mnemonic = privateKey.ToMnemonic();
            Debug.Log($"My mnemonic: {mnemonic}");
            UnityEngine.Application.OpenURL($"https://dispenser.testnet.aws.algodev.network/?account={account.Address}"); // dispenser for testing
            PlayerPrefs.SetString("users_local_mnemonic", mnemonic.ToString());
            PlayerPrefs.Save(); // NOT SECURE

            // AssetAccept
            AcceptAsset().Forget();
        }
        else
        {
            Debug.Log("Account already exists");
            Mnemonic mn = Mnemonic.FromString(PlayerPrefs.GetString("users_local_mnemonic"));
            account = new Algorand.Unity.Account(mn.ToPrivateKey());
        }
        // replace with your node address
        algod = new AlgodClient("https://node.testnet.algoexplorerapi.io");
        CheckAlgodStatus().Forget();
        CheckIndexerStatus().Forget();
    }
```

`Start()`: This method is called before the first frame update. It initializes the `algod` client, checks the status of the Algod and Indexer clients, and performs asset acceptance if a new account is generated.

6. Check Status Methods

```csharp
// Check the status of the algod client
    public async UniTaskVoid CheckAlgodStatus()
    {
        var response = await algod.HealthCheck();
        if (response.Error) algodHealth = response.Error;
        else algodHealth = "Connected";
    }
```

`CheckAlgodStatus()`: This method asynchronously checks the status of the Algod client and updates the `algodHealth` field accordingly.

```csharp
// Check the status of the indexer client
    public async UniTaskVoid CheckIndexerStatus()
    {
        var response = await indexer.MakeHealthCheck();
        if (response.Error) indexerHealth = response.Error;
        else indexerHealth = "Connected";
    }
```

`CheckIndexerStatus()`: This method asynchronously checks the status of the Indexer client and updates the `indexerHealth` field accordingly.

7. Get Asset Method

```csharp
// Get the asset
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
```

`GetAsset()`: This method asynchronously retrieves the asset from the Algorand blockchain using the `indexer` client and updates the `asset` field.

Since we are using the indexer we need to `await` the response. The indexer is a separate service that indexes the blockchain and provides a fast and efficient way to query the blockchain. Therefore all calls to the indexer are asynchronous and cannot be run in the `Start()` method.

8. Opting Into the Asset Method

Before we can send the asset to another account, we need to opt into the asset by sending a transaction to the Algorand blockchain. This is done by calling the `AssetAccept` method on the `Algorand.Unity.Account` instance.

```csharp
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
```

`AcceptAsset()`: This method asynchronously opts into the asset by sending a transaction to the Algorand blockchain.


When the code is run, the following transaction can be seen on the Algorand testnet:
![image]("blog-images/opt-in screenshot.png")

9. Sending the Asset Methods

The principal method for sending the asset to another account is the `SendAsset` method.

```csharp
// Send the asset to the user
    public async UniTaskVoid SendAsset(string receiver, Address sender, ulong amount)
    {
        // make sender account from mnemonic, this is a temporary solution, and NOT secure and should only be used for testing. a game server is the only place to hold the private key
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
```

`SendAsset(string receiver, Address sender, ulong amount)`: This method asynchronously sends the asset to the specified receiver by creating and sending a transaction to the Algorand blockchain. It also updates the balance and the `balanceText` UI element

You then need to call this method from the `waiter` coroutine so that the asset is first indexed before sending it.

```csharp
// Waiter is used to wait for 2 seconds before sending the asset, this is to ensure that the asset is created before sending
    IEnumerator waiter()
    {
        GetAsset().Forget();
        //Wait for 2 seconds
        yield return new WaitForSeconds(1);
        
        Address assetCreator = asset.Params.Creator;
        ulong assetAmount = ulong.Parse(asset.Params.Total.ToString());

        Debug.Log($"Asset amount: {assetAmount}");
        // reward amount, can be changed and based on game logic
        ulong rewardAmount = 10;
        if (assetAmount - rewardAmount < 0)
        {
            Debug.Log("Not enough ASA to send");
        }
        // send asa to user, this is a temporary solution, and NOT secure and should only be used for testing. a game server is the only place to hold the private key and therefore this operation should send a request to the server to send the asset
        else
        SendAsset(account.Address, assetCreator, 10).Forget();
    }
```

`waiter()`: This coroutine is used to wait for 2 seconds before sending the asset. It retrieves the asset and then waits for the specified time before calling the `SendAsset` method.

`WrappedSendAsset()` is called to initiate the process of sending the asset to another account. It starts a coroutine named `waiter` and is called from the `Update()` method in `Level.cs` when the player finishes all the levels.

When the code is run, the following transaction can be seen on the Algorand testnet:
![image]("blog-images/send-asset screenshot.png")

10. Usage

To use this script, follow these steps:

1. Import the required dependencies (`Algorand.Unity`, `Algorand.Unity.Indexer`, `System.Collections`, `UnityEngine`, `Cysharp.Threading.Tasks`, and `UnityEngine.UI`) into your Unity project.
2. Attach the `Algo` script to a GameObject in your scene.
3. Make sure you have a UI element with the name "BalanceText" to display the balance.
4. Ensure that you have a valid Algorand node URL and indexer URL specified in the code (replace the placeholders with the correct URLs).
5. Call the appropriate methods from other scripts or UI buttons to interact with the Algorand blockchain (e.g., `WrappedSendAsset()` to initiate the asset transfer process).
6. Customize the code as per your specific requirements and integrate it into your game logic.

Please note that this documentation provides a general overview of the script's functionality. It's recommended to have a good understanding of Algorand blockchain integration and Unity development before using this script in a production environment.

11. Done!

You can use the Unity editor to run the game. You can also build the game for Windows, Mac, or Linux. To do this, go to File > Build Settings, select the platform you want to build for, and click Build. This will create a build in the specified folder. You can even run a test run by just clicking the play button in the Unity editor. View the debug logs in the console to see the status of the Algorand client and the transactions.

Finished Dapp repo: https://github.com/adrianmross/galagorand
If this tutorial is helpful at all please leave a star :)

12. A Building Block to Much More
Some ideas that you can implement to improve this demo and make an even more complex game ready for production:

Add a wallet to the game so that players can see their balance and send the ASA to other accounts.
Add a leaderboard to the game so that players can see the top players using a smart contract.
Add a game server to the game so the private key of the transaction is not stored on the client side.
Refactor. This is just one solution, but it’s definitely not the best solution. Improve it or write a better solution!