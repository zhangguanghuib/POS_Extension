/**
 * SAMPLE CODE NOTICE
 * 
 * THIS SAMPLE CODE IS MADE AVAILABLE AS IS.  MICROSOFT MAKES NO WARRANTIES, WHETHER EXPRESS OR IMPLIED,
 * OF FITNESS FOR A PARTICULAR PURPOSE, OF ACCURACY OR COMPLETENESS OF RESPONSES, OF RESULTS, OR CONDITIONS OF MERCHANTABILITY.
 * THE ENTIRE RISK OF THE USE OR THE RESULTS FROM THE USE OF THIS SAMPLE CODE REMAINS WITH THE USER.
 * NO TECHNICAL SUPPORT IS PROVIDED.  YOU MAY NOT DISTRIBUTE THIS CODE UNLESS YOU HAVE A LICENSE AGREEMENT WITH MICROSOFT THAT ALLOWS YOU TO DO SO.
 */

import MessageDialog from "../Controls/Dialogs/MessageDialog";
import { IExtensionContext } from "PosApi/Framework/ExtensionContext";

/**
 * Message method helpers.
 */
export default class MessageHelpers {
    /**
     * Shows the message dialog.
     * @param {IExtensionContext} context The runtime context within the message is shown
     * @param {string} title The title to display
     * @param {string} message The message to display
     * @returns Promise<void> The promise (always success) after the message has been shown
     */
    public static ShowMessage(context: IExtensionContext, title: string, message: string): Promise<void> {
        return MessageDialog.show(context, title, message);
    }

    /**
     * Shows the error message dialog.
     * @param {IExtensionContext} context The runtime context within the message is shown
     * @param {string} message The message to display
     * @param {string} error The error object
     * @returns Promise<void> The promise after the message has been shown as success
     */
    public static ShowErrorMessage(context: IExtensionContext, message: string, error: any): Promise<void> {
        let title: string = context.resources.getString("string_70"); // Error:\n
        return MessageDialog.show(context, title, message).then(() => {
            return Promise.reject(error);
        }).catch(() => {
            return Promise.reject(error);
        });
    }
}
