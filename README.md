# Galagorand

An Algorand x Unity Solution Repository.

The step-by-step tutorial for this solution can be found on the official Algorand Developer Portal. [Link 🔗](https://developer.algorand.org/solutions/shmup-unity-game-with-algorand-sdk/)

## Table of Contents

- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Getting Started

This repository contains a Unity project that serves as a solution for creating a scrolling shoot-em-up (SHMUP) game integrated with the Algorand blockchain. This game can serve as a foundation for more complex and feature-rich projects, including adding wallets, leaderboards, game servers, and further refactoring for improved functionality.

## Prerequisites

To work with this Unity solution, you need to meet the following requirements:

1. **Integrated Development Environment (IDE):** You should have an IDE to write and manage your code. Visual Studio for Windows is recommended due to its Unity integration and the AlgoStudio extension for smart contracts. Visual Studio Code is also compatible but requires additional setup.

2. **Unity Hub and Unity:** You need to have Unity and Unity Hub installed. Make sure you have a basic understanding of Unity project development.

3. **Algorand SDK for Unity:** This Unity package enables interaction with the Algorand blockchain within your Unity project. It serves as a wrapper around the Algorand SDK for C#. You can download it from the [Unity Asset Store](https://assetstore.unity.com/packages/decentralization/infrastructure/algorand-sdk-247704).

4. **Algorand Indexer:** To view transactions on the blockchain, you need an indexer like AlgoExplorer or DappFlow. These tools provide visibility into blockchain activities.

5. **Docker (Optional but recommended):** You can use Docker to run a testnet node, although third-party providers like AlgoExplorer also offer nodes for testing.

## Installation

Follow these steps to set up and use this Unity solution:

### Getting Started

To begin, ensure you have Unity installed and follow these steps:

#### Create with Unity in three steps

1. Download Unity Hub from [https://unity.com/download/](https://unity.com/download/).

2. Select your desired Unity version (for example, `2022.3.4f1`) and create a new project using the 2D Core template.

### Setup Algorand Unity SDK

Install the Algorand SDK for Unity:

1. In Unity Hub, access the Unity Asset Store by clicking on "Community" in the sidebar or visiting [https://assetstore.unity.com/](https://assetstore.unity.com/).

2. Search for "Algorand SDK" and download the package from the Unity Asset Store.

3. Back in Unity Hub, open your project.

4. In the Unity editor, navigate to the "Packages: Unity Registry" dropdown in the left-hand corner and go to "My Assets."

5. Refresh the list if necessary, then click "Algorand SDK" and choose "Download."

6. Once downloaded, select "Import" to add the package to your project. Confirm the import.

### Setup Game Assets

Load the pre-made game assets:

1. Visit the [repository](https://github.com/adrianmross/galagorand/) associated with this tutorial.

2. Download the `galagorand-game-assets.unitypackage` from the root of the repository.

3. In Unity's navbar, navigate to "Assets" > "Import Package" > "Custom Package..." and select the downloaded package.

4. Confirm the import of the assets.

### Configuring Unity

Configure your Unity project:

1. Within the "Project Window" under `./Assets`, locate the "Scenes" folder, which contains five `.unity` scene files.

2. Delete the default "SampleScene" if it's present.

3. In the Unity editor, navigate to "File" > "Build Settings..."

4. In the "Build Settings" menu, edit the "Scenes in Build" section. Uncheck "SampleScene" if it's still listed.

5. Double-click each scene in `Assets/Scenes` (excluding "SampleScene") to load them into the editor. Then, in the "Build Settings" menu, click "Add Open Scenes" for each scene in the following order:
   - 0 - `MainMenu`
   - 1 - `Level1`
   - 2 - `Level2`
   - 3 - `Level3`

6. Set Visual Studio as the external script editor:
   - In Unity's navbar, go to "Edit" > "Preferences."
   - In the "Preferences" menu, under "Analysis," go to "External Tools."
   - Select "Microsoft Visual Studio" if it's installed on your machine.

### Creating Game Tokens

Before players and the demo can receive tokens, you, as the game creator, must create game tokens:

1. With the Algorand Unity SDK installed in your Unity project, follow the "CreateASAs" tutorial from the Algorand Unity SDK documentation: [CreateASAs Tutorial](https://careboo.github.io/unity-algorand-sdk/4.1/manual/algorand_standard_assets/creating_asas_in_editor.html).

2. From the tutorial, obtain the following information:
   - The index of your GameToken asset (your assetId).
   - The creator account address and mnemonic (creator account).

### Build the Algo UI (Optional)

If you imported the game assets from the repository, the "AlgoUserInterface" prefab is already included. To understand how to create it manually, follow these steps:

1. Create a prefab of a UI element that will display the asset balance.

2. Create a canvas to hold UI elements by right-clicking in the hierarchy and selecting "UI" > "Canvas."

3. Right-click the canvas and select "UI" > "Text" to create a text element for displaying the balance. Name it "BalanceText" and customize its appearance.

### Coding the Algo.cs Script

Refer to the `Algo.cs` script in the [GitHub repository](#) for the full code. Modify the following components as needed:

- Set the `assetId` obtained in [Step 5](#5-creating-game-tokens) for the asset you created.
- Adjust the `senderMnemonic` in the `SendAsset()` method.

#### Dependencies

The script relies on the following dependencies:

- Algorand.Unity: Provides Algorand blockchain integration for Unity.
- Algorand.Unity.Indexer: Offers Algorand indexer integration for Unity.
- System.Collections: Standard library for collections in C#.
- UnityEngine: Unity engine library.
- Cysharp.Threading.Tasks: Library for asynchronous programming in C#.
- UnityEngine.UI: Library for working with UI elements in Unity.
- TMPro: Library for advanced Text GUI elements in Unity.

Ensure that you have these dependencies correctly configured.

#### Vulnerabilities

The script temporarily uses local account generation and stores the mnemonic in PlayerPrefs, which is not secure and is suitable for testing purposes only. A more secure approach involves using WalletConnect sessions or a game server to manage private keys and transactions.

## Usage

To use the `Algo` script and integrate it into your Unity project, follow these steps:

1. Import the required dependencies into your Unity project as outlined in [Step 2](#2-setup-algorand-unity-sdk).

2. Attach the `Algo` script to a GameObject in your scene:

   - Open the "AlgorandUserInterface" prefab.
   - In the Inspector, click "Add Component."
   - Search for "Algo" and add it.

3. Ensure you have a UI element named "BalanceText" to display the balance.

4. Verify that your Algorand node URL and indexer URL are correctly specified in the code, replacing placeholders with the actual URLs.

5. Call the appropriate methods from other scripts or UI buttons to interact with the Algorand blockchain. For example, use `WrappedSendAsset()` to initiate the asset transfer process.

6. Customize the code to meet your requirements and integrate it into your game logic.

Please note that this documentation provides a general overview of the script's functionality. It's advisable to have a solid understanding of Algorand blockchain integration and Unity development before deploying this script in a production environment.

## Contributing

You're welcome to contribute to this Unity solution repository. Feel free to open issues, suggest improvements, or submit pull requests.

## License

This Unity solution repository is licensed under the [MIT License](#). You are free to use, modify, and distribute the code as per the terms of the license.

## Acknowledgement

* Russ Fusitino, Algorand Foundation Developer Advocate was a mentor and played a major part in this project.
* Professor Hank Korth, Lehigh University Professor, who was advisor for this project.
* Justin Fletcher, Unity Developer and YouTuber for the Gradius-like Demo.
* Jason Bou Kheir, Community Developer and Algorand SDK for Unity author, who guided me through the SDK.
* Gabriel Coleman, Community Developer, who helped me get started with Unity.
* Frank Szendzielarz, Community Developer, who wrote the Algorand .NET SDK, as well as the Visual Studio extension, and aided me in working with both.

## Release History

- Version 1.0.0 (08/31/2023)
  - Initial release with ASA opting, reading, and transferring

## Additional Resources

* Need a refresher on Algorand blockchain integration? Here is a [playlist](https://www.youtube.com/@algodevs/playlists) for you.
* Have a look at all other Algorand Microsoft Technology tools [here](https://developer.algorand.org/articles/algorand-microsoft-developer-tools/)!

---

If you found this documentation helpful, please consider leaving a star on the repository.

