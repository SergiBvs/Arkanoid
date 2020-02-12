using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BrickScript))]
[CanEditMultipleObjects]

public class Brick_Editor : Editor {

	public SerializedProperty
		brickType_Prop,
		steelBroken_Prop,
		sandSpeed_Prop;

	void OnEnable()
	{
		brickType_Prop = serializedObject.FindProperty("brickType");
		sandSpeed_Prop = serializedObject.FindProperty("sandSpeed");
		steelBroken_Prop = serializedObject.FindProperty("steelBrokenSprite");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update(); 
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Select what brick type", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(brickType_Prop);

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Needed Parameters:", EditorStyles.boldLabel);


		BrickScript.Brick_Type bT = (BrickScript.Brick_Type)brickType_Prop.enumValueIndex;

		switch (bT)
		{
			case BrickScript.Brick_Type.normal:				
				EditorGUILayout.LabelField("Normal Brick doesn't need any parameters for now.");
				break;

			case BrickScript.Brick_Type.steel:
				EditorGUILayout.PropertyField(steelBroken_Prop);
				break;

			case BrickScript.Brick_Type.desert:
				EditorGUILayout.PropertyField(sandSpeed_Prop);
				break;
		}

		serializedObject.ApplyModifiedProperties();
	}
}
