using BaseMod;
using BattleAssistGift.Models;
using BattleAssistGift.Resources;

namespace BattleAssistGift
{
    /// <summary>
    /// 戦闘表象「月光の祝福」の機能を実装します。
    /// </summary>
    public sealed class MoonlightBlessing
    {
        /// <summary>MOD 設定データにアクセスするリポジトリ オブジェクト</summary>
        private readonly IModSettingsRepository repository;

        /// <summary>
        /// <see cref="MoonlightBlessing"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="repository"></param>
        public MoonlightBlessing(IModSettingsRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// 敵味方全ての生存キャラクターに効果を適用します。
        /// </summary>
        public void ApplyEffectAll()
        {
            repository.Reload();
            
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList())
            {
                var buf = target.bufListDetail.FindBuf<BattleUnitBuf_MoonlightBlessing>();
                if (buf == null)
                {
                    buf = new BattleUnitBuf_MoonlightBlessing();
                    target.bufListDetail.AddBuf(buf);
                }

                EffectModel effect = repository.GetEffect(target);
                buf.Apply(effect);
            }
        }

        /// <summary>
        /// 味方全ての生存キャラクターに個人特殊ページ「設定リロード」を追加します。
        /// </summary>
        public void AddReloadCardAllPlayers()
        {
            foreach (BattleUnitModel target in BattleObjectManager.instance.GetAliveList(Faction.Player))
            {
                AddReloadCard(target);
            }
        }

        /// <summary>
        /// 指定したキャラクターに個人特殊ページ「設定リロード」を追加します。
        /// </summary>
        /// <param name="target"></param>
        public void AddReloadCard(BattleUnitModel target)
        {
            target.personalEgoDetail.RemoveCard(CardId.Reload);
            target.personalEgoDetail.AddCard(CardId.Reload);
        }
    }
}
