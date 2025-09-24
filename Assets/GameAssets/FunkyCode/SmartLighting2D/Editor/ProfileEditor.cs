using GameAssets.FunkyCode.SmartLighting2D.Scripts.Settings;
using UnityEditor;

namespace FunkyCode
{
	[CustomEditor(typeof(Profile))]
	public class ProfileEditor2 : Editor
	{
		override public void OnInspectorGUI()
		{
			Profile profile = target as Profile;

			ProfileEditor.DrawProfile(profile);
		}
	}
}