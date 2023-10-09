export interface MainframeConfig {
    plugins: Record<string, MainframePlugin>;
    login: { subRoute: string };
    navigation: { subRoute: string };
}

export interface MainframePlugin {
    subRoute?: string;
    permissions: string[];
    info: MainframePluginInfos;
}

export interface MainframePluginInfos {
    name: string;
    description: string;
    teachers: string[];
    tags: string[];
    startDate: string; // TODO: Change this in the config, as this should really be of type number
    endDate: string;
    image: string;
    targetUserGroups?: Array<"staff" | "teacher" | "student">;
    repoUrl: string;
    repoOwner: string;
    repoName: string;
}
