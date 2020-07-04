import {
    BehaviorSubject,
} from 'rxjs';
import RequestManagerInst from './RequestManager';
import { PlayerData } from '@/defaults/general';

const windowAny: any = window;

const BattleyeIsRunning$ = new BehaviorSubject<boolean>(false);
const R6SCGT_IsRunning$ = new BehaviorSubject<boolean>(false);
const PlayerUpdated$ = new BehaviorSubject<any>(null);

const playerList: Array<PlayerData> = [];
const PlayerList$ = new BehaviorSubject<Array<PlayerData>>([]);

function startTool() {
    windowAny.ipcRenderer.send('start-tool', 'start');
}

function stopTimer(params: boolean) {
    windowAny.ipcRenderer.send('stopTimer', params);
}

function changeWeapon(playerIndex: string, slotIndex: string, weaponIndex: string) {
    windowAny.ipcRenderer.send('changeWeapon', {
        playerIndex: playerIndex,
        slotIndex: slotIndex,
        weaponIndex: weaponIndex,
    });
}

function changeGadget(playerIndex: string, slotIndex: string, gadgetIndex: string) {
    windowAny.ipcRenderer.send('changeGadget', {
        playerIndex: playerIndex,
        slotIndex: slotIndex,
        gadgetIndex: gadgetIndex,
    });
}

function randomizeAll() {
    windowAny.ipcRenderer.send('randomizeAll', 'doit');
}

windowAny.ipcRenderer.on('BattleyeIsRunning', (event: any, arg: any) => {
    BattleyeIsRunning$.next(arg);
    console.log('BattleyeIsRunning', arg);
})

windowAny.ipcRenderer.on('R6SCGT_IsRunning', (event: any, arg: any) => {
    R6SCGT_IsRunning$.next(arg);
    console.log('R6SCGT_IsRunning', arg);
})

windowAny.ipcRenderer.on('PlayerUpdated', (event: any, playerData: PlayerData) => {
    PlayerUpdated$.next(playerData as any);
    playerList[playerData.index] = playerData;
    PlayerList$.next(playerList);
})

windowAny.ipcRenderer.on('request_loadout', (event: any, arg: any) => {
    RequestManagerInst.pushLoadoutRequests(arg);
})

const BehaviorSubjects = {
    BattleyeIsRunning$,
    R6SCGT_IsRunning$,
    PlayerUpdated$,
    PlayerList$
}

export {
    startTool,
    changeWeapon,
    changeGadget,
    BehaviorSubjects,
    stopTimer,
    randomizeAll
};