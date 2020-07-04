import { BehaviorSubject } from 'rxjs';

class RequestManager {
    private loadoutRequests: Array<any> = [];
    // BehaviorSubject because I might need to compare statuses
    private loadoutRequests$ = new BehaviorSubject<Array<any>>([]);

    constructor() {
        this.debugUi();
    }

    debugUi() {
        const example = {
            uplayName: 'TestUser',
            debug: true,
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
        }
        const tester = [];
        const count = 8;
        for (let i = 0; i < count; i++) {
            tester.push({
                uplayName: example.uplayName + i,
                loadout: example.loadout
            })
        }
        this.loadoutRequests = tester;
        this.loadoutRequests$.next(tester);
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