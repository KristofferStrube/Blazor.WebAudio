namespace IntegrationTests.AudioNodeTests;

public class StereoPannerNodeTest : AudioNodeTest<StereoPannerNode>
{
    public override async Task<StereoPannerNode> GetDefaultInstanceAsync()
    {
        return await StereoPannerNode.CreateAsync(EvaluationContext.JSRuntime, await EvaluationContext.GetAudioContext());
    }
}
