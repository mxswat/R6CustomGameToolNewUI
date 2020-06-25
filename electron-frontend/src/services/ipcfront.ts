const windowAny: any = window;

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
    console.log('PlayerUpdated', arg)
})

export { startTool };