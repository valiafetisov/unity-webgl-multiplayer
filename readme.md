![](screenshot.png)

# Unity WebGL basic multiplayer demo using WebSockets

The project is a basic working example demonstrating the possibility to exchange multiply players positions using websockets. Under the hood it uses slightly modified websockets plugin and small node.js server.

Main code can be found at [`node/app.js`](node/app.js) (node.js server), [`unity/Assets/Multiplayer.cs`](unity/Assets/Multiplayer.cs) (Unity C# script) and [`unity/Assets/Plugins`](unity/Assets/Plugins) (modified plugin).


### Running

1. Start node.js server from the `node` directory: `npm i && npm start`
2. Open `unity` folder as Unity project and run the game `CMD+P`


### Developing

If you're willing to develop server code further, you may find useful to use `npm run dev` instead of `npm start` as it will start script with `nodemon` – a monitoring tool that will restart server after every edit.


### Credits

- Simple Web Sockets Plugin by Unity Technologies ([no longer available in the store](https://assetstore.unity.com/packages/essentials/tutorial-projects/simple-web-sockets-for-unity-webgl-38367))
