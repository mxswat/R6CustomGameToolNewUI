import { ConnectionBuilder, Connection } from 'electron-cgi'
import { logger, logColors } from './logger';
import { ipcMain, BrowserWindow } from "electron";

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
      const pathToTool = process.env.NODE_ENV !== 'production'
        ? 'D:/MxRepos/R6S_Custom_Game_Tool/R6S Custom Game Tool/bin/x64/Release/R6S Custom Game Tool.exe'
        : 'R6S Custom Game Tool.exe';
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
  
      this.connection.on('BattleyeIsRunning', (request) => {
        console.log(`BattleyeIsRunning: ${request}`);
        win.webContents.send('BattleyeIsRunning', request);
      })
  
      this.connection.on('R6SCGT_IsRunning', (request) => {
        console.log(`R6SCGT_IsRunning: ${request}`);
        win.webContents.send('R6SCGT_IsRunning', request);
      })
  
      this.connection.on('PlayerUpdated', (request) => {
        console.log("PlayerUpdated", Date.now());
        win.webContents.send('PlayerUpdated', request);
      })
    }
  }

  closeToolConnection() {
    this.connection.close();
  }
}

const ComunicationServiceInst = new ComunicationService();

export default ComunicationServiceInst;