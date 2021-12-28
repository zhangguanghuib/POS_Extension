/**
 * SAMPLE CODE NOTICE
 * 
 * THIS SAMPLE CODE IS MADE AVAILABLE AS IS.  MICROSOFT MAKES NO WARRANTIES, WHETHER EXPRESS OR IMPLIED,
 * OF FITNESS FOR A PARTICULAR PURPOSE, OF ACCURACY OR COMPLETENESS OF RESPONSES, OF RESULTS, OR CONDITIONS OF MERCHANTABILITY.
 * THE ENTIRE RISK OF THE USE OR THE RESULTS FROM THE USE OF THIS SAMPLE CODE REMAINS WITH THE USER.
 * NO TECHNICAL SUPPORT IS PROVIDED.  YOU MAY NOT DISTRIBUTE THIS CODE UNLESS YOU HAVE A LICENSE AGREEMENT WITH MICROSOFT THAT ALLOWS YOU TO DO SO.
 */

import { ExtensionOperationRequestType, ExtensionOperationRequestHandlerBase } from "PosApi/Create/Operations";
import LargeQuantityOperationResponse from "./LargeQuantityOperationResponse";
import LargeQuantityOperationRequest from "./LargeQuantityOperationRequest";
import { ClientEntities } from "PosApi/Entities";

/**
 * (Sample) Request handler for the EndOfDayOperationRequest class.
 */
export default class LargeQuantityOperationRequestHandler<TResponse extends LargeQuantityOperationResponse> extends ExtensionOperationRequestHandlerBase<TResponse> {
    /**
     * Gets the supported request type.
     * @return {RequestType<TResponse>} The supported request type.
     */
    public supportedRequestType(): ExtensionOperationRequestType<TResponse> {
        return LargeQuantityOperationRequest;
    }

    public executeAsync(largeQuantityRequest: LargeQuantityOperationRequest<TResponse>): Promise<ClientEntities.ICancelableDataResult<TResponse>> {

        //let asyncResult: AsyncResult = new AsyncResult();

        //return asyncResult;

        return Promise.resolve(<ClientEntities.ICancelableDataResult<TResponse>>{
            canceled: false,
            data: new LargeQuantityOperationResponse()
        });
    }
}