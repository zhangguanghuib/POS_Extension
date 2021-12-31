
/* tslint:disable */
import { ProxyEntities } from "PosApi/Entities";
import { Entities } from "./DataServiceEntities.g";
import { DataServiceRequest, DataServiceResponse } from "PosApi/Consume/DataService";
export { ProxyEntities };
export { Entities };

  export namespace StoreOperations {

  export class GetQtyValidationResultByCartResponse extends DataServiceResponse {
    public result: ProxyEntities.DataValidationFailure[];
  }

  export class GetQtyValidationResultByCartRequest<TResponse extends GetQtyValidationResultByCartResponse> extends DataServiceRequest<TResponse> {
    /**
     * Constructor
     */
      public constructor(cartId: string, cartVersion: number) {
        super();

        this._entitySet = "";
        this._entityType = "";
        this._method = "GetQtyValidationResultByCart";
        this._parameters = { cartId: cartId, cartVersion: cartVersion };
        this._isAction = true;
        this._returnType = ProxyEntities.DataValidationFailureClass;
        this._isReturnTypeCollection = true;
        
      }
  }

}
