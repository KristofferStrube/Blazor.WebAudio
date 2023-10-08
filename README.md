[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](/LICENSE)
[![GitHub issues](https://img.shields.io/github/issues/KristofferStrube/Blazor.WebAudio)](https://github.com/KristofferStrube/Blazor.WebAudio/issues)
[![GitHub forks](https://img.shields.io/github/forks/KristofferStrube/Blazor.WebAudio)](https://github.com/KristofferStrube/Blazor.WebAudio/network/members)
[![GitHub stars](https://img.shields.io/github/stars/KristofferStrube/Blazor.WebAudio)](https://github.com/KristofferStrube/Blazor.WebAudio/stargazers)
[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/KristofferStrube.Blazor.WebAudio?label=NuGet%20Downloads)](https://www.nuget.org/packages/KristofferStrube.Blazor.WebAudio/)

# Blazor.WebAudio
A Blazor wrapper for the [Web Audio browser API.](https://www.w3.org/TR/webaudio/)
This Web API standardizes methods for processing and synthesizing audio in web applications. The primary paradigm is of an audio routing graph, where a number of AudioNode objects are connected together to define the overall audio rendering. This project implements a wrapper around the API for Blazor so that we can easily and safely work with audio in the browser.

**This wrapper is still under development which means it is still not done, but you can still get the preview release on NuGet.**

# Demo
The sample project can be demoed at https://kristofferstrube.github.io/Blazor.WebAudio/

On each page, you can find the corresponding code for the example in the top right corner.

On the [API Coverage Status](https://kristofferstrube.github.io/Blazor.WebAudio/Status) page, you can see how much of the WebIDL specs this wrapper has covered.


# Related repositories
The library uses the following other packages to support its features:
- https://github.com/KristofferStrube/Blazor.WebIDL (To make error handling JSIntero)
- https://github.com/KristofferStrube/Blazor.DOM (To implement *EventTarget*'s in the package like `BaseAudioContext` and `AudioNode`)
- https://github.com/KristofferStrube/Blazor.MediaCaptureStreams (To enable the creation of the `MediaStreamAudioDestinationNode`, `MediaStreamAudioSourceNode`, and other other `MediaStream` related nodes)

# Related articles
This repository was built with inspiration and help from the following series of articles:

- [Typed exceptions for JSInterop in Blazor](https://kristoffer-strube.dk/post/typed-exceptions-for-jsinterop-in-blazor/)
- [Wrapping JavaScript libraries in Blazor WebAssembly/WASM](https://blog.elmah.io/wrapping-javascript-libraries-in-blazor-webassembly-wasm/)
- [Call anonymous C# functions from JS in Blazor WASM](https://blog.elmah.io/call-anonymous-c-functions-from-js-in-blazor-wasm/)
- [Using JS Object References in Blazor WASM to wrap JS libraries](https://blog.elmah.io/using-js-object-references-in-blazor-wasm-to-wrap-js-libraries/)
- [Blazor WASM 404 error and fix for GitHub Pages](https://blog.elmah.io/blazor-wasm-404-error-and-fix-for-github-pages/)
- [How to fix Blazor WASM base path problems](https://blog.elmah.io/how-to-fix-blazor-wasm-base-path-problems/)
