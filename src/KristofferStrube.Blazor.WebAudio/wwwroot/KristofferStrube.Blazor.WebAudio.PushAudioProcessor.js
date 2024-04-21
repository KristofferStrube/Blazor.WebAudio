class PushAudioProcessor extends AudioWorkletProcessor {
    queue = [];
    backIndex = 0;
    frontIndex = 0;
    dataRequested = 100;

    static get parameterDescriptors() {
        return [{
            name: 'lowTide',
            defaultValue: 100,
            minValue: 1,
            maxValue: 10000,
            automationRate: "k-rate"
        },
        {
            name: 'highTide',
            defaultValue: 500,
            minValue: 1,
            maxValue: 10000,
            automationRate: "k-rate"
        },
        {
            name: 'bufferRequestSize',
            defaultValue: 100,
            minValue: 1,
            maxValue: 10000,
            automationRate: "k-rate"
        }];
    }

    constructor(...args) {
        super(...args);
        this.queue = [];
        this.port.onmessage = (e) => {
            e.data.forEach(data => this.queue.push(data));
            this.frontIndex += e.data.length;
            this.dataRequested -= e.data.length;
            //this.port.postMessage((this.frontIndex - this.backIndex).toString());
        };
    }

    process(inputs, outputs, parameters) {
        const output = outputs[0];
        const lowTide = parameters.lowTide[0];
        const highTide = parameters.highTide[0];
        const bufferRequestSize = parameters.bufferRequestSize[0];

        try {
            const count = this.frontIndex - this.backIndex;
            if (count != 0) {
                let data = this.queue[this.backIndex];
                this.backIndex++;

                output.forEach((channel) => {
                    for (let i = 0; i < channel.length; i++) {
                        channel[i] = data[i];
                    }
                });
            }
            if (count < lowTide && this.dataRequested + bufferRequestSize < highTide) {
                this.dataRequested += bufferRequestSize;
                this.port.postMessage(bufferRequestSize);
            }
        }
        catch (e) {
            //this.port.postMessage(e.message + "-----" + e.stack);
        }
        return true;
    }
}

registerProcessor("kristoffer-strube-webaudio-push-audio-processor", PushAudioProcessor);