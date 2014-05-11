mountebank .NET remoting example
==========================

This example is used to demonstrate using [mountebank](http://www.mbtest.org) with .NET remoting.
For demonstration purposes, we show a simple proxy.

To run:
* Run client.exe
* Change the server port to 1501
* Run Server.exe
* Run `mb --configfile imposters.json`

Send a message from the client, and watch mountebank proxy it. 

This example was forked from rrozel.
