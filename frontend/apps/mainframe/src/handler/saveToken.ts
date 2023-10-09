import {IncomingMessageHandler} from "../types/handler";
import {fetch2} from "../utils/fetch";
import {clearFrameContainer, createPluginFrame} from "../utils/frame";
import {addMessageListenerToFrame} from "../utils/message";
import {createIncomingMessageHandler} from "../app/handler";
import {environment} from "apps/environments/environment";

export const saveToken: IncomingMessageHandler<"saveToken"> = (message, context) => {
    if (message.data.token === null) return;
    fetch2(environment.secureBackendUrl, "/Pw/SetEduvidualToken", "POST", JSON.stringify(message.data.token))
        .then(_ => {
            clearFrameContainer("login-container");
            createPluginFrame(environment.ipAddress + context.config.navigation.subRoute, "nav-container");
            addMessageListenerToFrame("nav-container", createIncomingMessageHandler({
                config: context.config,
                sender: "navigation"
            }));
        })
        .catch(e => console.log(e));
}
