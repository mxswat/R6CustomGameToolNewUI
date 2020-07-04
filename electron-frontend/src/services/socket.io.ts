import IpcBackIns from './ipcBack';

class SocketIoService {

    constructor() { }

    runSocketIo() {
        const app = require('express')();
        const http = require('http').Server(app);
        const io = require('socket.io')(http);
        const port = process.env.PORT || 3000;

        app.get('/', function (req: any, res: any) {
            res.send('hello world!');
        });

        io.on('connection', function (socket: any) {
            console.log('connection');
            socket.on('request_loadout', function (msg: any) {
                console.log('request_loadout', msg)
                IpcBackIns.sendDataToIpcFront('request_loadout', msg);
            });
        });

        http.listen(port, function () {
            console.log('listening on *:' + port);
        });
    }
}

const SocketIoServiceInst = new SocketIoService();

export default SocketIoServiceInst;