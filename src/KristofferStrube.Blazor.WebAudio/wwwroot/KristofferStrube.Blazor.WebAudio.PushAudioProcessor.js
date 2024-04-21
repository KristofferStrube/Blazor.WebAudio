class PushAudioProcessor extends AudioWorkletProcessor {
    queue = [];
    backIndex = 0;
    frontIndex = 0;

    constructor(...args) {
        super(...args);
        this.queue = [];
        this.port.onmessage = (e) => {
            e.data.forEach(data => this.queue.push(data));
            this.frontIndex += e.data.length;
            //this.port.postMessage((this.frontIndex - this.backIndex).toString());
        };
    }

    process(inputs, outputs, parameters) {
        const output = outputs[0];
        try {
            const count = this.frontIndex - this.backIndex;
            let data = undefined;
            if (count != 0) {
                data = this.queue[this.backIndex];
                this.backIndex++;

                output.forEach((channel) => {
                    for (let i = 0; i < channel.length; i++) {
                        channel[i] = data[i];
                    }
                });
            }
            if (count < 100) {
                this.port.postMessage((100 - count).toString());
            }
        }
        catch (e) {
            //this.port.postMessage(e.message + "-----" + e.stack);
        }
        return true;
    }
}

registerProcessor("kristoffer-strube-webaudio-push-audio-processor", PushAudioProcessor);