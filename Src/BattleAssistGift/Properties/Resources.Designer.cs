﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BattleAssistGift.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BattleAssistGift.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   すべてについて、現在のスレッドの CurrentUICulture プロパティをオーバーライドします
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   {
        ///  // [前提]
        ///  // この設定ファイルは JSON 形式 (コメント可能な JSON 形式) で記述しています。
        ///  // JSON の記述ルールについては説明を割愛します。
        ///  // このコメントでは当 MOD で使用する各設定項目の概要と仕様を説明しています。
        ///
        ///  // [この設定ファイルがリロードされるタイミング]
        ///  // ・舞台の開始時
        ///  // ・味方キャラクターに追加される特殊バトルページ「設定リロード」の装着時
        ///
        ///
        ///  // 戦闘表象「月光の輪」の効果が有効であることを表すフラグです。
        ///  // true の場合は、設定項目 &quot;ApplyingEffectName&quot; および &quot;Effects&quot; に指定された値に基づいて、各キャラクターが効果を発揮します。
        ///  // false の場合は、全てのキャラクターが効果を発揮しません。
        ///  // また、どちらの値であっても、全ての味方キャラクターに設定ファイルリロード用の特殊バトルページを追加します。
        ///  // (有効値: true, false / 既定値: false)
        ///  &quot;Enabled&quot;: tru [残りの文字列は切り詰められました]&quot;; に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ModSettings {
            get {
                return ResourceManager.GetString("ModSettings", resourceCulture);
            }
        }
    }
}
