using UnityEditor;

[CustomEditor(typeof(GroundCheck))]
public class GroundCheckEditor : Editor
{
    SerializedProperty _checkType;
    SerializedProperty _checkRadius;
    SerializedProperty  _dimensions;
    SerializedProperty _whatIsGround;

    private void OnEnable()
    {
        _checkType = serializedObject.FindProperty("_checkType");
        _checkRadius = serializedObject.FindProperty("_checkRadius");
        _dimensions = serializedObject.FindProperty("_dimensions");
        _whatIsGround = serializedObject.FindProperty("_whatIsGround");   
    }

    public override void OnInspectorGUI()
    {
        GroundCheck _groundCheck = target as GroundCheck;

        serializedObject.Update();

        EditorGUILayout.PropertyField(_checkType);

        if (_groundCheck._checkType == GroundCheck.CheckType.Circle)
        {
            EditorGUILayout.PropertyField(_checkRadius);
        }
        else if (_groundCheck._checkType == GroundCheck.CheckType.Box)
        {
            EditorGUILayout.PropertyField(_dimensions);
        }

        EditorGUILayout.Space(5);

        EditorGUILayout.PropertyField(_whatIsGround);

        serializedObject.ApplyModifiedProperties();
    }
}
