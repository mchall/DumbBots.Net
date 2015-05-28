using System;
using Core;
using Entities;

namespace Game
{
    /// <summary>
    /// This class is responsible for handling the rendering of the scene. 
    /// </summary>
    internal static class RenderManager
    {
        internal static void BeginRender()
        {
            Globals.Driver.BeginScene(true, true, IrrlichtNETCP.Color.TransparentWhite);
            Globals.Scene.DrawAll();
            DebugRenderer.DrawBoundingBoxes();
        }

        internal static void EndRender()
        {
            Globals.Driver.EndScene();
        }
    }
}