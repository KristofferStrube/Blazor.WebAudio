﻿using FluentAssertions;
using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebIDL.Exceptions;

namespace IntegrationTests.AudioNodeTests;

public class AudioBufferSourceNodeTest : AudioNodeTest<AudioBufferSourceNode>
{
    public override async Task<AudioBufferSourceNode> GetDefaultInstanceAsync()
    {
        return await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext);
    }

    [Test]
    public async Task GetBufferAsync_ShouldReturnNull_WhenItHasNoBuffer()
    {
        // Arrange
        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext);

        // Act
        AudioBuffer? buffer = await node.GetBufferAsync();

        // Assert
        _ = buffer.Should().BeNull();
    }

    [Test]
    public async Task GetBufferAsync_ShouldRetrieveBuffer_WhenItHasBuffer()
    {
        // Arrange
        await using AudioBuffer buffer = await AudioBuffer.CreateAsync(JSRuntime, new AudioBufferOptions() { Length = 1, SampleRate = 8000 });

        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Buffer = buffer
        });

        // Act
        AudioBuffer? readBuffer = await node.GetBufferAsync();

        // Assert
        _ = readBuffer.Should().NotBeNull();
    }

    [Test]
    public async Task SetBufferAsync_ShouldUpdateBuffer_WhenItHasNotBeenSetAlready()
    {
        // Arrange
        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext);
        await using AudioBuffer buffer = await AudioBuffer.CreateAsync(JSRuntime, new AudioBufferOptions() { Length = 1, SampleRate = 8000 });

        // Act
        await node.SetBufferAsync(buffer);

        // Assert
        AudioBuffer? readBuffer = await node.GetBufferAsync();
        _ = readBuffer.Should().NotBeNull();
    }

    [Test]
    public async Task SetBufferAsync_WithNullArgument_ShouldUpdateBuffer_WhenItHasAlreadyBeenSet()
    {
        // Arrange
        await using AudioBuffer buffer = await AudioBuffer.CreateAsync(JSRuntime, new AudioBufferOptions() { Length = 1, SampleRate = 8000 });
        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Buffer = buffer
        });

        // Act
        await node.SetBufferAsync(null);

        // Assert
        AudioBuffer? readBuffer = await node.GetBufferAsync();
        _ = readBuffer.Should().BeNull();
    }

    [Test]
    [TestCase(1)]
    [TestCase(-2)]
    public async Task GetPlaybackRateAsync_RetrievesPlaybackRate(float playbackRate)
    {
        // Arrange
        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            PlaybackRate = playbackRate
        });

        // Act
        await using AudioParam detuneParameter = await node.GetPlaybackRateAsync();
        float readPlaybackRate = await detuneParameter.GetValueAsync();

        // Assert
        _ = readPlaybackRate.Should().Be(playbackRate);
    }

    [Test]
    [TestCase(1)]
    [TestCase(-2)]
    public async Task GetDetuneAsync_RetrievesDetune(float detune)
    {
        // Arrange
        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Detune = detune
        });

        // Act
        await using AudioParam detuneParameter = await node.GetDetuneAsync();
        float readDetune = await detuneParameter.GetValueAsync();

        // Assert
        _ = readDetune.Should().Be(detune);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task GetLoopAsync_RetrievesLoop(bool loop)
    {
        // Arrange
        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Loop = loop
        });

        // Act
        bool readLoop = await node.GetLoopAsync();

        // Assert
        _ = readLoop.Should().Be(loop);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public async Task SetLoopAsync_UpdatesLoop(bool loop)
    {
        // Arrange
        await using AudioContext context = await AudioContext.CreateAsync(JSRuntime);

        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Loop = !loop
        });
        bool readLoopBeforeSetting = await node.GetLoopAsync();

        // Act
        await node.SetLoopAsync(loop);

        // Assert
        bool readLoopAfterSetting = await node.GetLoopAsync();
        _ = readLoopBeforeSetting.Should().Be(!loop);
        _ = readLoopAfterSetting.Should().Be(loop);
    }

    [Test]
    [TestCase(100)]
    [TestCase(-3)]
    public async Task GetLoopStartAsync_RetrievesLoopStart(double loopStart)
    {
        // Arrange
        await using AudioContext context = await AudioContext.CreateAsync(JSRuntime);

        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            LoopStart = loopStart
        });

        // Act
        double readLoopStart = await node.GetLoopStartAsync();

        // Assert
        _ = readLoopStart.Should().Be(loopStart);
    }

    [Test]
    [TestCase(100)]
    [TestCase(-3)]
    public async Task SetLoopStartAsync_UpdatesLoopStart(double loopStart)
    {
        // Arrange
        await using AudioContext context = await AudioContext.CreateAsync(JSRuntime);

        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext);

        // Act
        await node.SetLoopStartAsync(loopStart);

        // Assert
        double readLoopStart = await node.GetLoopStartAsync();
        _ = readLoopStart.Should().Be(loopStart);
    }

    [Test]
    [TestCase(100)]
    [TestCase(-3)]
    public async Task GetLoopEndAsync_RetrievesLoopEnd(double loopEnd)
    {
        // Arrange
        await using AudioContext context = await AudioContext.CreateAsync(JSRuntime);

        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            LoopEnd = loopEnd
        });

        // Act
        double readLoopEnd = await node.GetLoopEndAsync();

        // Assert
        _ = readLoopEnd.Should().Be(loopEnd);
    }

    [Test]
    [TestCase(100)]
    [TestCase(-3)]
    public async Task SetLoopEndAsync_UpdatesLoopEnd(double loopEnd)
    {
        // Arrange
        await using AudioContext context = await AudioContext.CreateAsync(JSRuntime);

        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext);

        // Act
        await node.SetLoopEndAsync(loopEnd);

        // Assert
        double readLoopEnd = await node.GetLoopEndAsync();
        _ = readLoopEnd.Should().Be(loopEnd);
    }

    [Test]
    public async Task StartAsync_WithNoArguments_StartsNodeImmediately()
    {
        // Arrange
        await using AudioContext context = await AudioContext.CreateAsync(JSRuntime);

        await using AudioBuffer buffer = await AudioBuffer.CreateAsync(JSRuntime, new AudioBufferOptions()
        {
            SampleRate = 8000,
            Length = 1
        });
        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Buffer = buffer
        });

        bool eventListenerTriggered = false;
        await using EventListener<Event> onEndedListener = await EventListener<Event>.CreateAsync(JSRuntime, e => eventListenerTriggered = true);
        await node.AddOnEndedEventListenerAsync(onEndedListener);

        // Act
        await node.StartAsync();

        // Assert
        await Task.Delay(100);
        _ = eventListenerTriggered.Should().BeTrue();
        await node.RemoveOnEndedEventListenerAsync(onEndedListener);
    }

    [Test]
    [TestCase(0.2)]
    [TestCase(0.4)]
    public async Task StartAsync_WithWhenArgument_StartsNodeThen(double whenOffset)
    {
        // Arrange
        await using AudioContext context = await AudioContext.CreateAsync(JSRuntime);

        await using AudioBuffer buffer = await AudioBuffer.CreateAsync(JSRuntime, new AudioBufferOptions()
        {
            SampleRate = 8000,
            Length = 1
        });
        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Buffer = buffer
        });

        bool eventListenerTriggered = false;
        await using EventListener<Event> onEndedListener = await EventListener<Event>.CreateAsync(JSRuntime, e => eventListenerTriggered = true);
        await node.AddOnEndedEventListenerAsync(onEndedListener);

        // Act
        double time = await context.GetCurrentTimeAsync();
        await node.StartAsync(when: time + whenOffset);

        // Assert
        await Task.Delay(TimeSpan.FromSeconds(whenOffset - 0.1));
        _ = eventListenerTriggered.Should().BeFalse();
        await Task.Delay(TimeSpan.FromSeconds(0.2));
        _ = eventListenerTriggered.Should().BeTrue();
        await node.RemoveOnEndedEventListenerAsync(onEndedListener);
    }

    [Test]
    [TestCase(0.2)]
    [TestCase(0.7)]
    [Ignore("as the start method doesn't seem to respect the offset parameter")]
    public async Task StartAsync_WithOffsetParameter_StartsOffsetInBuffer(double offset)
    {
        // Arrange
        await using AudioContext context = await AudioContext.CreateAsync(JSRuntime);

        await using AudioBuffer buffer = await AudioBuffer.CreateAsync(JSRuntime, new AudioBufferOptions()
        {
            SampleRate = 8000,
            Length = 8000 * 1,
        });

        var a = await buffer.GetDurationAsync();

        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Buffer = buffer
        });

        bool eventListenerTriggered = false;
        await using EventListener<Event> onEndedListener = await EventListener<Event>.CreateAsync(JSRuntime, e => eventListenerTriggered = true);
        await node.AddOnEndedEventListenerAsync(onEndedListener);

        // Act
        await node.StartAsync(when: 0, offset: offset);

        // Assert
        await Task.Delay(TimeSpan.FromSeconds(1 - offset - 0.1));
        _ = eventListenerTriggered.Should().BeFalse();
        await Task.Delay(TimeSpan.FromSeconds(0.2));
        _ = eventListenerTriggered.Should().BeTrue();
        await node.RemoveOnEndedEventListenerAsync(onEndedListener);
    }

    [Test]
    public async Task StartAsync_ThrowsInvalidStateErrorException_WhenItHasAlreadyBeenStarted()
    {
        // Arrange
        await using AudioContext context = await AudioContext.CreateAsync(JSRuntime);

        await using AudioBuffer buffer = await AudioBuffer.CreateAsync(JSRuntime, new AudioBufferOptions()
        {
            SampleRate = 8000,
            Length = 1
        });
        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Buffer = buffer
        });
        await node.StartAsync();

        // Act
        Func<Task> action = async () => await node.StartAsync();

        // Assert
        _ = await action.Should().ThrowAsync<InvalidStateErrorException>();
    }

    [Test]
    public async Task StartAsync_WithNegativeOffset_ThrowsRangeErrorException()
    {
        // Arrange
        await using AudioContext context = await AudioContext.CreateAsync(JSRuntime);

        await using AudioBuffer buffer = await AudioBuffer.CreateAsync(JSRuntime, new AudioBufferOptions()
        {
            SampleRate = 8000,
            Length = 1
        });
        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Buffer = buffer
        });

        // Act
        Func<Task> action = async () => await node.StartAsync(when: 0, offset: -1);

        // Assert
        _ = await action.Should().ThrowAsync<RangeErrorException>();
    }

    [Test]
    public async Task StartAsync_WithNegativeDuration_ThrowsRangeErrorException()
    {
        // Arrange
        await using AudioContext context = await AudioContext.CreateAsync(JSRuntime);

        await using AudioBuffer buffer = await AudioBuffer.CreateAsync(JSRuntime, new AudioBufferOptions()
        {
            SampleRate = 8000,
            Length = 1
        });
        await using AudioBufferSourceNode node = await AudioBufferSourceNode.CreateAsync(JSRuntime, AudioContext, new()
        {
            Buffer = buffer
        });

        // Act
        Func<Task> action = async () => await node.StartAsync(when: 0, offset: 0, duration: -1);

        // Assert
        _ = await action.Should().ThrowAsync<RangeErrorException>();
    }
}
