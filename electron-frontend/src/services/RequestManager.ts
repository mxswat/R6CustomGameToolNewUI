import { BehaviorSubjects } from "./ipcfront";
import { BehaviorSubject } from 'rxjs';

class RequestManager {
    private loadoutRequests: Array<any> = [];
    private loadoutRequests$: BehaviorSubject<Array<any>> | null = null;

    constructor() {
        BehaviorSubjects.LoadoutRequests$.subscribe(this.pushLoadoutRequests)
        this.loadoutRequests$ = new BehaviorSubject(this.loadoutRequests);
    }

    pushLoadoutRequests(request: any) {
        this.loadoutRequests.push(request);
        this.loadoutRequests$?.next(this.loadoutRequests);
    }

    removeLoadoutRequests(index: any) {
        this.loadoutRequests.splice(index, 1);
        this.loadoutRequests$?.next(this.loadoutRequests);
    }

    getRequests$() {
        return this.loadoutRequests$;
    }
}

const RequestManagerInst = new RequestManager();

export default RequestManagerInst;