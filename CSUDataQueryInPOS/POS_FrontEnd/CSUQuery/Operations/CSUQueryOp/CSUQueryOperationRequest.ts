import { ExtensionOperationRequestBase } from "PosApi/Create/Operations";
import CSUQueryOperationResponse from "./CSUQueryOperationResponse";

/**
 * (Sample) Operation request for store hours.
 */
export default class CSUQueryOperationRequest<TResponse extends CSUQueryOperationResponse> extends ExtensionOperationRequestBase<TResponse> {
    constructor(correlationId: string) {
        super(60004, correlationId);
    }
}