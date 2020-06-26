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

// TODO USE RXJS
windowAny.ipcRenderer.on('BattleyeIsRunning', (event: any, arg: any) => {
    console.log('BattleyeIsRunning', arg)
})
windowAny.ipcRenderer.on('R6SCGT_IsRunning', (event: any, arg: any) => {
    console.log('R6SCGT_IsRunning', arg)
})
windowAny.ipcRenderer.on('PlayerUpdated', (event: any, arg: any) => {
    PlayerUpdated$.next(arg);
})

export {
    startTool,
    BattleyeIsRunning$,
    R6SCGT_IsRunning$,
    PlayerUpdated$,
};