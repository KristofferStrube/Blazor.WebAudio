export function getAttribute(object, attribute) { return object[attribute]; }

export function constructAudioContext(contextOptions = null) {
    return new AudioContext(contextOptions)
}

export function constructOcillatorNote(context, options = null) {
    return new OscillatorNode(context, options);
}