using FluentAssertions;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebIDL.Exceptions;
using Microsoft.JSInterop;

namespace IntegrationTests.AudioNodeTests;

public class AnalyserNodeTest : AudioNodeWithAudioNodeOptions<AnalyserNode, AnalyserOptions>
{
    public override Task<AnalyserNode> CreateAsync(IJSRuntime jSRuntime, AudioContext context, AnalyserOptions? options)
        => AnalyserNode.CreateAsync(JSRuntime, AudioContext, options);

    [Test]
    public async Task GetFloatFrequencyDataAsync_ShouldPopulateBuffer()
    {
        // Arrange
        await using OscillatorNode oscillator = await OscillatorNode.CreateAsync(JSRuntime, AudioContext);
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext);
        await oscillator.ConnectAsync(node);
        await oscillator.StartAsync();

        int bufferLength = (int)await node.GetFrequencyBinCountAsync();
        await using Float32Array buffer = await Float32Array.CreateAsync(JSRuntime, bufferLength);

        // Act
        await Task.Delay(100);
        await node.GetFloatFrequencyDataAsync(buffer);

        // Assert
        float lastElement = await buffer.AtAsync(bufferLength - 1);
        _ = lastElement.Should().NotBe(0);
    }

    [Test]
    public async Task GetByteFrequencyDataAsync_ShouldPopulateBuffer()
    {
        // Arrange
        await using OscillatorNode oscillator = await OscillatorNode.CreateAsync(JSRuntime, AudioContext);
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext);
        await oscillator.ConnectAsync(node);
        await oscillator.StartAsync();

        int bufferLength = (int)await node.GetFrequencyBinCountAsync();
        await using Uint8Array buffer = await Uint8Array.CreateAsync(JSRuntime, bufferLength);

        // Act
        await node.GetByteFrequencyDataAsync(buffer);

        // Assert
        byte tenthElement = await buffer.AtAsync(10);
        _ = tenthElement.Should().NotBe(0);
    }

    [Test]
    public async Task GetFloatTimeDomainDataAsync_ShouldPopulateBuffer()
    {
        // Arrange
        await using OscillatorNode oscillator = await OscillatorNode.CreateAsync(JSRuntime, AudioContext);
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext);
        await oscillator.ConnectAsync(node);
        await oscillator.StartAsync();

        int bufferLength = (int)await node.GetFftSizeAsync();
        await using Float32Array buffer = await Float32Array.CreateAsync(JSRuntime, bufferLength);

        // Act
        await node.GetFloatTimeDomainDataAsync(buffer);

        // Assert
        float lastElement = await buffer.AtAsync(bufferLength - 1);
        _ = lastElement.Should().NotBe(0);
    }

    [Test]
    public async Task GetByteTimeDomainDataAsync_ShouldPopulateBuffer()
    {
        // Arrange
        await using OscillatorNode oscillator = await OscillatorNode.CreateAsync(JSRuntime, AudioContext);
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext);
        await oscillator.ConnectAsync(node);
        await oscillator.StartAsync();

        int bufferLength = (int)await node.GetFftSizeAsync();
        await using Uint8Array buffer = await Uint8Array.CreateAsync(JSRuntime, bufferLength);

        // Act
        await Task.Delay(100);
        await node.GetByteTimeDomainDataAsync(buffer);

        // Assert
        byte tenthElement = await buffer.AtAsync(bufferLength - 1);
        _ = tenthElement.Should().NotBe(0);
    }

    [Test]
    [TestCase(32ul)]
    [TestCase(64ul)]
    public async Task GetFftSizeAsync_ShouldRetrieveFftSize(ulong fftSize)
    {
        // Arrange
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            FftSize = fftSize
        });

        // Act
        ulong readFftSize = await node.GetFftSizeAsync();

        // Assert
        _ = readFftSize.Should().Be(fftSize);
    }

    [Test]
    [TestCase(32ul)]
    [TestCase(64ul)]
    public async Task SetFftSizeAsync_ShouldUpdateFftSize(ulong fftSize)
    {
        // Arrange
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext);

        // Act
        await node.SetFftSizeAsync(fftSize);

        // Assert
        ulong readFftSize = await node.GetFftSizeAsync();
        _ = readFftSize.Should().Be(fftSize);
    }

    [Test]
    [TestCase(16ul)]
    [TestCase(65536ul)]
    public async Task SetFftSizeAsync_ThrowsIndexSizeErrorException_WhenFftSizeIsOutOfBounds(ulong fftSize)
    {
        // Arrange
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext);

        // Act
        Func<Task> action = async () => await node.SetFftSizeAsync(fftSize);

        // Assert
        _ = await action.Should().ThrowAsync<IndexSizeErrorException>();
    }

    [Test]
    [TestCase(33ul)]
    [TestCase(32767ul)]
    public async Task SetFftSizeAsync_ThrowsIndexSizeErrorException_WhenFftSizeIsNotAPowerOfTwo(ulong fftSize)
    {
        // Arrange
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext);

        // Act
        Func<Task> action = async () => await node.SetFftSizeAsync(fftSize);

        // Assert
        _ = await action.Should().ThrowAsync<IndexSizeErrorException>();
    }

    [Test]
    [TestCase(32ul)]
    [TestCase(64ul)]
    public async Task GetFrequencyBinCountAsync_ShouldRetrieveFrequncyBinCount(ulong fftSize)
    {
        // Arrange
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            FftSize = fftSize
        });

        // Act
        ulong readBinCount = await node.GetFrequencyBinCountAsync();

        // Assert
        _ = readBinCount.Should().Be(fftSize / 2);
    }

    [Test]
    [TestCase(-30)]
    [TestCase(0)]
    public async Task GetMaxDecibelsAsync_ShouldRetrieveMaxDecibels(double maxDecibels)
    {
        // Arrange
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            MaxDecibels = maxDecibels
        });

        // Act
        double readMaxDecibels = await node.GetMaxDecibelsAsync();

        // Assert
        _ = readMaxDecibels.Should().Be(maxDecibels);
    }

    [Test]
    [TestCase(-30)]
    [TestCase(0)]
    public async Task SetMaxDecibelsAsync_ShouldUpdateMaxDecibels(double maxDecibels)
    {
        // Arrange
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext);

        // Act
        await node.SetMaxDecibelsAsync(maxDecibels);

        // Assert
        double readMaxDecibels = await node.GetMaxDecibelsAsync();
        _ = readMaxDecibels.Should().Be(maxDecibels);
    }

    [Test]
    [TestCase(-100, -110)]
    [TestCase(50, 0)]
    public async Task SetMaxDecibelsAsync_ThrowsIndexSizeErrorException_WhenSetLowerThanMinDecibels(double minDecibels, double maxDecibels)
    {
        // Arrange
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            MinDecibels = minDecibels,
            MaxDecibels = minDecibels + 1,
        });

        // Act
        Func<Task> action = async () => await node.SetMaxDecibelsAsync(maxDecibels);

        // Assert
        _ = await action.Should().ThrowAsync<IndexSizeErrorException>();
    }

    [Test]
    [TestCase(-100)]
    [TestCase(-50)]
    public async Task GetMinDecibelsAsync_ShouldRetrieveMinDecibels(double minDecibels)
    {
        // Arrange
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            MinDecibels = minDecibels
        });

        // Act
        double readMinDecibels = await node.GetMinDecibelsAsync();

        // Assert
        _ = readMinDecibels.Should().Be(minDecibels);
    }

    [Test]
    [TestCase(-100)]
    [TestCase(-50)]
    public async Task SetMinDecibelsAsync_ShouldUpdateMinDecibels(double minDecibels)
    {
        // Arrange
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext);

        // Act
        await node.SetMinDecibelsAsync(minDecibels);

        // Assert
        double readMinDecibels = await node.GetMinDecibelsAsync();
        _ = readMinDecibels.Should().Be(minDecibels);
    }

    [Test]
    [TestCase(-100, -110)]
    [TestCase(50, 0)]
    public async Task SetMinDecibelsAsync_ThrowsIndexSizeErrorException_WhenSetHigherThanMaxDecibels(double minDecibels, double maxDecibels)
    {
        // Arrange
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            MinDecibels = maxDecibels - 1,
            MaxDecibels = maxDecibels,
        });

        // Act
        Func<Task> action = async () => await node.SetMinDecibelsAsync(minDecibels);

        // Assert
        _ = await action.Should().ThrowAsync<IndexSizeErrorException>();
    }

    [Test]
    [TestCase(0.8)]
    [TestCase(0)]
    public async Task GetSmoothingTimeConstantAsync_ShouldRetrieveSmoothingTimeConstant(double smoothingTimeConstant)
    {
        // Arrange
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            SmoothingTimeConstant = smoothingTimeConstant
        });

        // Act
        double readSmoothingTimeConstant = await node.GetSmoothingTimeConstantAsync();

        // Assert
        _ = readSmoothingTimeConstant.Should().Be(smoothingTimeConstant);
    }

    [Test]
    [TestCase(0.8)]
    [TestCase(0)]
    public async Task SetSmoothingTimeConstantAsync_ShouldUpdateSmoothingTimeConstant(double smoothingTimeConstant)
    {
        // Arrange
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext);

        // Act
        await node.SetSmoothingTimeConstantAsync(smoothingTimeConstant);

        // Assert
        double readSmoothingTimeConstant = await node.GetSmoothingTimeConstantAsync();
        _ = readSmoothingTimeConstant.Should().Be(smoothingTimeConstant);
    }

    [Test]
    [TestCase(-0.2)]
    [TestCase(1.2)]
    public async Task SetMinDecibelsAsync_ThrowsIndexSizeErrorException_WhenSetToValueOutsideRange(double smoothingTimeConstant)
    {
        // Arrange
        await using AnalyserNode node = await AnalyserNode.CreateAsync(JSRuntime, AudioContext);

        // Act
        Func<Task> action = async () => await node.SetSmoothingTimeConstantAsync(smoothingTimeConstant);

        // Assert
        _ = await action.Should().ThrowAsync<IndexSizeErrorException>();
    }
}
