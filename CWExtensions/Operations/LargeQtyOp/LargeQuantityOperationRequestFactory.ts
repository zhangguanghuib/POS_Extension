import LargeQuantityOperationResponse from "./LargeQuantityOperationResponse";
import LargeQuantityOperationRequest from "./LargeQuantityOperationRequest";
import { ExtensionOperationRequestFactoryFunctionType, IOperationContext } from "PosApi/Create/Operations";
import { ClientEntities } from "PosApi/Entities";

let getOperationRequest: ExtensionOperationRequestFactoryFunctionType<LargeQuantityOperationResponse> =
    /**
     * Gets an instance of EndOfDayOperationRequest.
     * @param {number} operationId The operation Id.
     * @param {string[]} actionParameters The action parameters.
     * @param {string} correlationId A telemetry correlation ID, used to group events logged from this request together with the calling context.
     * @return {EndOfDayOperationRequest<TResponse>} Instance of EndOfDayOperationRequest.
     */
    function (
        context: IOperationContext,
        operationId: number,
        actionParameters: string[],
        correlationId: string
    ): Promise<ClientEntities.ICancelableDataResult<LargeQuantityOperationRequest<LargeQuantityOperationResponse>>> {

        let operationRequest: LargeQuantityOperationRequest<LargeQuantityOperationResponse> = new LargeQuantityOperationRequest<LargeQuantityOperationResponse>(correlationId);
        return Promise.resolve(<ClientEntities.ICancelableDataResult<LargeQuantityOperationRequest<LargeQuantityOperationResponse>>>{
            canceled: false,
            data: operationRequest
        });
    };

export default getOperationRequest;