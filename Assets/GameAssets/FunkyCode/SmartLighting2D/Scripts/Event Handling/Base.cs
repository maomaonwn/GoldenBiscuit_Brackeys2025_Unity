using GameAssets.FunkyCode.SmartUtilities2D.Scripts.Utilities._2.Polygon2;
using UnityEngine;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Event_Handling
{
	public class Base
	{
		public static Vector2 edgeLeft, edgeRight;
		public static Vector2 projectionLeft, projectionRight;
		public static Polygon2 eventPoly = null;

		static public Polygon2 GetPolygon()
		{
			if (eventPoly == null) {
				eventPoly = new Polygon2(4);
			}
			
			return(eventPoly);
		}
	}
}