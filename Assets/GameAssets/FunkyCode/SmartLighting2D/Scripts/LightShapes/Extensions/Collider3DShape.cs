using System.Collections.Generic;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Misc;
using GameAssets.FunkyCode.SmartUtilities2D.Scripts.Triangulation;
using GameAssets.FunkyCode.SmartUtilities2D.Scripts.Utilities._2.Polygon2;
using UnityEngine;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.LightShapes.Extensions
{
	public class Collider3DShape : Base
	{
		public bool edgeCollider2D = false;
				
		public override List<MeshObject> GetMeshes() {
			if (Meshes == null) {
				List<Polygon2> polygons = GetPolygonsLocal();

				if (polygons == null) {
					return(null);
				}

				if (polygons.Count > 0) {
					Meshes = new List<MeshObject>();
					
					foreach(Polygon2 poly in polygons) {
						if (poly.points.Length < 3) {
							continue;
						}
						
						Mesh mesh = PolygonTriangulator2.Triangulate (poly, Vector2.zero, Vector2.zero, PolygonTriangulator2.Triangulation.Advanced);
						
						if (mesh) {							
							MeshObject meshObject = MeshObject.Get(mesh);

							if (meshObject != null) {
								Meshes.Add(meshObject);
							}
						}
					}
				}
			}
			return(Meshes);
		}

		public override List<Polygon2> GetPolygonsLocal()  {
			if (LocalPolygons != null) {
				return(LocalPolygons);
			}

			if (transform == null) {
				return(LocalPolygons);
			}

			LocalPolygons = Polygon2ListCollider3D.CreateFromGameObject(transform.gameObject);

			return(LocalPolygons);
		}

		public override List<Polygon2> GetPolygonsWorld() {
			if (WorldPolygons != null) {
				return(WorldPolygons);
			}

			WorldPolygons = new List<Polygon2>();

			if (GetPolygonsLocal() != null) {
				foreach(Polygon2 poly in GetPolygonsLocal()) {
					WorldPolygons.Add(poly.ToWorldSpace(transform));
				}
			}
			
			return(WorldPolygons);
		}
	}
}