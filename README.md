# Notify Station

This software is designed to be used with either desktop/taskbar shortcuts, or something like a stream deck to alert users on another computer that they need are needed to answer communications (primarily in a live production environment - similar idea to a clearcom flasher).

The stations will need to connect to an API server... one is provided for free by default, but is rate limited using the token bucket algorithm, so if you have more than a handful of computers connecting to the server, you may want to spin up your own server and adjust the rate limiting to avoid any issues.

## Basic pre-compiled configuration
### Installation
