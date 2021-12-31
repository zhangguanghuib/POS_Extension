
import * as Triggers from "PosApi/Extend/Triggers/PaymentTriggers";

export default class PrePaymentTriggerExt extends Triggers.PrePaymentTrigger {
    execute(options: Triggers.IPrePaymentTriggerOptions): Promise<Commerce.Client.Entities.ICancelable> {
        return Promise.resolve({ canceled: false });
    }

}