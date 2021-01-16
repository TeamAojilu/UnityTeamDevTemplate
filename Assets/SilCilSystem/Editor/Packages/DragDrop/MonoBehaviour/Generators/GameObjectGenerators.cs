using UnityEngine;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal static class GameObjectGenerators
    {
        [MonoBehaviourDragDrop("Empty")]
        private static GameObject CreateEmpty()
        {
            return RegisterUndo(new GameObject(), "Create Empty");
        }

        [MonoBehaviourDragDrop("Primitives/Sphere")]
        private static GameObject CreateSphere()
        {
            return RegisterUndo(GameObject.CreatePrimitive(PrimitiveType.Sphere), "Create Sphere");
        }

        [MonoBehaviourDragDrop("Primitives/Cube")]
        private static GameObject CreateCube()
        {
            return RegisterUndo(GameObject.CreatePrimitive(PrimitiveType.Cube), "Create Cube");
        }

        [MonoBehaviourDragDrop("Primitives/Cylinder")]
        private static GameObject CreateCylinder()
        {
            return RegisterUndo(GameObject.CreatePrimitive(PrimitiveType.Cylinder), "Create Cylinder");
        }

        [MonoBehaviourDragDrop("Primitives/Plane")]
        private static GameObject CreatePlane()
        {
            return RegisterUndo(GameObject.CreatePrimitive(PrimitiveType.Plane), "Create Plane");
        }

        [MonoBehaviourDragDrop("Primitives/Quad")]
        private static GameObject CreateQuad()
        {
            return RegisterUndo(GameObject.CreatePrimitive(PrimitiveType.Quad), "Create Quad");
        }

        [MonoBehaviourDragDrop("Primitives/Capsule")]
        private static GameObject CreateCapsule()
        {
            return RegisterUndo(GameObject.CreatePrimitive(PrimitiveType.Capsule), "Create Capsule");
        }

        private static GameObject RegisterUndo(GameObject obj, string name, string objectName = null)
        {
            obj.name = objectName;
            Undo.RegisterCreatedObjectUndo(obj, name);
            return obj;
        }
    }
}