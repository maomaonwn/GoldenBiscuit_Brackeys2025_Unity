using System.Collections;
using System.Collections.Generic;
using GameAssets.FunkyCode.SmartLighting2D.Components.Lightmap;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Settings;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace FunkyCode
	{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(LightTexture2D))]
	public class LightTexture2DEditor : Editor
	{
		override public void OnInspectorGUI()
		{
			LightTexture2D script = target as LightTexture2D;

			script.lightLayer = EditorGUILayout.Popup("Layer (Light)", script.lightLayer, Lighting2D.Profile.layers.lightLayers.GetNames());

			script.shaderMode = (LightTexture2D.ShaderMode)EditorGUILayout.EnumPopup("Shader Mode", script.shaderMode);

			script.color = EditorGUILayout.ColorField("Color", script.color);

			script.size = EditorGUILayout.Vector2Field("Size", script.size);

			script.texture = (Texture)EditorGUILayout.ObjectField("Texture", script.texture, typeof(Texture), true);

			if (GUI.changed)
			{	
				if (!EditorApplication.isPlaying)
				{
					EditorUtility.SetDirty(target);
					EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
				}
			}
		}
	}
}	