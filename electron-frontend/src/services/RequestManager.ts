import { BehaviorSubjects } from "./ipcfront";

class RequestManager {
    private loadoutRequests: Array<any> = [];
    constructor() {
        BehaviorSubjects.LoadoutRequests$.subscribe(this.pushLoadoutRequests)
    }

    pushLoadoutRequests(request: any) {
        this.loadoutRequests.push(request);
    }

    removeLoadoutRequests(index: any) {
        this.loadoutRequests.splice(index, 1);
    }

    getRequests() {
        return [...this.loadoutRequests];
    }
}

const RequestManagerInst = new RequestManager();

export default RequestManagerInst;