import { ipcMain, BrowserWindow } from "electron";
import { exec } from 'child_process';
import ComunicationServiceInst from './comunication';

class IpcBack {
    private win: BrowserWindow = null as any;
    constructor() {

    }

    start(win: BrowserWindow) {
        this.win = win;
        ipcMain.on('start-tool', (event, arg) => {
            ComunicationServiceInst.startToolConnection(this.win);
        });

        ipcMain.on('close-tool', (event, arg) => {
            ComunicationServiceInst.closeToolConnection();
            console.log('closed')
            // Kills a process based on filename of the exe and all child processes
            exec(`taskkill /im "R6S Custom Game Tool.exe" /t`, (err: any) => {
                if (err) {
                    throw err
                }
            })
        });

        ipcMain.on('changeWeapon', (event, arg) => {
            ComunicationServiceInst.changeWeapon(arg);
        });

        ipcMain.on('stopTimer', (event, arg) => {
            ComunicationServiceInst.sendToC('stopTimer', arg);
        });
    }
}

const IpcBackIns = new IpcBack();

export default IpcBackIns;