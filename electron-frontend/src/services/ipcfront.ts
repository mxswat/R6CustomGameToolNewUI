import {
    BehaviorSubject,
} from 'rxjs';

const windowAny: any = window;

const BattleyeIsRunning$ = new BehaviorSubject(null);
const R6SCGT_IsRunning$ = new BehaviorSubject(null);
const PlayerUpdated$ = new BehaviorSubject(null);

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

windowAny.ipcRenderer.on('BattleyeIsRunning', (event: any, arg: any) => {
    BattleyeIsRunning$.next(arg);
    console.log('BattleyeIsRunning', arg);
})
windowAny.ipcRenderer.on('R6SCGT_IsRunning', (event: any, arg: any) => {
    R6SCGT_IsRunning$.next(arg);
    console.log('R6SCGT_IsRunning', arg);
})
windowAny.ipcRenderer.on('PlayerUpdated', (event: any, arg: any) => {
    PlayerUpdated$.next(arg);
    // console.log('PlayerUpdated', arg);
})

const BehaviorSubjects = {
    BattleyeIsRunning$,
    R6SCGT_IsRunning$,
    PlayerUpdated$,
}

export {
    startTool,
    changeWeapon,
    BehaviorSubjects,
    stopTimer
};