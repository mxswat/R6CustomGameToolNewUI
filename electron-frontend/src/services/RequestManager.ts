import { BehaviorSubject } from 'rxjs';

class RequestManager {
    private loadoutRequests: Array<any> = [];
    // BehaviorSubject because I might need to compare statuses
    private loadoutRequests$: BehaviorSubject<Array<any>> | any = new BehaviorSubject([]);

    constructor() {
        this.loadoutRequests$ = new BehaviorSubject(this.loadoutRequests);
        this.loadoutRequests$.next([{
            uplayName: 'TestUser',
            loadout: {
                Weapon: [{
                    slotIndex: 0,
                    elementIndex: 0,
                }],
                Gadget: [{
                    slotIndex: 0,
                    elementIndex: 0,
                }]
            }
        }]);
    }

    pushLoadoutRequests(request: any) {
        this.loadoutRequests.push(request);
        this.loadoutRequests$.next(this.loadoutRequests);
    }

    removeLoadoutRequests(index: any) {
        this.loadoutRequests.splice(index, 1);
        this.loadoutRequests$.next(this.loadoutRequests);
    }

    getRequests$() {
        return this.loadoutRequests$;
    }
}

const RequestManagerInst = new RequestManager();

export default RequestManagerInst;