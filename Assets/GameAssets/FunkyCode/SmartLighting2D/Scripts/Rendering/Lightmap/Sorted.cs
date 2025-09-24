using GameAssets.FunkyCode.SmartLighting2D.Components.Lightmap;
using GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Lightmap.Objects;

namespace GameAssets.FunkyCode.SmartLighting2D.Scripts.Rendering.Lightmap
{
    public class Sorted
    {    
        static public void Draw(Pass pass)
        {
            for(int id = 0; id < pass.sortList.Count; id++)
            {
                var sortObject = pass.sortList.List[id];
                object lightObject = sortObject.LightObject;

                if (sortObject.LightObject is LightTilemapRoom2D lightTilemapRoom)
                    TilemapRoom.Draw(lightTilemapRoom, pass.camera);    
                else if (sortObject.LightObject is LightRoom2D lightRoom)
                    Room.Draw(lightRoom, pass.camera);
                else if (sortObject.LightObject is LightSprite2D lightSprite)
                    LightSprite.Simple.Draw(lightSprite, pass.camera);
                else if (sortObject.LightObject is Light2D light)
                    LightSource.Draw(light, pass.camera);
            }
        }
    }
}