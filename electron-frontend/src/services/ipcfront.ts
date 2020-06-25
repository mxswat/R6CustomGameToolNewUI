function startTool() {
    window.ipcRenderer.send('start-tool', 'start');
}



export { startTool };