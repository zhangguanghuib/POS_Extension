
/* tslint:disable */
import { ProxyEntities } from "PosApi/Entities";
import { Entities } from "./DataServiceEntities.g";
import { DataServiceRequest, DataServiceResponse } from "PosApi/Consume/DataService";
export { ProxyEntities };
export { Entities };

  export namespace StoreOperations {

  export class CSUQueryForCSVAsyncResponse extends DataServiceResponse {
    public result: string;
  }

  export class CSUQueryForCSVAsyncRequest<TResponse extends CSUQueryForCSVAsyncResponse> extends DataServiceRequest<TResponse> {
    /**
     * Constructor
     */
      public constructor(sqlText: string) {
        super();

        this._entitySet = "";
        this._entityType = "";
        this._method = "CSUQueryForCSVAsync";
        this._parameters = { sqlText: sqlText };
        this._isAction = true;
        this._returnType = null;
        this._isReturnTypeCollection = false;
        
      }
  }

  export class CSUQueryForHtmlAsyncResponse extends DataServiceResponse {
    public result: string;
  }

  export class CSUQueryForHtmlAsyncRequest<TResponse extends CSUQueryForHtmlAsyncResponse> extends DataServiceRequest<TResponse> {
    /**
     * Constructor
     */
      public constructor(sqlText: string) {
        super();

        this._entitySet = "";
        this._entityType = "";
        this._method = "CSUQueryForHtmlAsync";
        this._parameters = { sqlText: sqlText };
        this._isAction = true;
        this._returnType = null;
        this._isReturnTypeCollection = false;
        
      }
  }

}
