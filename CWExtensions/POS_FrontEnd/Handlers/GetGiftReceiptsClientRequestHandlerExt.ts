import { GetGiftReceiptsClientRequestHandler } from "PosApi/Extend/RequestHandlers/SalesOrdersRequestHandlers";
import { GetGiftReceiptsClientResponse, GetGiftReceiptsClientRequest } from "PosApi/Consume/SalesOrders";
import { ClientEntities } from "PosApi/Entities";

export default class GetGiftReceiptsClientRequestHandlerExt extends GetGiftReceiptsClientRequestHandler {
    public executeAsync(request: GetGiftReceiptsClientRequest<GetGiftReceiptsClientResponse>): Promise<ClientEntities.ICancelableDataResult<GetGiftReceiptsClientResponse>> {
        // when checkout, bypass the gift receipt print 
        if (!request.isCopyOfReceipt) {
            let response: GetGiftReceiptsClientResponse = new GetGiftReceiptsClientResponse(null);
            return Promise.resolve(<ClientEntities.ICancelableDataResult<GetGiftReceiptsClientResponse>>{
                canceled: true,
                data: response
            });
        }
        // when show journal, print receipt through the OOB functionality
        return this.defaultExecuteAsync(request);
    }

}