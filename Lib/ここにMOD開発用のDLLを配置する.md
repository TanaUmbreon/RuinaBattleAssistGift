# Libフォルダーについて

このフォルダーは、 `Src` フォルダーにあるソースコードをビルドしてMODの実行ファイルを作成するために必須となる、各種ライブラリ (DLLファイル) を配置するためのフォルダーです。

ソースコードをビルドしたい場合、このフォルダーに以下のDLLファイルを配置してください。

- `Assembly-CSharp.dll` : ゲーム本体からコピペして配置 (\*1)
- `Unity.TextMeshPro.dll` : ゲーム本体からコピペして配置 (\*1)
- `UnityEngine.CoreModule.dll` : ゲーム本体からコピペして配置 (\*1)
- `UnityEngine.AnimationModule.dll` : ゲーム本体からコピペして配置 (\*1)
- `0Harmony.dll` : 「BaseMod for Workshop」からコピペして配置 (\*2)
- `Mono.Cecil.dll` : 「BaseMod for Workshop」からコピペして配置 (\*2)
- `MonoMod.RuntimeDetour.dll` : 「BaseMod for Workshop」からコピペして配置 (\*2)
- `MonoMod.Utils.dll` : 「BaseMod for Workshop」からコピペして配置 (\*2)
- `Newtonsoft.Json.dll` : GitHubからダウンロードして配置 (\*3)

\*1: Steamでゲーム本体を購入してダウンロードすると、一般的には `C:\Program Files (x86)\Steam\steamapps\common\Library Of Ruina\LibraryOfRuina_Data\Managed` フォルダーにこれらのDLLがインストールされます。

\*2: Steamワークショップで公開されているMOD「[BaseMod for Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=2603522001)」をサブスクライブすると、一般的には `C:\Program Files (x86)\Steam\steamapps\workshop\content\1256670\2603522001\Assemblies` フォルダーにこれらのDLLがインストールされます。ゲーム本体側でこのMODを有効にしてプレイする必要はなく、DLLのコピーのみを目的としています。

\*3: JamesNK/Newtonsoft.Jsonリポジトリの [「Releases」ページ](https://github.com/JamesNK/Newtonsoft.Json/releases) のAssetsからZIPファイルをダウンロードして解凍し、 `Bin\net20` フォルダー直下にあるDLLファイルを使用します。DLLのバージョンは「13.0.1」を使用していますが、このライブラリに破壊的な変更がない限りはセキュリティの観点上から最新版を使用してください。
