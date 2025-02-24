namespace IntegrationTests.AudioNodeTests;

public class PannerNodeTest : AudioNodeTest<PannerNode>
{
    public override async Task<PannerNode> GetDefaultInstanceAsync()
    {
        return await PannerNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext());
    }
}
