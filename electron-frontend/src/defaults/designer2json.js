var gadgets = [
    null,
    "F8",
    "Montange",
    "118",
    "Blackbeard",
    "148",
    "Glaz",
    "1C8",
    "Clash",
    "198",
    "Buck",
    "1F8",
    "Blitz",
    "248",
    "Kali",
    "Node0",
    "[Weapon Dependant(Will Replace Primary)]",
    "D8",
    "Frost",
    "158",
    "Ela",
    "230",
    "Lesion",
    "270",
    "Goyo",
    "278",
    "Kapkan",
    "Node1",
    "[Traps]",
    "E0",
    "Zofia",
    "F0",
    "Hibana",
    "100",
    "Maverick",
    "108",
    "Amaru",
    "168",
    "Doc",
    "1A8",
    "Capitao",
    "1C0",
    "Ash",
    "290",
    "Mozzie",
    "Node2",
    "[Holdable]",
    "E8",
    "Valkyrie",
    "170",
    "Warden",
    "190",
    "Mira",
    "1E0",
    "Alibi",
    "1E8",
    "Jackal",
    "210",
    "Dokkaebi",
    "220",
    "Pulse",
    "228",
    "Lion",
    "288",
    "Iana",
    "258",
    "Maestro",
    "268",
    "Echo",
    "Node3",
    "[Information]",
    "120",
    "Tachanka",
    "140",
    "Bandit",
    "150",
    "Twitch",
    "1D8",
    "Rook",
    "160",
    "Jager",
    "188",
    "Castle",
    "1A0",
    "Finka",
    "298",
    "Thatcher",
    "218",
    "Wamai",
    "1F0",
    "Mute",
    "238",
    "Recruit Primary Gadget Attack",
    "240",
    "Recruit Primary Gadget Defense",
    "128",
    "Nokk",
    "208",
    "Caveira",
    "110",
    "Ying",
    "130",
    "Smoke",
    "1B0",
    "Gridlock",
    "280",
    "Oryx",
    "138",
    "Melusi",
    "Node5",
    "[Denials]",
    "180",
    "Sledge",
    "200",
    "Thermite",
    "250",
    "Fuze",
    "1B8",
    "Ace",
    "Node6",
    "[Destruction]",
];

let holdId;
let holdGadgets = [];
let obj;
let gadgetsJSON = [];

for (let i = 1; i < gadgets.length; i++) {
    const gadgetAtI = gadgets[i];
    if (gadgetAtI.indexOf('[') > -1) {
        gadgetsJSON.push({
            name: gadgetAtI,
            children: holdGadgets,
        })
        holdGadgets = [];
    } else {
        if (i % 2 == 0) {
            holdGadgets.push({
                index: holdId,
                name: gadgetAtI,
                type: null
            })
            holdId = '';
        } else {
            holdId = gadgetAtI
        }
    }
}

console.log(JSON.stringify(gadgetsJSON));