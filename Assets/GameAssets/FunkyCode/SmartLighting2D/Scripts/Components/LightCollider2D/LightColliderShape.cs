using System.Collections.Generic;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.LightShapes;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.LightShapes.Extensions;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Misc;
using GameAssets.FunkyCode.SmartUtilities2D.Scripts.Utilities._2.Polygon2;
using UnityEngine;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Components.LightCollider2D
{
	[System.Serializable]
	public class LightColliderShape
	{
		public SmartLighting2D.Components.LightCollider.LightCollider2D.ShadowType shadowType = SmartLighting2D.Components.LightCollider.LightCollider2D.ShadowType.SpritePhysicsShape;
		public SmartLighting2D.Components.LightCollider.LightCollider2D.MaskType maskType = SmartLighting2D.Components.LightCollider.LightCollider2D.MaskType.Sprite;
		public SmartLighting2D.Components.LightCollider.LightCollider2D.MaskPivot maskPivot = SmartLighting2D.Components.LightCollider.LightCollider2D.MaskPivot.TransformCenter;

		public LightColliderTransform transform2D = new LightColliderTransform();
		public Transform transform;

		public Collider2DShape collider2DShape = new Collider2DShape();
		public CompositeCollider2DShape compositeShape = new CompositeCollider2DShape();

		public SpriteShape spriteShape = new SpriteShape();
		public SpritePhysicsShape spritePhysicsShape = new SpritePhysicsShape();
		
		public MeshRendererShape meshShape = new MeshRendererShape();
		public SkinnedMeshRendererShape skinnedMeshShape = new SkinnedMeshRendererShape();

		public Collider3DShape collider3DShape = new Collider3DShape();

		public Base GetShadowShape()
		{
			switch(shadowType)
			{
				case SmartLighting2D.Components.LightCollider.LightCollider2D.ShadowType.SpritePhysicsShape:
					return(spritePhysicsShape);

				case SmartLighting2D.Components.LightCollider.LightCollider2D.ShadowType.Collider2D:
					return(collider2DShape);

				case SmartLighting2D.Components.LightCollider.LightCollider2D.ShadowType.Collider3D:
					return(collider3DShape);

				case SmartLighting2D.Components.LightCollider.LightCollider2D.ShadowType.CompositeCollider2D:
					return(compositeShape);

				case SmartLighting2D.Components.LightCollider.LightCollider2D.ShadowType.MeshRenderer:
					return(meshShape);

				case SmartLighting2D.Components.LightCollider.LightCollider2D.ShadowType.SkinnedMeshRenderer:
					return(skinnedMeshShape);
			}

			return(null);
		}

		public Base GetMaskShape()
		{
			switch(maskType) {
				case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskType.Sprite:
					return(spriteShape);

				case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskType.BumpedSprite:
					return(spriteShape);

				case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskType.SpritePhysicsShape:
					return(spritePhysicsShape);

				case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskType.CompositeCollider2D:
					return(compositeShape);

				case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskType.Collider2D:
					return(collider2DShape);
					
				case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskType.Collider3D:
					return(collider3DShape);

				case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskType.MeshRenderer:
					return(meshShape);

				case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskType.BumpedMeshRenderer:
					return(meshShape);

				case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskType.SkinnedMeshRenderer:
					return(skinnedMeshShape);
			}

			return(null);
		}
		
		public void SetTransform(SmartLighting2D.Components.LightCollider.LightCollider2D lightCollider2D)
		{
			transform = lightCollider2D.transform;

			transform2D.SetShape(this, lightCollider2D);

			spriteShape.SetTransform(transform);
			spritePhysicsShape.SetTransform(transform);

			collider2DShape.SetTransform(transform);
			compositeShape.SetTransform(transform);

			meshShape.SetTransform(transform);
			skinnedMeshShape.SetTransform(transform);

			collider3DShape.SetTransform(transform);
		}

		public void ResetLocal()
		{
			Base shadowShape = GetShadowShape();

			if (shadowShape != null) {
				shadowShape.ResetLocal();
				shadowShape.ResetWorld();
			}

			Base maskShape = GetMaskShape();

			if (maskShape != null)
			{
				maskShape.ResetLocal();
				maskShape.ResetWorld();
			}
		}

		public void ResetWorld()
		{
			Base shadowShape = GetShadowShape();

			if (shadowShape != null)
			{
				shadowShape.ResetWorld();
			}

			Base maskShape = GetMaskShape();

			if (maskShape != null)
			{
				maskShape.ResetWorld();
			}
		}

		public bool RectOverlap(Rect rect)
		{
			Base shadowShape = GetShadowShape();
			Base maskShape = GetMaskShape();

			if (shadowShape != null)
			{
				bool result = shadowShape.GetWorldRect().Overlaps(rect);

				if (result)
				{
					return(true);
				}
			}

			if (maskShape != null)
			{
				bool result = maskShape.GetWorldRect().Overlaps(rect);

				if (result)
				{
					return(true);
				}
			}

			return(false);
		}

		public Rect GetWorldRect()
		{
			Base shadowShape = GetShadowShape();

			if (shadowShape != null)
			{
				return(shadowShape.GetWorldRect());
			}

			Base maskShape = GetMaskShape();

			if (maskShape != null)
			{
				return(maskShape.GetWorldRect());
			}

			return(new Rect());
		}

		public int GetSortingOrder()
		{
			Base shadowShape = GetShadowShape();

			if (shadowShape != null)
			{
				int shadowOrder = shadowShape.GetSortingOrder();

				if (shadowOrder != 0)
				{
					return(shadowOrder);
				}
			}

			Base maskShape = GetMaskShape();

			if (maskShape != null)
			{
				int maskOrder = maskShape.GetSortingOrder();

				if (maskOrder != 0)
				{
					return(maskOrder);
				}
			}

			return(0);
		}

		public int GetSortingLayer()
		{
			Base shadowShape = GetShadowShape();

			if (shadowShape != null)
			{
				int shadowLayer = shadowShape.GetSortingLayer();

				if (shadowLayer != 0)
				{
					return(shadowLayer);
				}
			}

			Base maskShape = GetMaskShape();

			if (maskShape != null)
			{
				int maskLayer = maskShape.GetSortingLayer();

				if (maskLayer != 0)
				{
					return(maskLayer);
				}
			}

			return(0);
		}

		public Rect GetIsoWorldRect()
		{
			Base shadowShape = GetShadowShape();

			if (shadowShape != null)
			{
				return(shadowShape.GetIsoWorldRect());
			}

			Base maskShape = GetMaskShape();

			if (maskShape != null)
			{
				return(maskShape.GetIsoWorldRect());
			}

			return(new Rect());
		}

		public List<MeshObject> GetMeshes()
		{
			Base maskShape = GetMaskShape();

			if (maskShape != null)
			{
				return(maskShape.GetMeshes());
			}

			return(null);
		}

		public List<Polygon2> GetPolygonsLocal()
		{
			Base shadowShape = GetShadowShape();

			if (shadowShape != null)
			{
				return(shadowShape.GetPolygonsLocal());
			}

			return(null);
		}

		public List<Polygon2> GetPolygonsWorld()
		{
			Base shadowShape = GetShadowShape();

			if (shadowShape != null)
			{
				return(shadowShape.GetPolygonsWorld());
			}

			return(null);
		}

		public Vector2 GetPivotPoint()
		{
			Base shadowShape = GetShadowShape();

			if (shadowShape != null)
			{
				switch(maskPivot)
				{
					case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskPivot.TransformCenter:
						return(shadowShape.GetPivotPoint_TransformCenter());

					case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskPivot.ShapeCenter:
						return(shadowShape.GetPivotPoint_ShapeCenter());

					case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskPivot.LowestY:
						return(shadowShape.GetPivotPoint_LowestY());
				}
			}

			Base maskShape = GetMaskShape();

			if (maskShape != null) 
			{
				switch(maskPivot)
				{
					case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskPivot.TransformCenter:
						return(maskShape.GetPivotPoint_TransformCenter());

					case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskPivot.ShapeCenter:
						return(maskShape.GetPivotPoint_ShapeCenter());

					case SmartLighting2D.Components.LightCollider.LightCollider2D.MaskPivot.LowestY:
						return(maskShape.GetPivotPoint_LowestY());
				}
			}

			return(Vector2.zero);
		}

		public bool IsEdgeCollider()
		{
			switch(shadowType) {
				case SmartLighting2D.Components.LightCollider.LightCollider2D.ShadowType.Collider2D:
					return(collider2DShape.edgeCollider2D);
			}
			
			return(false);
		}
	}
}