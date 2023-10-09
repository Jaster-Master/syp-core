import { checkLoginState } from "./login";
import { createPluginFrame } from "../utils/frame";
import { loadMainframeConfig } from "./config";
import { addMessageListenerToFrame } from "../utils/message";
import { createIncomingMessageHandler } from "./handler";
import {environment} from "apps/environments/environment";

main().catch(e => console.error(e));

// TODO: implement loading spinner
export async function main() {
    const config = await loadMainframeConfig();
    const isAuthed = await checkLoginState(environment.secureBackendUrl);

    if (!isAuthed) {
        createPluginFrame(environment.ipAddress + config.login.subRoute, "login-container");

        addMessageListenerToFrame("login-container", createIncomingMessageHandler({
            config,
            sender: "auth"
        }));
    }
    else {
        createPluginFrame(environment.ipAddress + config.navigation.subRoute, "nav-container");
        addMessageListenerToFrame("nav-container", createIncomingMessageHandler({
            config,
            sender: "navigation"
        }));
    }
}
