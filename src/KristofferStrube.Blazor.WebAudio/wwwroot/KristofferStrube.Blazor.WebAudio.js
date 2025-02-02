export function getAttribute(object, attribute) { return object[attribute]; }

export function setAttribute(object, attribute, value) { object[attribute] = value; }

export function toArrayBuffer(array) {
    return array.buffer;
}

export function constructAudioContext(contextOptions = null) {
    return new AudioContext(contextOptions)
}

export function constructOfflineAudioContext(contextOptions) {
    return new OfflineAudioContext(contextOptions)
}

export function constructOfflineAudioContextWithThreeParameters(numberOfChannels, length, sampleRate) {
    return new OfflineAudioContext(numberOfChannels, length, sampleRate)
}

export function constructOcillatorNode(context, options) {
    return new OscillatorNode(context, options);
}

export function constructGainNode(context, options) {
    return new GainNode(context, options);
}

export function constructAnalyzerNode(context, options) {
    return new AnalyserNode(context, options);
}

export function constructAudioBufferSourceNode(context, options) {
    return new AudioBufferSourceNode(context, options);
}

export function constructBiquadFilterNode(context, options) {
    return new BiquadFilterNode(context, options);
}

export function constructChannelMergerNode(context, options) {
    return new ChannelMergerNode(context, options);
}

export function constructChannelSplitterNode(context, options) {
    return new ChannelSplitterNode(context, options);
}

export function constructConstantSourceNode(context, options) {
    return new ConstantSourceNode(context, options);
}

export function constructDelayNode(context, options) {
    return new DelayNode(context, options);
}

export function constructConvolverNode(context, options) {
    return new ConvolverNode(context, options);
}

export function constructDynamicsCompressorNode(context, options) {
    return new DynamicsCompressorNode(context, options);
}

export function constructMediaStreamAudioSourceNode(context, options) {
    return new MediaStreamAudioSourceNode(context, options);
}

export function constructMediaStreamTrackAudioSourceNode(context, options) {
    return new MediaStreamTrackAudioSourceNode(context, options);
}

export function constructMediaStreamAudioDestinationNode(context, options) {
    return new MediaStreamAudioDestinationNode(context, options);
}

export function constructPannerNode(context, options) {
    return new PannerNode(context, options);
}

export function constructPeriodicWave(context, options) {
    return new PeriodicWave(context, options);
}

export function constructAudioWorkletNode(context, name, options) {
    return new AudioWorkletNode(context, name, options);
}

export function constructAudioBuffer(options) {
    return new AudioBuffer(options);
}

export function constructOfflineAudioCompletionEvent(type, eventInitDict) {
    return new OfflineAudioCompletionEvent(type, eventInitDict);
}

export async function decodeAudioData(audioContext, audioData, successCallbackObjRef, errorCallbackObjRef) {
    let successCallback = successCallbackObjRef == null ? () => { } : async (decodedData) => await successCallbackObjRef.invokeMethodAsync('Invoke', DotNet.createJSObjectReference(decodedData));
    let errorCallback = errorCallbackObjRef == null ? () => { } : async (error) => await errorCallbackObjRef.invokeMethodAsync('Invoke', DOMExceptionInformation(error));

    return audioContext.decodeAudioData(audioData, successCallback, errorCallback);
}

function DOMExceptionInformation(error) {
    return {
        name: error.name,
        message: error.message,
        stack: error.stack,
    };
}