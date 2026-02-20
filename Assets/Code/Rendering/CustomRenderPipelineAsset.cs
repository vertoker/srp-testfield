using UnityEngine;
using UnityEngine.Rendering;

namespace Code.Rendering
{
    [CreateAssetMenu(fileName = nameof(CustomRenderPipelineAsset), 
        menuName = "Configs/Rendering/" + nameof(CustomRenderPipelineAsset))]
    public class CustomRenderPipelineAsset : RenderPipelineAsset<CustomRenderPipeline>
    {
        protected override RenderPipeline CreatePipeline()
        {
            return new CustomRenderPipeline(this);
        }
    }
}