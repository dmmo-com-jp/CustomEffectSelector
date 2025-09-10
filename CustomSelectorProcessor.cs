using NCalc;
using Vortice.Direct2D1;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Player.Video;
using YukkuriMovieMaker.Plugin.Effects;


namespace CustomSelector
{
    internal class CustomSelectorProcessor : IVideoEffectProcessor
    {
        readonly IGraphicsDevicesAndContext devices;
        readonly CustomSelector item;
        ID2D1Image? input;
        ID2D1Image? output;
        public List<IVideoEffectProcessor> videoEffectProcessors = new List<IVideoEffectProcessor>();
        public System.Collections.Immutable.ImmutableList<IVideoEffect> childeffects;
        public ID2D1Image Output => output ?? input ?? throw new NullReferenceException("No valid output image");
        public CustomSelectorProcessor(CustomSelector item, IGraphicsDevicesAndContext devices)
        {

            this.item = item;
            this.devices = devices;
            for (int i = 0; i < item.Effects.Count; i++)
            {
                videoEffectProcessors.Add(item.Effects[i].CreateVideoEffect(devices));

            }
            childeffects = item.Effects;
        }
        private object doclac(string clactxt,EffectDescription effectDescription ,Dictionary<string,object?>Paramlist)
        {
            var frame = effectDescription.ItemPosition.Frame;
            var length = effectDescription.ItemDuration.Frame;
            var fps = effectDescription.FPS;
            var layer = effectDescription.Layer;
            var inputIndex = effectDescription.InputIndex;
            var inputCount = effectDescription.InputCount;
            var groupIndex = effectDescription.GroupIndex;
            var groupCount = effectDescription.GroupCount;
            var drawDesc = effectDescription.DrawDescription;
            try
            {
                var cfreme = new Expression(clactxt);
                cfreme.Parameters = Paramlist;
                cfreme.Parameters["frame"] = frame;
                cfreme.Parameters["length"] = length;
                cfreme.Parameters["fps"] = fps;
                cfreme.Parameters["layer"] = layer;
                cfreme.Parameters["InputIndex"] = inputIndex;
                cfreme.Parameters["InputCount"] = inputCount;
                cfreme.Parameters["GroupIndex"] = groupIndex;
                cfreme.Parameters["GroupCount"] = groupCount;

                cfreme.Parameters["X"] = drawDesc.Draw.X;
                cfreme.Parameters["Y"] = drawDesc.Draw.Y;
                cfreme.Parameters["Z"] = drawDesc.Draw.Z;
                cfreme.Parameters["ZoomX"] = drawDesc.Zoom.X;
                cfreme.Parameters["ZoomY"] = drawDesc.Zoom.Y;
                cfreme.Parameters["RotationX"] = drawDesc.Rotation.X;
                cfreme.Parameters["RotationY"] = drawDesc.Rotation.Y;
                cfreme.Parameters["RotationZ"] = drawDesc.Rotation.Z;
                cfreme.Parameters["Opacity"] = drawDesc.Opacity;

                cfreme.Parameters["Value0"] = item.value0.GetValue(frame, length, fps);
                cfreme.Parameters["Value1"] = item.value1.GetValue(frame, length, fps);
                cfreme.Parameters["Value2"] = item.value2.GetValue(frame, length, fps);
                cfreme.Parameters["Value3"] = item.value3.GetValue(frame, length, fps);
                cfreme.Parameters["Value4"] = item.value4.GetValue(frame, length, fps);
                cfreme.Parameters["Value5"] = item.value5.GetValue(frame, length, fps);
                cfreme.Parameters["Value6"] = item.value6.GetValue(frame, length, fps);
                cfreme.Parameters["Value7"] = item.value7.GetValue(frame, length, fps);
                cfreme.Parameters["Value8"] = item.value8.GetValue(frame, length, fps);
                cfreme.Parameters["Value9"] = item.value9.GetValue(frame, length, fps);
                var res = cfreme.Evaluate();
                return res;
            }
            catch (Exception ex)
            {
                // その他の予期せぬエラーも捕捉する
                Console.WriteLine($"予期せぬエラー: {ex.Message}");
            }
            return 0;
        }
        /// <summary>
        /// 数値の入ったobject型をint型に変換するやつ
        /// 表現不可能は0になる
        /// </summary>
        /// <param name="value"></param>
        private int Convertint(object value) 
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        private float Convertfloat(object value)
        {
            try
            {
                return Convert.ToSingle(value);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public DrawDescription Update(EffectDescription effectDescription)
        {
            //こいつ入れないと子エフェクトの追加削除等の反映が遅れる。
            if(!childeffects.SequenceEqual(item.Effects))
            {
                videoEffectProcessors = [];
                for (int i = 0; i < item.Effects.Count; i++)
                {
                    videoEffectProcessors.Add(item.Effects[i].CreateVideoEffect(devices));

                }
                childeffects = item.Effects;
            }
            var frame = effectDescription.ItemPosition.Frame;
            var length = effectDescription.ItemDuration.Frame;
            var fps = effectDescription.FPS;
            var Params = item.VarList.Replace(Environment.NewLine, "").Split(',');
            var paramlist = new Dictionary<string, object?>();
            for(int i=0;i<Params.Length;i++)
            {
                var param = Params[i].Replace("==","::").Replace("!=", "!:").Replace("<=", "<:").Replace(">=", ">:").Split("=");
                try
                {
                    paramlist[param[0]] = doclac(param[1].Replace(":","="), effectDescription, paramlist);
                }
                catch(Exception e) { }
            }
            int rframe = Convertint(doclac(item.frameformula, effectDescription, paramlist));
            int rlength = Convertint(doclac(item.lenghtformula, effectDescription, paramlist));
            int rlayer = Convertint(doclac(item.layerformula, effectDescription, paramlist));
            int rinputindex = Convertint(doclac(item.inputindexformula, effectDescription, paramlist));
            int rinputcount = Convertint(doclac(item.inputCountformula, effectDescription, paramlist));
            int rgroupindex = Convertint(doclac(item.groupindexformula, effectDescription, paramlist));
            int rgroupcount = Convertint(doclac(item.groupCountformula, effectDescription, paramlist));

            object CanDoEffect = doclac(item.booleanClac, effectDescription, paramlist);
            Console.WriteLine(CanDoEffect.GetType());
            output = input;
            if (CanDoEffect is Boolean && (bool)CanDoEffect == false) return effectDescription.DrawDescription;
            var tmpdraw = effectDescription.DrawDescription;
            for(int i = 0; i < videoEffectProcessors.Count; i++)
            {
                var Timelinedesc = new TimelineItemSourceDescription(effectDescription, rframe,rlength, rlayer);
                var effectdesc = new EffectDescription(Timelinedesc, tmpdraw, rinputindex, rinputcount, rgroupindex, rgroupcount);
                videoEffectProcessors[i].SetInput(output);
                tmpdraw =videoEffectProcessors[i].Update(effectdesc);
                output = videoEffectProcessors[i].Output;
            }
            {
                var effectdesc = new EffectDescription(effectDescription, tmpdraw, rinputindex, rinputcount, rgroupindex, rgroupcount);
                tmpdraw = indraw(paramlist, effectdesc);
            }
            return tmpdraw;
        }
        private DrawDescription indraw(Dictionary<string,object?> paramlist,EffectDescription effectDescription) 
        {
            return effectDescription.DrawDescription with
            {
                Draw = new(
                    Convertfloat(doclac(item.Xformula, effectDescription, paramlist)),
                    Convertfloat(doclac(item.Yformula, effectDescription, paramlist)),
                    Convertfloat(doclac(item.Zformula, effectDescription, paramlist))
                    ),
                Rotation = new(
                    Convertfloat(doclac(item.RotationXformula, effectDescription, paramlist)),
                    Convertfloat(doclac(item.RotationYformula, effectDescription, paramlist)),
                    Convertfloat(doclac(item.RotationZformula, effectDescription, paramlist))
                    ),
                Zoom = new(
                    Convertfloat(doclac(item.ZoomXformula, effectDescription, paramlist)),
                    Convertfloat(doclac(item.ZoomYformula, effectDescription, paramlist))
                    ),

            };
        }
        public void ClearInput()
        {
            for (int i = 0; i < videoEffectProcessors.Count; i++) { videoEffectProcessors[i].ClearInput(); }
            input = null;
        }
        public void SetInput(ID2D1Image? input)
        {
            this.input = input;
        }
        public void Dispose() {
            for(int i = 0;i < videoEffectProcessors.Count;i++) { videoEffectProcessors[i].Dispose(); }
        }
    }
}
