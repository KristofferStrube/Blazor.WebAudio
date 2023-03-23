export function getAttribute(object, attribute) { return object[attribute]; }

export function constructAudioContext(contextOptions = null) {
    return new AudioContext(contextOptions)
}

export function constructOcillatorNode(context, options = null) {
    return new OscillatorNode(context, options);
}

export function constructGainNode(context, options = null) {
    return new GainNode(context, options);
}