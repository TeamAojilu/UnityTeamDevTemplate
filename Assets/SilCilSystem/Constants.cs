namespace SilCilSystem
{
    public static class Constants
    {
#if UNITY_EDITOR
        internal const string AddComponentPath = "SilCil/";

        // MenuItem
        internal const string CreateAssetMenuPath = "Assets/Create/SilCilSystem/";
        internal const string CreateVariableMenuPath = CreateAssetMenuPath + "Variables/";
        internal const string CreateGameEventMenuPath = CreateAssetMenuPath + "Events/";

        internal const string CreateGameObjectMenuPath = "GameObject/SilCilSystem/";
        internal const int CreateGameObjectMenuOrder = 0;

        // SilCilSystem Menu
        internal const string SilCilSystemMenuPath = "SilCilSystem/";
        internal const string DragDropSettingMenuPath = SilCilSystemMenuPath + "DragDrop/";

        // Add SubAssets
        internal const string ReadonlyMenuPath = "Readonly/";
        internal const string ListenerMenuPath = "Listener/";
        internal const string ConvertMenuPath = "Converter/";

        // GUIDで指定. フォルダ構成をいじられても動くように.
        internal const string VariableInspectorOrdersID = "632d6d46c658303489412669cd0c2a10";

        // Views
        internal const string CanvasTemplateID = "af0452c25383d8e44bef73d34d73bf00";
        internal const string EventSystemTemplateID = "1c18688ed5dc55b498134b8a9cfac836";
        internal const string TextTemplateID = "d9fbbac3448a71d448976595aa336392";
        internal const string TextTMPTemplateID = "9fe7ce90c3a2e2549a08857cc20c3d48";
        internal const string SliderTemplateID = "d6a2f0abf3a2fd840926ea55daafd5d5";
        internal const string ToggleTemplateID = "c347fd82a7661c74bbbab21a9592daf0";
#endif

        public const int SingletonExecutionOrder = -1000;
    }
}