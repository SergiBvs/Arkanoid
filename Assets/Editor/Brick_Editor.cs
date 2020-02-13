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
		chestPwrUp_Prop,
		mimicPwrUp_Prop,
        corruption_Prop,
        brickHealth_Prop,
        nearbyBricks_Prop,
		sandSpeed_Prop;

	void OnEnable()
	{
		brickType_Prop = serializedObject.FindProperty("brickType");
		sandSpeed_Prop = serializedObject.FindProperty("sandSpeed");
		chestPwrUp_Prop = serializedObject.FindProperty("ChestPowerUps");
		mimicPwrUp_Prop = serializedObject.FindProperty("MimicPowerUps");
		steelBroken_Prop = serializedObject.FindProperty("m_BrokenBrick");
        corruption_Prop = serializedObject.FindProperty("m_CorruptionObject");
        brickHealth_Prop = serializedObject.FindProperty("m_brickHealth");
        nearbyBricks_Prop = serializedObject.FindProperty("m_NearbyBricks");
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

			case BrickScript.Brick_Type.future:
				EditorGUILayout.LabelField("Future Brick doesn't need any parameters for now.");
				break;

			case BrickScript.Brick_Type.chest:
				EditorGUILayout.PropertyField(chestPwrUp_Prop);
				break;
			
			case BrickScript.Brick_Type.mimic:
				EditorGUILayout.PropertyField(mimicPwrUp_Prop);
				break;

            case BrickScript.Brick_Type.corrupted:
                EditorGUILayout.PropertyField(nearbyBricks_Prop);
                break;


        }
        EditorGUILayout.PropertyField(brickHealth_Prop);
        EditorGUILayout.PropertyField(corruption_Prop);


        serializedObject.ApplyModifiedProperties();
	}
}
