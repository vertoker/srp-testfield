using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;
using UnityEngine.Rendering.RenderGraphModule;

namespace Code.Rendering
{
    public class CustomCameraRenderer
    {
        public void Render(RenderGraph renderGraph, ScriptableRenderContext context, Camera camera)
        {
            context.SetupCameraProperties(camera);
            
            if (!camera.TryGetCullingParameters(out var cullingParameters)) return;
            var cullingResults = context.Cull(ref cullingParameters);
            
            var cmd = new CommandBuffer();
            cmd.name = "RenderCameraPass";
            
            // 1. Clear ScreenRenderGraph _renderGraph
            cmd.ClearRenderTarget(true, true, Color.clear);
            
            cmd.BeginSample("Render Skybox");
            var skyboxRendererList = context.CreateSkyboxRendererList(camera);
            cmd.DrawRendererList(skyboxRendererList);
            cmd.EndSample("Render Skybox");
            
            cmd.BeginSample("Render Objects");
            
            var sortingSettings = new SortingSettings(camera);
        
            var shaderPassName = new ShaderTagId("SRPDefaultUnlit"); // Default name for non-programmable SRP shaders
            var drawingSettings = new DrawingSettings(shaderPassName, sortingSettings);
            
            var desc = new RendererListDesc(shaderPassName, cullingResults, camera);
            var rendererList = context.CreateRendererList(desc);
            cmd.DrawRendererList(rendererList);
            
            cmd.EndSample("Render Objects");
            context.ExecuteCommandBuffer(cmd);
            
            cmd.Release();
        }
    }
}