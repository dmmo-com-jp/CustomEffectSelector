using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Controls;
using YukkuriMovieMaker.Exo;
using YukkuriMovieMaker.Player.Video;
using YukkuriMovieMaker.Plugin;
using YukkuriMovieMaker.Plugin.Effects;

namespace CustomSelector
{
    [VideoEffect("カスタムエフェクトセレクター", ["アニメーション"], [],true,false)]
    internal class CustomSelector : VideoEffectBase
    {
        public override string Label => "カスタムエフェクトセレクター";
        [Display(GroupName = "子映像エフェクト", Name = "", Description = "")]
        [VideoEffectSelector(PropertyEditorSize = PropertyEditorSize.FullWidth)]
        public ImmutableList<IVideoEffect> Effects { get => effects; set => Set(ref effects, value); }
        ImmutableList<IVideoEffect> effects = [];

        [Display(GroupName = "引数一覧", Name = "Value0", Description = "引数0")]
        [AnimationSlider("F0", "", -100, 100)]
        public Animation value0 { get; } = new Animation(0, -10000, 10000);

        [Display(GroupName = "引数一覧", Name = "Value1", Description = "引数1")]
        [AnimationSlider("F0", "", -100, 100)]
        public Animation value1 { get; } = new Animation(0, -10000, 10000);

        [Display(GroupName = "引数一覧", Name = "Value2", Description = "引数2")]
        [AnimationSlider("F0", "", -100, 100)]
        public Animation value2 { get; } = new Animation(0, -10000, 10000);

        [Display(GroupName = "引数一覧", Name = "Value3", Description = "引数3")]
        [AnimationSlider("F0", "", -100, 100)]
        public Animation value3 { get; } = new Animation(0, -10000, 10000);

        [Display(GroupName = "引数一覧", Name = "Value4", Description = "引数4")]
        [AnimationSlider("F0", "", -100, 100)]
        public Animation value4 { get; } = new Animation(0, -10000, 10000);

        [Display(GroupName = "引数一覧", Name = "Value5", Description = "引数5")]
        [AnimationSlider("F0", "", -100, 100)]
        public Animation value5 { get; } = new Animation(0, -10000, 10000);

        [Display(GroupName = "引数一覧", Name = "Value6", Description = "引数6")]
        [AnimationSlider("F0", "", -100, 100)]
        public Animation value6 { get; } = new Animation(0, -10000, 10000);

        [Display(GroupName = "引数一覧", Name = "Value7", Description = "引数7")]
        [AnimationSlider("F0", "", -100, 100)]
        public Animation value7 { get; } = new Animation(0, -10000, 10000);

        [Display(GroupName = "引数一覧", Name = "Value8", Description = "引数8")]
        [AnimationSlider("F0", "", -100, 100)]
        public Animation value8 { get; } = new Animation(0, -10000, 10000);

        [Display(GroupName = "引数一覧", Name = "Value9", Description = "引数9")]
        [AnimationSlider("F0", "", -100, 100)]
        public Animation value9 { get; } = new Animation(0, -10000, 10000);


        [Display(GroupName = "EffectDescription", Name = "Frame", Description = "Frameの式")]
        [TextEditor(AcceptsReturn = true)]
        public string frameformula { get => text; set => Set(ref text, value); }
        string text = "frame";

        [Display(GroupName = "EffectDescription", Name = "Length", Description = "Lengthの式")]
        [TextEditor(AcceptsReturn = true)]
        public string lenghtformula { get => text2; set => Set(ref text2, value); }
        string text2= "length";

        [Display(GroupName = "EffectDescription", Name = "Layer", Description = "Layerの式")]
        [TextEditor(AcceptsReturn = true)]
        public string layerformula { get => text3; set => Set(ref text3, value); }
        string text3 = "layer";

        [Display(GroupName = "EffectDescription", Name = "InputIndex", Description = "何文字目の式")]
        [TextEditor(AcceptsReturn = true)]
        public string inputindexformula { get => text4; set => Set(ref text4, value); }
        string text4 = "InputIndex";

        [Display(GroupName = "EffectDescription", Name = "InputCount", Description = "文字数の式")]
        [TextEditor(AcceptsReturn = true)]
        public string inputCountformula { get => text5; set => Set(ref text5, value); }
        string text5 = "InputCount";

