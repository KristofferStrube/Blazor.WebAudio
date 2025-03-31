using FluentAssertions;
using FluentAssertions.Execution;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class BiquadFilterNodeTest : AudioNodeWithAudioNodeOptions<BiquadFilterNode, BiquadFilterOptions>
{
    public override async Task<BiquadFilterNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, BiquadFilterOptions? options)
        => await BiquadFilterNode.CreateAsync(JSRuntime, AudioContext, options);

    [Test]
    [TestCase(BiquadFilterType.Lowpass)]
    [TestCase(BiquadFilterType.Highpass)]
    [TestCase(BiquadFilterType.Bandpass)]
    [TestCase(BiquadFilterType.Lowshelf)]
    [TestCase(BiquadFilterType.Highshelf)]
    [TestCase(BiquadFilterType.Peaking)]
    [TestCase(BiquadFilterType.Notch)]
    [TestCase(BiquadFilterType.Allpass)]
    public async Task GetTypeAsync_ShouldRetrieveType(BiquadFilterType type)
    {
        // Arrange
        await using BiquadFilterNode node = await BiquadFilterNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Type = type
        });

        // Act
        BiquadFilterType readType = await node.GetTypeAsync();

        // Assert
        _ = readType.Should().Be(type);
    }

    [Test]
    [TestCase(BiquadFilterType.Lowpass)]
    [TestCase(BiquadFilterType.Highpass)]
    [TestCase(BiquadFilterType.Bandpass)]
    [TestCase(BiquadFilterType.Lowshelf)]
    [TestCase(BiquadFilterType.Highshelf)]
    [TestCase(BiquadFilterType.Peaking)]
    [TestCase(BiquadFilterType.Notch)]
    [TestCase(BiquadFilterType.Allpass)]
    public async Task SetTypeAsync_ShouldUpdateType(BiquadFilterType type)
    {
        // Arrange
        await using BiquadFilterNode node = await BiquadFilterNode.CreateAsync(JSRuntime, AudioContext);

        // Act
        await node.SetTypeAsync(type);

        // Assert
        BiquadFilterType readType = await node.GetTypeAsync();
        _ = readType.Should().Be(type);
    }

    [Test]
    [TestCase(350)]
    [TestCase(0.5f)]
    public async Task GetFrequencyAsync_ShouldRetrieveFrequencyParameter(float frequency)
    {
        // Arrange
        await using BiquadFilterNode node = await BiquadFilterNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Frequency = frequency
        });

        // Act
        await using AudioParam frequencyParameter = await node.GetFrequencyAsync();

        // Assert
        float readFrequency = await frequencyParameter.GetValueAsync();
        _ = readFrequency.Should().Be(frequency);
        
        await frequencyParameter.SetValueAsync(frequency + 1);
        readFrequency = await frequencyParameter.GetValueAsync();
        _ = readFrequency.Should().Be(frequency + 1);
    }

    [Test]
    [TestCase(0)]
    [TestCase(100)]
    public async Task GetDetuneAsync_ShouldRetrieveDetuneParameter(float detune)
    {
        // Arrange
        await using BiquadFilterNode node = await BiquadFilterNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Detune = detune
        });

        // Act
        await using AudioParam detuneParameter = await node.GetDetuneAsync();

        // Assert
        float readDetune = await detuneParameter.GetValueAsync();
        _ = readDetune.Should().Be(detune);

        await detuneParameter.SetValueAsync(detune + 1);
        readDetune = await detuneParameter.GetValueAsync();
        _ = readDetune.Should().Be(detune + 1);
    }

    [Test]
    [TestCase(1)]
    [TestCase(24)]
    public async Task GetQAsync_ShouldRetrieveQParameter(float q)
    {
        // Arrange
        await using BiquadFilterNode node = await BiquadFilterNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Q = q
        });

        // Act
        await using AudioParam qParameter = await node.GetQAsync();

        // Assert
        float readQ = await qParameter.GetValueAsync();
        _ = readQ.Should().Be(q);

        await qParameter.SetValueAsync(q + 1);
        readQ = await qParameter.GetValueAsync();
        _ = readQ.Should().Be(q + 1);
    }

    [Test]
    [TestCase(0)]
    [TestCase(2)]
    public async Task GetGainAsync_ShouldRetrieveGainParameter(float gain)
    {
        // Arrange
        await using BiquadFilterNode node = await BiquadFilterNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Gain = gain
        });

        // Act
        await using AudioParam gainParameter = await node.GetGainAsync();

        // Assert
        float readGain = await gainParameter.GetValueAsync();
        _ = readGain.Should().Be(gain);

        await gainParameter.SetValueAsync(gain + 1);
        readGain = await gainParameter.GetValueAsync();
        _ = readGain.Should().Be(gain + 1);
    }

    [Test]
    public async Task GetFrequencyResponseAsync_ShouldFillResponseArrays_WhenParametersAreAllSameLength()
    {
        // Arrange
        await using BiquadFilterNode node = await BiquadFilterNode.CreateAsync(JSRuntime, AudioContext);

        await using Float32Array frequencyHz = await Float32Array.CreateAsync(JSRuntime, 3);
        await frequencyHz.FillAsync(300, 0, 1);
        await frequencyHz.FillAsync(400, 1, 2);
        await frequencyHz.FillAsync(500, 2, 3);
        await using Float32Array magResponse = await Float32Array.CreateAsync(JSRuntime, 3);
        await using Float32Array phaseResponse = await Float32Array.CreateAsync(JSRuntime, 3);

        // Act
        await node.GetFrequencyResponseAsync(frequencyHz, magResponse, phaseResponse);

        // Assert
        using AssertionScope __ = new();

        float magForFrequency300 = await magResponse.AtAsync(0);
        _ = magForFrequency300.Should().BeGreaterThan(1);

        float magForFrequency400 = await magResponse.AtAsync(1);
        _ = magForFrequency400.Should().BeLessThan(1);

        float magForFrequency500 = await magResponse.AtAsync(2);
        _ = magForFrequency500.Should().BeLessThan(magForFrequency400);

        float phaseForFrequency300 = await phaseResponse.AtAsync(0);
        _ = phaseForFrequency300.Should().BeGreaterThan(-MathF.PI).And.BeLessThan(MathF.PI);

        float phaseForFrequency400 = await phaseResponse.AtAsync(1);
        _ = phaseForFrequency400.Should().BeGreaterThan(-MathF.PI).And.BeLessThan(MathF.PI);

        float phaseForFrequency500 = await phaseResponse.AtAsync(2);
        _ = phaseForFrequency500.Should().BeGreaterThan(-MathF.PI).And.BeLessThan(MathF.PI);
    }
}
