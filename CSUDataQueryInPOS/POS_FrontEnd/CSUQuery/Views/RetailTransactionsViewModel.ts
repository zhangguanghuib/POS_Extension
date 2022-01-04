import { ICustomViewControllerContext, ICustomViewControllerBaseState } from "PosApi/Create/Views";
import KnockoutExtensionViewModelBase from "./BaseClasses/KnockoutExtensionViewModelBase";
import { StoreOperations } from "../DataService/DataServiceRequests.g";
import { ClientEntities, ProxyEntities } from "PosApi/Entities";
import { ObjectExtensions } from "PosApi/TypeExtensions";

export default class RetailTransactionsViewModel extends KnockoutExtensionViewModelBase {

    public title: string;
   // public CSVFilePath: Observable<string>;
    public tableHtmlString: string;
    public sqlText: Observable<string>;
    private _context: ICustomViewControllerContext;
    private _customViewControllerBaseState: ICustomViewControllerBaseState;

    constructor(context: ICustomViewControllerContext, state: ICustomViewControllerBaseState) {
        super();

        this.sqlText = ko.observable("");
        this._context = context;
        this.title = "CSU Query result";
        this._customViewControllerBaseState = state;
        this._customViewControllerBaseState.isProcessing = true;
        this.tableHtmlString = "";
    }

    public loadAsync(): Promise<string> {
        this._customViewControllerBaseState.isProcessing = true;
        return this._context.runtime.executeAsync(
            new StoreOperations.CSUQueryForHtmlAsyncRequest<StoreOperations.CSUQueryForHtmlAsyncResponse>(this.sqlText()))
            .then((response: ClientEntities.ICancelableDataResult<StoreOperations.CSUQueryForHtmlAsyncResponse>): Promise<string> => {
                if (ObjectExtensions.isNullOrUndefined(response)
                    || ObjectExtensions.isNullOrUndefined(response.data)
                    || response.canceled) {
                    return Promise.resolve("");
                }
                //this.tableHtmlString = response.data.result;
                this._customViewControllerBaseState.isProcessing = false;
                return Promise.resolve(response.data.result);
            }).catch(() => {
                this._customViewControllerBaseState.isProcessing = false;
            });
    }
}