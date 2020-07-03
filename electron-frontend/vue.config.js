module.exports = {
    pluginOptions: {
        electronBuilder: {
            preload: 'src/preload.js',
            builderOptions: {
                // options placed here will be merged with default configuration and passed to electron-builder
                "productName": "R6CGT",
                "appId": "R6ModdingCommunity.R6CGT",
                "win": {
                    "target": ["portable"],
                },
                "portable": {
                    "artifactName": "R6CGT_portable.exe",
                    "requestExecutionLevel": "admin"
                },
                "extraFiles": {
                    "from": "tool",
                    "to": "./resources/app.asar.unpacked/tool/"
                },
            },
            externals: ['socket.io', 'socket.io-client'], 
        }
    }
}