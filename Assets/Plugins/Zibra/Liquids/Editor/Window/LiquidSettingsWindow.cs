#if UNITY_2019_4_OR_NEWER
using com.zibra.liquid.Plugins.Editor;
using UnityEngine;
using UnityEngine.UIElements;

namespace com.zibra.liquid
{
    internal class LiquidSettingsWindow : PackageSettingsWindow<LiquidSettingsWindow>
    {
        internal override IPackageInfo GetPackageInfo() => new ZibraAiPackageInfo();

        protected override void OnWindowEnable(VisualElement root)
        {
            // AddTab("Settings", new SettingsTab());
            AddTab("Info", new AboutTab());
        }

        public static GUIContent WindowTitle => new GUIContent(ZibraAIPackage.DisplayName);
    }
}
#endif