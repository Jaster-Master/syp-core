import { IncomingMessageHandler } from "../types/handler";
import { sendMessageToFrame } from "../utils/message";
import {environment} from "apps/environments/environment";

export const getPublicKeyHandler: IncomingMessageHandler<"getPublicKey"> = (_message, context) => {
    fetch(environment.secureBackendUrl + "/Auth/GetPublicKey", { credentials: "include" })
        .then(response => response.arrayBuffer())
        .then(publicKey => sendMessageToFrame(context.sender == "auth" ? "login-container" : "nav-container", "getPublicKey", publicKey, [publicKey]));
};
