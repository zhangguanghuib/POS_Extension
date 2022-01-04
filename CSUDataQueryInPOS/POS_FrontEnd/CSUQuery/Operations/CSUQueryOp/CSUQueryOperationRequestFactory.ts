/**
 * SAMPLE CODE NOTICE
 * 
 * THIS SAMPLE CODE IS MADE AVAILABLE AS IS.  MICROSOFT MAKES NO WARRANTIES, WHETHER EXPRESS OR IMPLIED,
 * OF FITNESS FOR A PARTICULAR PURPOSE, OF ACCURACY OR COMPLETENESS OF RESPONSES, OF RESULTS, OR CONDITIONS OF MERCHANTABILITY.
 * THE ENTIRE RISK OF THE USE OR THE RESULTS FROM THE USE OF THIS SAMPLE CODE REMAINS WITH THE USER.
 * NO TECHNICAL SUPPORT IS PROVIDED.  YOU MAY NOT DISTRIBUTE THIS CODE UNLESS YOU HAVE A LICENSE AGREEMENT WITH MICROSOFT THAT ALLOWS YOU TO DO SO.
 */

import CSUQueryOperationResponse from "./CSUQueryOperationResponse";
import CSUQueryOperationRequest from "./CSUQueryOperationRequest";
import { ExtensionOperationRequestFactoryFunctionType, IOperationContext } from "PosApi/Create/Operations";
import { ClientEntities } from "PosApi/Entities";

let getOperationRequest: ExtensionOperationRequestFactoryFunctionType<CSUQueryOperationResponse> =
    /**
     * Gets an instance of StoreHoursOperationRequest.
     * @param {number} operationId The operation Id.
     * @param {string[]} actionParameters The action parameters.
     * @param {string} correlationId A telemetry correlation ID, used to group events logged from this request together with the calling context.
     * @return {StoreHoursOperationRequest<TResponse>} Instance of StoreHoursOperationRequest.
     */
    function (
        context: IOperationContext,
        operationId: number,
        actionParameters: string[],
        correlationId: string
    ): Promise<ClientEntities.ICancelableDataResult<CSUQueryOperationRequest<CSUQueryOperationResponse>>> {
        let operationRequest: CSUQueryOperationRequest<CSUQueryOperationResponse> =
            new CSUQueryOperationRequest<CSUQueryOperationResponse>(correlationId);

        return Promise.resolve(<ClientEntities.ICancelableDataResult<CSUQueryOperationRequest<CSUQueryOperationResponse>>>{
            canceled: false,
            data: operationRequest
        });
    };

export default getOperationRequest;