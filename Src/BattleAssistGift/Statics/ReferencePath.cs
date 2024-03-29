﻿using System.IO;
using BattleAssistGift.Refrection;

namespace BattleAssistGift.Statics
{
    /// <summary>
    /// 外部ファイルまたは外部フォルダーの参照パスを表します。
    /// </summary>
    public static class ReferencePath
    {
        /// <summary>MOD 設定ファイルのパス</summary>
        public static readonly string ModSettingsFile = Path.Combine(
            AssemblyInfo.DirectoryPath, @"ModSettings.json");
    }
}
