using com.zibra.liquid;

#if UNITY_2019_4_OR_NEWER
namespace com.zibra.liquid.Plugins.Editor
{
    internal class AboutTab : BaseTab
    {
        public AboutTab() : base($"{ZibraAIPackage.UIToolkitPath}/AboutTab/AboutTab")

        {
        }
    }
}
#endif