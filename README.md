# R6CustomGameToolNewUI

## Fast intro
The UI and The C# project are completely separate and they can exist and "work" alone

There is a file `MessageService.cs` that cares about the comunication between the UI and the C# project

Most of the memory editing is done inside `MemoryEngine.cs` to increase the maintainability and having one single point of debug

The communication is done using Electron-cgi, It's a lib used on both the projects.

The Elcetron UI is contained inside `/electron-frontend`

The c# project is inside the root folder.


## The Electron UI

The UI is made using electron and vue.js.

RxJs is used for most of the UI Services

Socket.io is used for the request loadout feature

### How to run and develop the UI

Before you start working on the UI there is a requirements.

* node.js

After you installed node.js you can start working with the tool.

#### How to setup the UI workspace. 

First of all you need to run and build atleast once the c# project

now inside the `electron-frontend` folder run the following commands using CMD or Powershell

* `npm install -g @vue/cli`
* `npm i`

#### How to run the UI

* run in CMD or Powershell `npm run electron:serve`

#### How to build the UI to release the tool

* Build the C# solution as Release => x64 (Be sure is "x64" NOT "any cpu")
* Copy  `R6S Custom Game Tool.exe` from `/bin/x64/Release/R6S Custom Game Tool.exe` to `\electron-frontend\tool` and obfuscate it (how to do it as @Zer0)
* run CMD or Powershell `npm run electron:build`

You will find the .exe file inside `\electron-frontend\dist_electron` named `R6CGT_portable.exe`


## The C#/DotNet project

TODO: Add info about the project.
