enum SLOT {
    Primary,
    Secondary
}

enum ITEM_TYPE {
    Weapon,
    Gadget
}

type PlayerData = {
    index: number;
    name: string;
    primary: string;
    secondary: string;
    primarygadget: string;
    secondarygadget: string;
}

export { SLOT, ITEM_TYPE, PlayerData }