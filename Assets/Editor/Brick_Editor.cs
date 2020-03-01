using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BrickScript))]
[CanEditMultipleObjects]

public class Brick_Editor : Editor {

	/* La verdad es que la idea de hacer un custom inspector
	 * parecia bastante guay y útil pero al final ha acabado
	 * dando mas problemas de los que ha solucionado. */

	public SerializedProperty
		brickType_Prop,
		steelBroken_Prop,
		chestPwrUp_Prop,
		mimicPwrUp_Prop,
		corruption_Prop,
		brickHealth_Prop,
		nearbyBricks_Prop,
		connectedPortal_Prop,
		sprites_Prop,
		ballCloneUp_Prop,
		ballCloneRight_Prop,
		ballCloneLeft_Prop,
		newDirection_Prop,
		needsNewDirectionX_Prop,
		needsNewDirectionY_Prop,
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
        connectedPortal_Prop = serializedObject.FindProperty("m_connectedPortal");
		sprites_Prop = serializedObject.FindProperty("sprites");
		ballCloneUp_Prop = serializedObject.FindProperty("m_BallCloneUp");
		ballCloneRight_Prop = serializedObject.FindProperty("m_BallCloneRight");
		ballCloneLeft_Prop = serializedObject.FindProperty("m_BallCloneLeft");
		newDirection_Prop = serializedObject.FindProperty("newDirection");
		needsNewDirectionX_Prop = serializedObject.FindProperty("needsNewDirectionX");
		needsNewDirectionY_Prop = serializedObject.FindProperty("needsNewDirectionY");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update(); 
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Select what brick type", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(brickType_Prop);

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Needed Parameters:", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(ballCloneLeft_Prop);
        EditorGUILayout.PropertyField(ballCloneRight_Prop);
        EditorGUILayout.PropertyField(ballCloneUp_Prop);

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
				EditorGUILayout.PropertyField(sprites_Prop);
                EditorGUILayout.PropertyField(chestPwrUp_Prop);
                EditorGUILayout.PropertyField(steelBroken_Prop);
                EditorGUILayout.PropertyField(sandSpeed_Prop);
                break;

			case BrickScript.Brick_Type.chest:
				EditorGUILayout.PropertyField(chestPwrUp_Prop);
				break;

            case BrickScript.Brick_Type.corrupted:
                EditorGUILayout.PropertyField(nearbyBricks_Prop);
                break;

            case BrickScript.Brick_Type.dimensional:
                EditorGUILayout.PropertyField(connectedPortal_Prop, new GUIContent("MyLabel"), true);
                EditorGUILayout.PropertyField(newDirection_Prop);
                EditorGUILayout.PropertyField(needsNewDirectionX_Prop);
                EditorGUILayout.PropertyField(needsNewDirectionY_Prop);
                break;


        }
        EditorGUILayout.PropertyField(brickHealth_Prop);
        EditorGUILayout.PropertyField(corruption_Prop);
        


        serializedObject.ApplyModifiedProperties();
	}
}
