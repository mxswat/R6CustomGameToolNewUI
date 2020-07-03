'use strict'

import { app, protocol, BrowserWindow, shell } from 'electron'
import {
  createProtocol,
  installVueDevtools
} from 'vue-cli-plugin-electron-builder/lib'

import path from "path";
import IpcBackIns from './services/ipcBack';
const isDevelopment = process.env.NODE_ENV !== 'production'

// Keep a global reference of the window object, if you don't, the window will
// be closed automatically when the JavaScript object is garbage collected.
let win: any;

// Scheme must be registered before the app is ready
protocol.registerSchemesAsPrivileged([
  { scheme: 'app', privileges: { secure: true, standard: true } }
])

function createWindow() {
  runSocketIo();
  // Create the browser window.
  win = new BrowserWindow({
    width: 1400,
    height: 650,
    webPreferences: {
      // Use pluginOptions.nodeIntegration, leave this alone // No, I don't think I will - Mx
      // See nklayman.github.io/vue-cli-plugin-electron-builder/guide/security.html#node-integration for more info
      nodeIntegration: true,
      preload: path.join(__dirname, 'preload.js')
    }
  })

  // Not needed
  win.removeMenu()

  if (process.env.WEBPACK_DEV_SERVER_URL) {
    // Load the url of the dev server if in development mode
    win.loadURL(process.env.WEBPACK_DEV_SERVER_URL)
    if (!process.env.IS_TEST) win.webContents.openDevTools()
  } else {
    createProtocol('app')
    // Load the index.html when not in development
    win.loadURL('app://./index.html')
  }

  // open target=blank links in default browser
  win.webContents.on('new-window', function (e: any, url: any) {
    // make sure local urls stay in electron perimeter
    if ('file://' === url.substr(0, 'file://'.length)) {
      return;
    }

    // and open every other protocols on the browser      
    e.preventDefault();
    shell.openExternal(url);
  });

  win.on('closed', () => {
    win = null
  })

  IpcBackIns.start(win);
}

// Quit when all windows are closed.
app.on('window-all-closed', () => {
  // On macOS it is common for applications and their menu bar
  // to stay active until the user quits explicitly with Cmd + Q
  if (process.platform !== 'darwin') {
    app.quit()
  }
})

app.on('activate', () => {
  // On macOS it's common to re-create a window in the app when the
  // dock icon is clicked and there are no other windows open.
  if (win === null) {
    createWindow()
  }
})

// This method will be called when Electron has finished
// initialization and is ready to create browser windows.
// Some APIs can only be used after this event occurs.
app.on('ready', async () => {
  if (isDevelopment && !process.env.IS_TEST) {
    // Install Vue Devtools
    try {
      await installVueDevtools()
    } catch (e) {
      console.error('Vue Devtools failed to install:', e.toString())
    }
  }
  createWindow()
})

// Exit cleanly on request from parent process in development mode.
if (isDevelopment) {
  if (process.platform === 'win32') {
    process.on('message', (data) => {
      if (data === 'graceful-exit') {
        app.quit()
      }
    })
  } else {
    process.on('SIGTERM', () => {
      app.quit()
    })
  }
}


function runSocketIo() {
  const app = require('express')();
  const http = require('http').Server(app);
  const io = require('socket.io')(http);
  const port = process.env.PORT || 3000;

  app.get('/', function (req: any, res: any) {
    res.send('hello world!');
  });

  io.on('connection', function (socket: any) {
    console.log('connection');
    socket.on('chat message', function (msg: any) {
      console.log('chat message', msg)
      io.emit('chat message', msg);
    });
  });

  http.listen(port, function () {
    console.log('listening on *:' + port);
  });

}