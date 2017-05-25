﻿using Microsoft.Xna.Framework;
using STACK.Graphics;
using StarFinder;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace STACK.Components
{
    [Serializable]
    public class ScenePath : Component
    {
        public string PathFile = string.Empty;        

        Path _Path;                

        public Path Path
        {
            get
            {
                return _Path;
            }
            set
            {
                if (_Path == value)
                {
                    return;
                }
                
                _Path = value;
                NotifyParent(Messages.ScenePathChanged, value);                
            }
        }

        public override void OnDraw(Renderer renderer)
        {
            bool DrawPath = EngineVariables.DebugPath && Path != null && renderer.Stage != RenderStage.PostBloom;

            if (DrawPath)
            {
                Path.Draw(renderer.PrimitivesRenderer.DrawTriangle, renderer.PrimitivesRenderer.DrawLine, Scene.World.Get<Mouse>().Position);
            }
        }

        public override void OnLoadContent(ContentLoader content)
        {
            if (!string.IsNullOrEmpty(PathFile))
            {
                Path = content.Load<Path>(PathFile);				
            }            
        }

        void LoadPath(string file)
        {
            PathFile = file;
            if (Loaded)
            {
                OnLoadContent(((Scene)Parent).Content);
            }
        }

        public static ScenePath Create(Scene addTo)
        {
            return addTo.Add<ScenePath>();
        }

        public ScenePath SetPathFile(string value) { LoadPath(value); return this; }
        public ScenePath SetPath(Path value) { Path = value; return this; }
    }  
}
