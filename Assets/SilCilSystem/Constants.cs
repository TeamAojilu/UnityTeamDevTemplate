namespace SilCilSystem
{
    public static class Constants
    {
#if UNITY_EDITOR
        internal const string AddComponentPath = "SilCil/";

        // MenuItem
        internal const string CreateAssetMenuPath = "Assets/Create/";
        internal const string CreateVariableMenuPath = CreateAssetMenuPath + "Variables/";
        internal const string CreateGameEventMenuPath = CreateAssetMenuPath + "Events/";
        
        // SilCilSystem Menu
        internal const string SilCilSystemMenuPath = "SilCilSystem/";
        internal const string DragDropSettingMenuPath = SilCilSystemMenuPath + "DragDrop/";

        // Add SubAssets
        internal const string ReadonlyMenuPath = "Readonly/";
        internal const string ListenerMenuPath = "Listener/";
        internal const string ConvertMenuPath = "Converter/";

        // メタファイルから読み取ったGUIDを使用.
        internal const string VariableInspectorOrdersID = "632d6d46c658303489412669cd0c2a10";
#endif

        public const int SingletonExecutionOrder = -1000;
    }
}