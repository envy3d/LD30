using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomPropertyDrawer(typeof(Good))]
class GoodDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect valueRect = new Rect(position.x, position.y, 40, position.height);
        Rect typeRect = new Rect(position.x + 45, position.y, 50, position.height);
        Rect imageRect = new Rect(position.x + 100, position.y, 45, position.height);
        Rect nameRect = new Rect(position.x + 150, position.y, position.width - 150, position.height);

        EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("value"), GUIContent.none);
        EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("type"), GUIContent.none);
        EditorGUI.PropertyField(imageRect, property.FindPropertyRelative("image"), GUIContent.none);
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}