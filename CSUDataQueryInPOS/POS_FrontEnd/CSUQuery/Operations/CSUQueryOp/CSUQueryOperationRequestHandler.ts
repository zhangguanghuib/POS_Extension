import { ExtensionOperationRequestType, ExtensionOperationRequestHandlerBase } from "PosApi/Create/Operations";
import CSUQueryOperationResponse from "./CSUQueryOperationResponse";
import CSUQueryOperationRequest from "./CSUQueryOperationRequest";
import { ClientEntities } from "PosApi/Entities";


export default class CSUQueryOperationRequestHandler<TResponse extends CSUQueryOperationResponse> extends ExtensionOperationRequestHandlerBase<TResponse> {
    /**
     * Gets the supported request type.
     * @return {RequestType<TResponse>} The supported request type.
     */
    public supportedRequestType(): ExtensionOperationRequestType<TResponse> {
        return CSUQueryOperationRequest;
    }

    /**
     * Executes the request handler asynchronously.
     * @param {CSUQueryOperationRequest<TResponse>} request The request.
     * @return {Promise<ICancelableDataResult<TResponse>>} The cancelable async result containing the response.
     */
    public executeAsync(request: CSUQueryOperationRequest<TResponse>): Promise<ClientEntities.ICancelableDataResult<TResponse>> {

        let response: CSUQueryOperationResponse = new CSUQueryOperationResponse();

        return new Promise((resolve: (value?: ClientEntities.ICancelableDataResult<CSUQueryOperationResponse>) => void) => {
            setTimeout(resolve, 1000);
        }).then((): ClientEntities.ICancelableDataResult<CSUQueryOperationResponse> => {

            this.context.navigator.navigate("RetailTransactionsView");
            return <ClientEntities.ICancelableDataResult<CSUQueryOperationResponse>>{
                canceled: false,
                data: response
            };
        });
    }
}