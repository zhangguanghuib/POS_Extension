//PreEndTransactionTrigger
import * as Triggers from "PosApi/Extend/Triggers/TransactionTriggers";
import { ClientEntities, ProxyEntities } from "PosApi/Entities";
import LargeQuantityOperationRequest from "../Operations/LargeQtyOp/LargeQuantityOperationRequest";
import LargeQuantityOperationResponse from "../Operations/LargeQtyOp/LargeQuantityOperationResponse";
import { ShowMessageDialogClientRequest, ShowMessageDialogClientResponse } from "PosApi/Consume/Dialogs";
import { StringExtensions } from "PosApi/TypeExtensions";
import { StoreOperations } from "../DataService/DataServiceRequests.g";

export default class PreEndTransactionTriggerExt extends Triggers.PreEndTransactionTrigger {
     private static DIALOG_RESULT_YES: string = "yes";
     private static DIALOG_RESULT_NO: string = "no";
     private static DIALOG_YES_BUTTON_ID: string = "PreEndTransactionTrigger_MessageDialog_Yes";
     private static DIALOG_NO_BUTTON_ID: string = "PreEndTransactionTrigger_MessageDialog_No";
     async execute(options: Triggers.IPreEndTransactionTriggerOptions): Promise<ClientEntities.ICancelable> {

         let lineQtyExceedLimitation: Boolean = false;
         let qtyLimitExceedInfo: string = '';
         let qtyLimitExceedAdditional: string = "All these products exceeded its quantity limitation, need manager approval to proceed";

         let getQtyValidationResultByCartRequest: StoreOperations.GetQtyValidationResultByCartRequest<StoreOperations.GetQtyValidationResultByCartResponse>
             = new StoreOperations.GetQtyValidationResultByCartRequest(options.cart.Id, options.cart.Version);

         let qtyValidationResultByCartResponse: ClientEntities.ICancelableDataResult<StoreOperations.GetQtyValidationResultByCartResponse> = await this.context.runtime.executeAsync(getQtyValidationResultByCartRequest);

         qtyValidationResultByCartResponse.data.result.forEach((validationFailure: ProxyEntities.DataValidationFailure): void => {
             lineQtyExceedLimitation = true;
             qtyLimitExceedInfo += validationFailure.ErrorContext + '\n';
         });
    
         if (lineQtyExceedLimitation) {
             let yesButton: ClientEntities.Dialogs.IDialogResultButton = {
                 id: PreEndTransactionTriggerExt.DIALOG_YES_BUTTON_ID,
                 label: "Yes", // "Yes"
                 result: PreEndTransactionTriggerExt.DIALOG_RESULT_YES
             };
             let noButton: ClientEntities.Dialogs.IDialogResultButton = {
                 id: PreEndTransactionTriggerExt.DIALOG_NO_BUTTON_ID,
                 label: "No", // "No"
                 result: PreEndTransactionTriggerExt.DIALOG_RESULT_NO
             };

             let showMessageDialogClientRequestOptions: ClientEntities.Dialogs.IMessageDialogOptions = {
                 title: "Quantity limitation exceeded",
                 subTitle: StringExtensions.EMPTY,
                 message: qtyLimitExceedInfo + qtyLimitExceedAdditional,
                 button1: yesButton,
                 button2: noButton
             };

             let showMessageDialogClientRequest: ShowMessageDialogClientRequest<ShowMessageDialogClientResponse> =
                 new ShowMessageDialogClientRequest(showMessageDialogClientRequestOptions);

             return this.context.runtime.executeAsync<ShowMessageDialogClientResponse>(showMessageDialogClientRequest).then((result: ClientEntities.ICancelableDataResult<ShowMessageDialogClientResponse>) => {
                 if (result.data.result.dialogResult === PreEndTransactionTriggerExt.DIALOG_RESULT_YES) {
                     let largeQuantityOperationRequest: LargeQuantityOperationRequest<LargeQuantityOperationResponse> =
                         new LargeQuantityOperationRequest(this.context.logger.getNewCorrelationId());

                     return this.context.runtime.executeAsync(largeQuantityOperationRequest).then((result: ClientEntities.ICancelableDataResult<LargeQuantityOperationResponse>): Promise<ClientEntities.ICancelable> => {
                         return Promise.resolve({ canceled: result.canceled });
                     });
                 }
                 else {
                     return Promise.resolve({ canceled: true });
                 }
             });

         } else {
             return Promise.resolve({ canceled: false });
         }
    }

}
