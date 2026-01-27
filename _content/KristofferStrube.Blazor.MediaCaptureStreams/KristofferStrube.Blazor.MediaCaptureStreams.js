export function getAttribute(object, attribute) { return object[attribute]; }

export function setAttribute(object, attribute, value) { object[attribute] = value; }

export function constructMediaStream() {
    return new MediaStream()
}

export function constructMediaStreamFromStreamOrTracks(streamOrTracks) {
    return new MediaStream(streamOrTracks)
}