using GameAssets.FunkyCode.SmartLighting2D.Components.Lightmap;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Universal.Objects;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Settings;
using UnityEngine;
using Sprite = GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Universal.Objects.Sprite;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Lightmap.Objects
{
    public static class Room
    {

        public static void Draw(LightRoom2D id, UnityEngine.Camera camera)
        {
            switch(id.shape.type)
            {
                case LightRoom2D.RoomType.Collider:
                    DrawCollider(id, camera);
                break;

                case LightRoom2D.RoomType.Sprite:
                    DrawSprite(id, camera);
                break;
            }

            var material = Lighting2D.Materials.mask.GetMask();
            material.mainTexture = null;
        }

        public static bool drawColliderPass = false;

        static public void DrawColliderPass(LightRoom2D id, UnityEngine.Camera camera)
        {
            var meshObjects = id.shape.GetMeshes();
            if (meshObjects == null)
                return;
            
            if (!drawColliderPass)
            {
                drawColliderPass = true;

                var material = Lighting2D.Materials.room.GetRoomMask();
                material.mainTexture = null;
                material.SetPass(0);

                GLExtended.color = id.color;
                
                GL.Begin(GL.TRIANGLES);
            }

            Vector2 position = id.transform.position - camera.transform.position;
            GLExtended.DrawMeshPass(meshObjects, position, id.transform.lossyScale, id.transform.rotation.eulerAngles.z);
        }

        static public void DrawCollider(LightRoom2D id, UnityEngine.Camera camera)
        {
            var meshObjects = id.shape.GetMeshes();
            if (meshObjects == null)
                return;

            var material = Lighting2D.Materials.room.GetRoomMask();
            material.mainTexture = null;
            material.SetPass(0);

            GLExtended.color = id.color;
            Vector2 position = id.transform.position - camera.transform.position;
            GLExtended.DrawMesh(meshObjects, position, id.transform.lossyScale, id.transform.rotation.eulerAngles.z);
        }

        static public void DrawSprite(LightRoom2D id, UnityEngine.Camera camera)
        {
            var spriteRenderer = id.shape.spriteShape.GetSpriteRenderer();      
            if (spriteRenderer == null)
                return;

            var sprite = spriteRenderer.sprite;
            if (sprite == null)
                return;

            var material = Lighting2D.Materials.room.GetRoomMask();
            material.mainTexture = sprite.texture;
            material.SetPass(0);

            GLExtended.color = id.color;
            Vector2 position = id.transform.position - camera.transform.position;
            Sprite.FullRect.Draw(id.spriteMeshObject, spriteRenderer, position, id.transform.lossyScale, id.transform.eulerAngles.z);	
        }
    }
}