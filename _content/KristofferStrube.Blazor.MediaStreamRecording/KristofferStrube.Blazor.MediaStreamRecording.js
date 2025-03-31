export function getAttribute(object, attribute) { return object[attribute]; }

export function constructMediaRecorder(stream, options) {
    return new MediaRecorder(stream, options);
}

export function constructBlobEvent(type, eventInitDict) {
    return new BlobEvent(type, eventInitDict);
}