        [Display(GroupName = "EffectDescription", Name = "GroupIndex", Description = "グループ制御の何番目かの式")]
        [TextEditor(AcceptsReturn = true)]
        public string groupindexformula { get => text6; set => Set(ref text6, value); }
        string text6 = "GroupIndex";

        [Display(GroupName = "EffectDescription", Name = "GroupCount", Description = "グループ制御の大きさの式")]
        [TextEditor(AcceptsReturn = true)]
        public string groupCountformula { get => text7; set => Set(ref text7, value); }
        string text7 = "GroupCount";


        [Display(GroupName = "DrawDescription", Name = "X座標", Description = "X座標の式")]
        [TextEditor(AcceptsReturn = true)]
        public string Xformula { get => tx1; set => Set(ref tx1, value); }
        string tx1 = "X";

        [Display(GroupName = "DrawDescription", Name = "Y座標", Description = "Y座標の式")]
        [TextEditor(AcceptsReturn = true)]
        public string Yformula { get => tx2; set => Set(ref tx2, value); }
        string tx2 = "Y";

        [Display(GroupName = "DrawDescription", Name = "Z座標", Description = "Z座標の式")]
        [TextEditor(AcceptsReturn = true)]
        public string Zformula { get => tx3; set => Set(ref tx3, value); }
        string tx3 = "Z";

        [Display(GroupName = "DrawDescription", Name = "ZoomX", Description = "ZoomXの式")]
        [TextEditor(AcceptsReturn = true)]
        public string ZoomXformula { get => tx4; set => Set(ref tx4, value); }
        string tx4 = "ZoomX";

        [Display(GroupName = "DrawDescription", Name = "ZoomY", Description = "ZoomYの式")]
        [TextEditor(AcceptsReturn = true)]
        public string ZoomYformula { get => tx5; set => Set(ref tx5, value); }
        string tx5 = "ZoomY";
        //Rotation
        [Display(GroupName = "DrawDescription", Name = "X回転", Description = "X回転の式")]
        [TextEditor(AcceptsReturn = true)]
        public string RotationXformula { get => tx6; set => Set(ref tx6, value); }
        string tx6 = "RotationX";

        [Display(GroupName = "DrawDescription", Name = "Y回転", Description = "Y回転の式")]
        [TextEditor(AcceptsReturn = true)]
        public string RotationYformula { get => tx7; set => Set(ref tx7, value); }
        string tx7 = "RotationY";

        [Display(GroupName = "DrawDescription", Name = "Z回転", Description = "Z回転の式")]
        [TextEditor(AcceptsReturn = true)]
        public string RotationZformula { get => tx8; set => Set(ref tx8, value); }
        string tx8 = "RotationZ";


        [Display(GroupName = "変数設定", Name = "", Description = "")]
        [TextEditor(AcceptsReturn = true)]
        public string VarList { get => vars; set => Set(ref vars, value); }
        string vars = "";

        [Display(GroupName = "実行条件", Name = "", Description = "")]
        [TextEditor(AcceptsReturn = true)]
        public string booleanClac { get => boolclac; set => Set(ref boolclac, value); }
        string boolclac = "true";


        public override IEnumerable<string> CreateExoVideoFilters(int keyFrameIndex, ExoOutputDescription exoOutputDescription)
        {
            //サンプルはSampleD2DVideoEffectを参照
            return [];
        }

        /// <summary>
        /// 映像エフェクトを作成する
        /// </summary>
        /// <param name="devices">デバイス</param>
        /// <returns>映像エフェクト</returns>
        public override IVideoEffectProcessor CreateVideoEffect(IGraphicsDevicesAndContext devices)
        {
            return new CustomSelectorProcessor(this, devices);
        }

        /// <summary>
        /// クラス内のIAnimatableを列挙する。
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<IAnimatable> GetAnimatables() => [
            value0,value1,value2,value3,value4,value5,value6,value7,value8,value9,
            .. effects
            ];
        public PluginDetailsAttribute Details => new()
        {
            //制作者
            AuthorName = "メタロロ",
            //作品ID
            ContentId = "",
        };
    }
}
