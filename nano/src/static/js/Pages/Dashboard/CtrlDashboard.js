import { isAuthenticated } from "@app/Infra/Auth";
import { CheckPopUpCloseClick } from "@app/Infra/PopupControl"

import MasterPageHome from "@app/Components/Views/MasterPageHome";
import MyForm from "./Views/ViewDashboard";

export default class {

  getHtml() {
    return MyForm.getHtml();
  }

  constructor() {
    $(document).ready(function () {
      isAuthenticated();
      MasterPageHome.update();
    });

    document.body.addEventListener("click", (e) => {
      if (CheckPopUpCloseClick(e))
        return;
      MasterPageHome.checkClicks(e)
      // --------------------------------------- page clicks ---------------------------------------
    });
  }
}
