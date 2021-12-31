import { ExtensionOperationRequestBase } from "PosApi/Create/Operations";
import LargeQuantityOperationResponse from "./LargeQuantityOperationResponse";

/**
 * (Sample) Operation request for executing end of day operations.
 */
export default class LargeQuantityOperationRequest<TResponse extends LargeQuantityOperationResponse> extends ExtensionOperationRequestBase<TResponse> {
    constructor(correlationId: string) {
        super(60001, correlationId);
    }
}