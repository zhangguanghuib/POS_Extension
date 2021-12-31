import WeChatPayOperationResponse from "./WeChatPayOperationResponse";
import WeChatPayOperationRequest from "./WeChatPayOperationRequest";
import { ExtensionOperationRequestFactoryFunctionType, IOperationContext } from "PosApi/Create/Operations";
import { ClientEntities } from "PosApi/Entities";

let getOperationRequest: ExtensionOperationRequestFactoryFunctionType<WeChatPayOperationResponse> =
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
    ): Promise<ClientEntities.ICancelableDataResult<WeChatPayOperationRequest<WeChatPayOperationResponse>>> {

        let operationRequest: WeChatPayOperationRequest<WeChatPayOperationResponse> = new WeChatPayOperationRequest<WeChatPayOperationResponse>(correlationId);
        return Promise.resolve(<ClientEntities.ICancelableDataResult<WeChatPayOperationRequest<WeChatPayOperationResponse>>>{
            canceled: false,
            data: operationRequest
        });
    };

export default getOperationRequest;