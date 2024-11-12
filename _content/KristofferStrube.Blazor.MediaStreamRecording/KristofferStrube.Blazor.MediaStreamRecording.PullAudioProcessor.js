class PullAudioProcessor extends AudioWorkletProcessor {
    queue = [];
    backIndex = 0;
    frontIndex = 0;
    dataRequested = 0;

    static get parameterDescriptors() {
        return [{
            name: 'lowTide',
            defaultValue: 10,
            minValue: 1,
            maxValue: 10000,
            automationRate: "k-rate"
        },
        {
            name: 'highTide',
            defaultValue: 50,
            minValue: 1,
            maxValue: 10000,
            automationRate: "k-rate"
        },
        {
            name: 'bufferRequestSize',
            defaultValue: 10,
            minValue: 1,
            maxValue: 10000,
            automationRate: "k-rate"
        },
        {
            name: 'resolution',
            defaultValue: 1,
            minValue: 1,
            maxValue: 255,
            automationRate: "k-rate"
        }];
    }

    constructor(...args) {
        super(...args);
        this.queue = [];
        this.port.onmessage = (e) => {
            for (let i = 0; i < e.data.length / 128; i++) {
                this.queue.push(e.data.slice(i * 128, (i + 1) * 128));
                this.frontIndex++;
                this.dataRequested--;
            }
        };
    }

    process(inputs, outputs, parameters) {
        const output = outputs[0];
        const lowTide = parameters.lowTide[0];
        const highTide = parameters.highTide[0];
        const bufferRequestSize = parameters.bufferRequestSize[0];
        const resolution = parameters.resolution[0];

        try {
            const count = this.frontIndex - this.backIndex;
            if (count >= output.length) {
                for (let i = 0; i < output.length; i++) {
                    let data = this.queue[this.backIndex];
                    this.backIndex++;

                    let channel = output[i];
                    for (let j = 0; j < channel.length; j++) {
                        if (resolution == 255) {
                            channel[j] = data[j] / 255 * 2 - 1;
                        }
                        else {
                            channel[j] = data[j];
                        }
                    }
                }
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

registerProcessor("kristoffer-strube-webaudio-pull-audio-processor", PullAudioProcessor);