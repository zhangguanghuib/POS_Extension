import * as Views from "PosApi/Create/Views";
import { ObjectExtensions } from "PosApi/TypeExtensions";
import RetailTransactionsViewModel from "./RetailTransactionsViewModel";

export default class RetailTransactionsView extends Views.CustomViewControllerBase {
    public details: Observable<string>;
    public readonly viewModel: RetailTransactionsViewModel;
    public transactionComment: Observable<string>;

    constructor(context: Views.ICustomViewControllerContext) {
        super(context);
        this.details = ko.observable("");
        this.transactionComment = ko.observable("");
        // Initialize the view model.
        this.viewModel = new RetailTransactionsViewModel(context, this.state);

        this.state.title = this.viewModel.title;
    }

    public dispose(): void {
        ObjectExtensions.disposeAllProperties(this);
    }

    public onReady(element: HTMLElement): void {
        ko.applyBindings(this, element);

        this.viewModel.loadAsync().then((result: string) => {
            console.log(result);
            this.details(result);
            // The below way did not work.
            //this.details = ko.observable(result);
        });
    }

    public onTransactionComment(): void {
        this.viewModel.sqlText(this.transactionComment());
        this.details("");
        //console.log(this.transactionComment());
        this.viewModel.loadAsync().then((result: string) => {
            console.log(result);
            this.details(result);
            // The below way did not work.
            //this.details = ko.observable(result);
        });
    }
}