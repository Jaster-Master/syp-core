import {Component} from "@angular/core";
import {Plugin, PluginListService} from "mainframe-connector";

@Component({
  selector: "app-credit-dash",
  templateUrl: "./credit-dash.component.html",
  styleUrls: ["./credit-dash.component.css"],
})
export class CreditDashComponent {
  plugins: Plugin[] = [];

  constructor(private pluginService: PluginListService) {
    this.pluginService.getPluginList().then((pluginList: Plugin[]) => {
      pluginList.forEach((plugin) => {
        this.plugins.push(plugin);
      });
    });
  }
}
