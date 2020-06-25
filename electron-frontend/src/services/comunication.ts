import { ConnectionBuilder, Connection } from 'electron-cgi'
import { logger, logColors } from './logger';

class ComunicationService {
  private connection: Connection;

  constructor() {
    logger.log('Connecting to R6S Custom Game Tool.exe!', logColors.Green);
    const pathToTool = process.env.NODE_ENV !== 'production'
      ? 'D:/MxRepos/R6S_Custom_Game_Tool/R6S Custom Game Tool/bin/x64/Release/R6S Custom Game Tool.exe'
      : 'R6S Custom Game Tool.exe';
    console.log(`Path to the tool is: ${pathToTool}`);
    this.connection = new ConnectionBuilder()
      .connectTo(pathToTool)
      .build();

    this.connection.on('helloElectron', (request) => {
      console.log(request);
    })

    this.connection.send('helloC#', 'Hello from Electron', (error, theGreeting) => {
      if (error) {
        console.log(error); //serialized exception from the .NET handler
        return;
      }
      console.log(theGreeting); // will print "Hello John!"
    });

    this.connection.onDisconnect = () => {
      console.log('Lost connection to the .Net process');
    };

    this.connection.on('Battleye', (request) => {
      console.log(`Battleye: ${request}`);
    })

    // this.connection.close();
    // this.connection.send('closed', 'closed');
  }
}

const ComunicationServiceInst = new ComunicationService();

export default ComunicationServiceInst;