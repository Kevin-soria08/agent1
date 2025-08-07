#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using MyGame.Presentation.Controllers;

namespace MyGame.Editor
{
    /// <summary>
    /// Custom inspector for <see cref="PlayerController"/> with a reset stats button.
    /// </summary>
    [CustomEditor(typeof(PlayerController))]
    public class PlayerControllerEditor : UnityEditor.Editor
    {
        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var controller = (PlayerController)target;
            if (GUILayout.Button("Reset Stats"))
            {
                Undo.RecordObject(controller, "Reset Player Stats");
                controller.ResetStats();
                EditorUtility.SetDirty(controller);
            }
        }
    }
}
#endif
