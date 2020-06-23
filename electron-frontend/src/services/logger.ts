const logger = {
    log(content: any, color?: logColors) {
        if (color) {
            console.log(`${color}%s\x1b[0m`, content);
        } else {
            console.log(content);
        }
    }
}

enum logColors {
    Black = "\x1b[30m",
    Red = "\x1b[31m",
    Green = "\x1b[32m",
    Yellow = "\x1b[33m",
    Blue = "\x1b[34m",
    Magenta = "\x1b[35m",
    Cyan = "\x1b[36m",
    White = "\x1b[37m",
}

export { logger, logColors };