using System.Collections.Generic;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.LightShapes.Extensions;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Misc;
using GameAssets.FunkyCode.SmartUtilities2D.Scripts.Utilities._2.Polygon2;
using UnityEngine;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Components.DayLightCollider2D
{
	[System.Serializable]
	public class DayLightColliderShape
	{
		public SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.ShadowType shadowType = SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.ShadowType.SpritePhysicsShape;
		
		public SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.MaskType maskType = SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.MaskType.Sprite;

		public Transform transform;
	
		public DayLightingColliderTransform transform2D = new DayLightingColliderTransform();

		public SpriteShape spriteShape = new SpriteShape();
		public SpritePhysicsShape spritePhysicsShape = new SpritePhysicsShape();
		public Collider2DShape colliderShape = new Collider2DShape();

		public float height = 1;
		public float thickness = 1;

		public bool isStatic = false;

		public void SetTransform(Transform t)
		{
			transform = t;

			transform2D.SetShape(this);

			spriteShape.SetTransform(t);
			spritePhysicsShape.SetTransform(t);
			
			colliderShape.SetTransform(t);
		}

		public void ResetLocal()
		{
			spriteShape.ResetLocal();
			spritePhysicsShape.ResetLocal();

			colliderShape.ResetLocal();
		}

		public void ResetWorld()
		{
			spritePhysicsShape.ResetWorld();

			colliderShape.ResetWorld();
		}

		public List<MeshObject> GetMeshes()
		{
			switch(shadowType)
			{

				case SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.ShadowType.FillCollider2D:

					return(colliderShape.GetMeshes());

				case SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.ShadowType.FillSpritePhysicsShape:

					return(spritePhysicsShape.GetMeshes());
			}

			return(null);
		}

		public List<Polygon2> GetPolygonsLocal()
		{
			switch(shadowType)
			{
				case SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.ShadowType.SpritePhysicsShape:
				case SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.ShadowType.FillSpritePhysicsShape:

					return(spritePhysicsShape.GetPolygonsLocal());

				case SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.ShadowType.Collider2D:
				case SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.ShadowType.FillCollider2D:

					return(colliderShape.GetPolygonsLocal());
			}

			return(null);
		}

		public List<Polygon2> GetPolygonsWorld()
		{
			switch(shadowType)
			{
				case SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.ShadowType.SpritePhysicsShape:
				case SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.ShadowType.SpriteProjectionShape:
				case SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.ShadowType.FillSpritePhysicsShape:

					return(spritePhysicsShape.GetPolygonsWorld());
					
				case SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.ShadowType.Collider2D:
				case SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.ShadowType.SpriteProjectionCollider:
				case SmartLighting2D.Components.DayLightCollider.DayLightCollider2D.ShadowType.FillCollider2D:

					return(colliderShape.GetPolygonsWorld());
			}

			return(null);
		}

		public Rect GetShadowBounds()
		{
			List<Polygon2> polygons = GetPolygonsWorld();

			if (polygons != null)
			{
				return( Polygon2Helper.GetDayRect(polygons, height) );
			}

			return(new Rect());
		}
	}
}