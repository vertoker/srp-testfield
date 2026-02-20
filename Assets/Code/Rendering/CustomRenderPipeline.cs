using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Code.Rendering
{
    public class CustomRenderPipeline : RenderPipeline
    {
        private readonly CustomRenderPipelineAsset _asset;

        public CustomRenderPipeline(CustomRenderPipelineAsset asset)
        {
            _asset = asset;
        }

        protected override void Render(ScriptableRenderContext context, List<Camera> cameras)
        {
            // 1. Clear frame
            
            // 2. Culling objects
            
            // 3. Drawing objects
            
        }
    }
}