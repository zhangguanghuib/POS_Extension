import * as Triggers from "PosApi/Extend/Triggers/ProductTriggers";
import { ClientEntities } from "PosApi/Entities";
import LargeQuantityOperationRequest from "../Operations/LargeQtyOp/LargeQuantityOperationRequest";
import LargeQuantityOperationResponse from "../Operations/LargeQtyOp/LargeQuantityOperationResponse";


export default class PostSetQuantityTriggerExt extends Triggers.PostSetQuantityTrigger {
    execute(options: Triggers.IPostSetQuantityTriggerOptions): Promise<void> {
        //if (options.cartLines[0].Quantity > 10) {
        //    this.context.logger.logInformational("Log message from PostSetQuantityTriggerExt execute().", this.context.logger.getNewCorrelationId());

        //    let largeQuantityOperationRequest: LargeQuantityOperationRequest<LargeQuantityOperationResponse> =
        //        new LargeQuantityOperationRequest(this.context.logger.getNewCorrelationId());

        //    return this.context.runtime.executeAsync(largeQuantityOperationRequest).then((result: ClientEntities.ICancelableDataResult<LargeQuantityOperationResponse>):
        //        Promise<void> => {
        //        if (result.canceled) {
        //            return Promise.reject("Canceled");
        //        } else {
        //            return Promise.resolve();
        //        }
        //    });
        //}
        return Promise.resolve();
    }

}
