export function getAttribute(object, attribute) { return object[attribute]; }

export function setAttribute(object, attribute, value) { object[attribute] = value; }

export function toArrayBuffer(array) {
    return array.buffer;
}

export function constructAudioContext(contextOptions = null) {
    return new AudioContext(contextOptions)
}

export function constructOcillatorNode(context, options = null) {
    return new OscillatorNode(context, options);
}

export function constructGainNode(context, options = null) {
    return new GainNode(context, options);
}

export function constructAnalyzerNode(context, options = null) {
    return new AnalyserNode(context, options);
}

export async function decodeAudioData(audioContext, audioData, successCallbackObjRef, errorCallbackObjRef) {
    let successCallback = successCallbackObjRef == null ? null : async (decodedData) => await successCallbackObjRef.invokeMethodAsync('Invoke', DotNet.createJSObjectReference(decodedData));
    let errorCallback = errorCallbackObjRef == null ? null : async (error) => await errorCallbackObjRef.invokeMethodAsync('Invoke', DOMExceptionInformation(error));
    return audioContext.decodeAudioData(audioData, successCallback, errorCallback)
}

function DOMExceptionInformation(error) {
    return {
        name: error.name,
        message: error.message,
        stack: error.stack,
    };
}