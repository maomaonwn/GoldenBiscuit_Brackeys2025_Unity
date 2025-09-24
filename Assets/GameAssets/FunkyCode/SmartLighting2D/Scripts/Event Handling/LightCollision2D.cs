using System.Collections.Generic;
using GameAssets.FunkyCode.SmartLighting2D.Components.LightCollider;
using GameAssets.FunkyCode.SmartLighting2D.Components.Lightmap;
using UnityEngine;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Event_Handling
{
	public struct LightCollision2D
	{
		public enum State
		{
			OnCollision, 
			OnCollisionEnter, 
			OnCollisionExit
		}

		public Light2D light;
		public LightCollider2D collider;

		public List<Vector2> points;
		public State state;

		public LightCollision2D(State state)
		{
			this.light = null;

			this.collider = null;
			
			this.points = null;

			this.state = state;
		}
	}
}