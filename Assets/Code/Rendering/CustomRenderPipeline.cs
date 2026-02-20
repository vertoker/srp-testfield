using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;

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
            foreach (var camera in cameras)
            {
                context.SetupCameraProperties(camera);
                
                // 2. Culling objects
                if (!camera.TryGetCullingParameters(out var cullingParameters)) continue;
                
                var cullingResults = context.Cull(ref cullingParameters);
            
                // 3. Drawing objects
                
                var cmd = new CommandBuffer();
                
                cmd.ClearRenderTarget(true, true, Color.black);
                
                cmd.BeginSample("Render Skybox");
                var skyboxRendererList = context.CreateSkyboxRendererList(camera);
                cmd.DrawRendererList(skyboxRendererList);
                cmd.EndSample("Render Skybox");
                
                cmd.BeginSample("Render Objects");
                var shaderPassName = new ShaderTagId("CustomLightModeTag");
                var desc = new RendererListDesc(shaderPassName, cullingResults, camera);
                var rendererList = context.CreateRendererList(desc);
                cmd.DrawRendererList(rendererList);
                cmd.EndSample("Render Objects");
                
                context.ExecuteCommandBuffer(cmd);
                cmd.Dispose();
            }
            
            context.Submit();
        }
    }
}