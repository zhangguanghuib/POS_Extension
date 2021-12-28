import * as Triggers from "PosApi/Extend/Triggers/ProductTriggers";
import { ClientEntities } from "PosApi/Entities";

export default class PreSetQuantityTriggerExt extends Triggers.PreSetQuantityTrigger {
    execute(options: Triggers.IPreSetQuantityTriggerOptions): Promise<ClientEntities.ICancelable> {
        return Promise.resolve({ canceled: false });
    }

}