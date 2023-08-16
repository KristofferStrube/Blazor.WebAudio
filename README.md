# Blazor.WebAudio
A Blazor wrapper for the [Web Audio browser API.](https://www.w3.org/TR/webaudio/)
This Web API standardizes methods for processing and synthesizing audio in web applications. The primary paradigm is of an audio routing graph, where a number of AudioNode objects are connected together to define the overall audio rendering. This project implements a wrapper around the API for Blazor so that we can easily and safely work with audio in the browser.

**This wrapper is still being developed so ideas are still being tested and experimented with.**

# Demo
The sample project can be demoed at https://kristofferstrube.github.io/Blazor.WebAudio/

On each page, you can find the corresponding code for the example in the top right corner.

On the [API Coverage Status](https://kristofferstrube.github.io/Blazor.WebAudio/Status) page, you can see how much of the WebIDL specs this wrapper has covered.

# Research
The preliminary analysis of what is needed to wrap this has begun and will be collected in this section before the real work begins.

Seems like there is a lot of work with streams of data in the API. This might be a suitable playground for working with the [IJSUnmarshalledObjectReference](https://learn.microsoft.com/en-us/dotnet/api/microsoft.jsinterop.ijsunmarshalledobjectreference?view=aspnetcore-7.0) in the context of API wrappers.
