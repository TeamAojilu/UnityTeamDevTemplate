using SilCilSystem.Variables.Base;

namespace SilCilSystem.Editors
{
    public abstract class VariableDragDropAction
    {
        public abstract bool IsAccepted(VariableAsset dropAsset);
        public abstract void OnDropExited(VariableAsset dropAsset);
    }
}