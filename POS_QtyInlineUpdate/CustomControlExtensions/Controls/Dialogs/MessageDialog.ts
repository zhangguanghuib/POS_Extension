/**
 * SAMPLE CODE NOTICE
 * 
 * THIS SAMPLE CODE IS MADE AVAILABLE AS IS.  MICROSOFT MAKES NO WARRANTIES, WHETHER EXPRESS OR IMPLIED,
 * OF FITNESS FOR A PARTICULAR PURPOSE, OF ACCURACY OR COMPLETENESS OF RESPONSES, OF RESULTS, OR CONDITIONS OF MERCHANTABILITY.
 * THE ENTIRE RISK OF THE USE OR THE RESULTS FROM THE USE OF THIS SAMPLE CODE REMAINS WITH THE USER.
 * NO TECHNICAL SUPPORT IS PROVIDED.  YOU MAY NOT DISTRIBUTE THIS CODE UNLESS YOU HAVE A LICENSE AGREEMENT WITH MICROSOFT THAT ALLOWS YOU TO DO SO.
 */

import { ShowMessageDialogClientRequest, ShowMessageDialogClientResponse, IMessageDialogOptions } from "PosApi/Consume/Dialogs";
import { IExtensionContext } from "PosApi/Framework/ExtensionContext";
import { ClientEntities } from "PosApi/Entities";
/**
 * The class to show a message dialog.
 */
export default class MessageDialog {
    /**
     * Shows the message dialog.
     * @param {IExtensionContext} context The runtime context within the message is shown
     * @param {string} title The title to display
     * @param {string} message The message to display
     * @returns Promise<void> The promise (always success) after the message has been shown
     */
    public static show(context: IExtensionContext, title: string, message: string): Promise<void> {
        let messageDialogOptions: IMessageDialogOptions = {
            title: title,
            message: message,
            showCloseX: true, // this property will return "Close" as result when "X" is clicked to close dialog.
            button1: {
                id: "Button1Close",
                label: context.resources.getString("string_50"), // OK
                result: "OKResult"
            }
        };

        let dialogRequest: ShowMessageDialogClientRequest<ShowMessageDialogClientResponse> =
            new ShowMessageDialogClientRequest<ShowMessageDialogClientResponse>(messageDialogOptions);

        return context.runtime.executeAsync(dialogRequest).then((value: ClientEntities.ICancelableDataResult<ShowMessageDialogClientResponse>) => {
            return Promise.resolve();
        });
    }
}