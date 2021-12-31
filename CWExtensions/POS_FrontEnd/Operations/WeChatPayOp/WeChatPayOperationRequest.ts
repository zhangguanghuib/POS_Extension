import { ExtensionOperationRequestBase } from "PosApi/Create/Operations";
import WeChatPayOperationResponse from "./WeChatPayOperationResponse";

/**
 * (Sample) Operation request for executing end of day operations.
 */
export default class  WeChatPayOperationRequest<TResponse extends WeChatPayOperationResponse> extends ExtensionOperationRequestBase<TResponse> {
    constructor(correlationId: string) {
        super(60002, correlationId);
    }
}