import { ConnectionBuilder, Connection } from 'electron-cgi'
import { logger, logColors } from './logger';
import { app, BrowserWindow } from "electron";
import path from 'path';

class ComunicationService {
  private connection: Connection = null as any;
  private win: BrowserWindow = null as any;
  public isConnected: boolean = false;
  constructor() {
    // Notthing do do here
  }

  /**
   * startToolConnection
   */
  public startToolConnection(win: BrowserWindow) {
    if (!this.isConnected) {
      this.win = win;
      logger.log('Connecting to R6S Custom Game Tool.exe!', logColors.Green);
      // Load files from project https://github.com/nklayman/vue-cli-plugin-electron-builder/issues/353
      const pathToTool = process.env.NODE_ENV !== 'production'
        ? path.join(app.getAppPath(), '..', '..', '/bin/x64/Release/R6S Custom Game Tool.exe')
        : path.join(process.resourcesPath, '/app.asar.unpacked/tool/R6S Custom Game Tool.exe')
      console.log(`Path to the tool is: ${pathToTool}`);
      this.connection = new ConnectionBuilder()
        .connectTo(pathToTool)
        .build();
      this.isConnected = true;
      this.connection.on('helloElectron', (request) => {
        console.log(request);
      })

      this.connection.onDisconnect = () => {
        this.isConnected = false;
        console.log('Lost connection to the .Net process');
        win.webContents.send('tool-disconnected', 'test');
      };

      const eventNames = [
        'BattleyeIsRunning',
        'R6SCGT_IsRunning',
        'PlayerUpdated'
      ]

      for (let i = 0; i < eventNames.length; i++) {
        const eventName = eventNames[i];
        this.connection.on(eventName, (request) => {
          // console.log(`${eventName} ${request}`);
          win.webContents.send(eventName, request);
        })
      }
    }
  }

  changeWeapon(playload: any) {
    this.connection.send('changeWeapon', playload);
  }

  sendToC(eventName: string, playload: any) {
    this.connection.send(eventName, playload);
  }

  closeToolConnection() {
    this.connection.close();
  }
}

const ComunicationServiceInst = new ComunicationService();

export default ComunicationServiceInst;