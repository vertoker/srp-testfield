using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;
using UnityEngine.Rendering.RenderGraphModule;

namespace Code.Rendering
{
    public class CustomRenderPipeline : RenderPipeline
    {
        private readonly RenderGraph _renderGraph;
        private readonly CustomCameraRenderer _cameraRenderer;
        private readonly CustomRenderPipelineAsset _asset;

        public CustomRenderPipeline(CustomRenderPipelineAsset asset)
        {
            _renderGraph = new("Custom SRP Render Graph");
            _cameraRenderer = new CustomCameraRenderer();
            _asset = asset;
        }

        protected override void Render(ScriptableRenderContext context, List<Camera> cameras)
        {
            foreach (var camera in cameras)
            {
                if (!camera.isActiveAndEnabled) continue;
                
                _cameraRenderer.Render(_renderGraph, context, camera);
            }
            
            context.Submit();
            _renderGraph.EndFrame();
        }

        protected override void Dispose(bool disposing)
        {
            _renderGraph.Cleanup();
        }
    }
}