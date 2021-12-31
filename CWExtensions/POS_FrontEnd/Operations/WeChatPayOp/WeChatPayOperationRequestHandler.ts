import { ExtensionOperationRequestType, ExtensionOperationRequestHandlerBase } from "PosApi/Create/Operations";
import WeChatPayOperationRequest from "./WeChatPayOperationRequest";
import WeChatPayOperationResponse from "./WeChatPayOperationResponse";
import { ClientEntities, ProxyEntities } from "PosApi/Entities";
import {
    AddTenderLineToCartClientRequest,
    AddTenderLineToCartClientResponse,
    ConcludeTransactionClientRequest,
    ConcludeTransactionClientResponse,
    GetCurrentCartClientRequest,
    GetCurrentCartClientResponse
} from "PosApi/Consume/Cart";
import {
    GetDeviceConfigurationClientRequest,
    GetDeviceConfigurationClientResponse,
    GetExtensionProfileClientResponse
} from "PosApi/Consume/Device";
import ICancelable = ClientEntities.ICancelable;
import ICancelableDataResult = ClientEntities.ICancelableDataResult;
import { ArrayExtensions, ObjectExtensions } from "PosApi/TypeExtensions";

/**
 * (Sample) Request handler for the EndOfDayOperationRequest class.
 */
export default class WeChatPayOperationRequestHandler<TResponse extends WeChatPayOperationResponse> extends ExtensionOperationRequestHandlerBase<TResponse> {
    /**
     * Gets the supported request type.
     * @return {RequestType<TResponse>} The supported request type.
     */
    public supportedRequestType(): ExtensionOperationRequestType<TResponse> {
        return WeChatPayOperationRequest;
    }

    public executeAsync(weChatPayQuantityRequest: WeChatPayOperationRequest<TResponse>): Promise<ClientEntities.ICancelableDataResult<TResponse>> {

        let correlationId: string = weChatPayQuantityRequest.correlationId;

        let getCartRequest: GetCurrentCartClientRequest<GetCurrentCartClientResponse> = new GetCurrentCartClientRequest<GetCurrentCartClientResponse>();
        this.context.runtime.executeAsync(getCartRequest)
            .then((getcurrentCartClientResponse: ICancelableDataResult<GetCurrentCartClientResponse>): void => {
                if (!getcurrentCartClientResponse.canceled) {
                    let cart: ProxyEntities.Cart = getcurrentCartClientResponse.data.result;

                    if (cart.AmountDue != 0) {
                        //{
                        //    //let dialog: BarcodeMsrDialog = new BarcodeMsrDialog();

                        //    //dialog.open().then((dialogResult: IBarcodeMsrDialogResult): Promise<void> => {
                        //    //    return Promise.reject(new ClientEntities.ExtensionError("Please scan a product that matches a selected fulfillment line."));
                        //    //});

                        //    let dialog: GiftCardBalanceDialog = new GiftCardBalanceDialog();

                        //    let giftCard: ProxyEntities.GiftCard = new ProxyEntities.GiftCard{ };
                        //    dialog.open(giftCard).then((dialogResult: IGiftCardBalanceDialogResult): Promise<void> => {
                        //        return Promise.reject(new ClientEntities.ExtensionError("Please scan a product that matches a selected fulfillment line."));
                        //    });

                        //}

                        if (!ObjectExtensions.isNullOrUndefined(cart) && ArrayExtensions.hasElements(cart.CartLines)) {
                            let getDeviceConfigurationClientRequest: GetDeviceConfigurationClientRequest<GetDeviceConfigurationClientResponse> =
                                new GetDeviceConfigurationClientRequest<GetExtensionProfileClientResponse>();

                            this.context.runtime.executeAsync(getDeviceConfigurationClientRequest)
                                .then((getDeviceConfigurationClientResponse: ICancelableDataResult<GetDeviceConfigurationClientResponse>) => {
                                    if (!getDeviceConfigurationClientResponse.canceled) {
                                        let deviceConfiguration: GetDeviceConfigurationClientResponse = getDeviceConfigurationClientResponse.data;

                                        let weChatTenderLine: ProxyEntities.CartTenderLine = {
                                            Amount: cart.AmountDue,
                                            TenderTypeId: "1",
                                            Currency: deviceConfiguration.result.Currency
                                        };

                                        let addTenderLineToCartClientRequest: AddTenderLineToCartClientRequest<AddTenderLineToCartClientResponse> =
                                            new AddTenderLineToCartClientRequest<AddTenderLineToCartClientResponse>(weChatTenderLine);

                                        this.context.runtime.executeAsync(addTenderLineToCartClientRequest)
                                            .then((addTenderLineToCartClientResponse: ICancelableDataResult<AddTenderLineToCartClientResponse>) => {
                                                if (!addTenderLineToCartClientResponse.canceled) {
                                                    let cart: ProxyEntities.Cart = addTenderLineToCartClientResponse.data.result;

                                                    if (ArrayExtensions.hasElements(cart.TenderLines) && cart.TenderLines.length === 1) {
                                                        let concludeTransactionClientRequest: ConcludeTransactionClientRequest<ConcludeTransactionClientResponse> =
                                                            new ConcludeTransactionClientRequest(correlationId);

                                                        this.context.runtime.executeAsync(concludeTransactionClientRequest)
                                                            .then((concludeTransactionClientResponse: ICancelableDataResult<ConcludeTransactionClientResponse>) => {
                                                                if (!concludeTransactionClientResponse.canceled) {
                                                                    return Promise.resolve(<ClientEntities.ICancelableDataResult<TResponse>>{
                                                                        canceled: false,
                                                                        data: new WeChatPayOperationResponse()
                                                                    });
                                                                } else {
                                                                    return Promise.resolve(<ClientEntities.ICancelableDataResult<TResponse>>{
                                                                        canceled: true,
                                                                        data: null
                                                                    });
                                                                }
                                                            });
                                                    }
                                                }
                                            });
                                    }
                                });
                        }
                    }
                }
            });

        return Promise.resolve(<ClientEntities.ICancelableDataResult<TResponse>>{
            canceled: false,
            data: new WeChatPayOperationResponse()
        });
    }
}