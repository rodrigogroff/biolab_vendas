import { loginOk, logout } from "@app/Infra/Auth";
import { Crypt } from "@app/Infra/Crypt";
import { postPublicPortal } from "@app/Infra/Api";
import { Endpoints } from "@app/Infra/Endpoints";
import { DtoLoginInformation } from "@app/Infra/Dto/DtoLoginInformation";
import { displaySystemPopup, CheckPopUpCloseClick, displaySystemPopupLoading, closeSystemPopupLoading } from "@app/Infra/PopupControl"
import MasterPageLogin from "@app/Components/Views/MasterPageLogin";
import MyForm from "./Views/ViewLogin";

export default class {

  getHtml() {
    return MyForm.getHtml();
  }

  constructor() {

    $(document).ready(function () {
      logout();
      displaySystemPopupLoading();
      postPublicPortal(Endpoints().config)
        .then((resp) => {
          if (resp.ok == true) {
            MasterPageLogin.update(resp.payload);
            var loginElements = MyForm.m_elements();
            document.getElementById(loginElements.labelWelcome).innerText = resp.payload.welcome;
            closeSystemPopupLoading();
            var w = localStorage.getItem('warning')
            if (w != undefined) {
              displaySystemPopup(w);
              localStorage.removeItem('warning')
            }
          } else {
            displaySystemPopup("Servidor offline. Tente mais tarde!")
            closeSystemPopupLoading();
          }
       })
    });

    $(document).on("keydown", function (e) {
      switch (e.keyCode) {
        case 13: btnSubmit_Click(); break;
      }
    });

    document.body.addEventListener("click", (e) => {
      if (CheckPopUpCloseClick(e))
        return;
      // --------------------------------------- page clicks ---------------------------------------
      let elements = MyForm.m_elements();
      switch ($(e.target).attr("id")) {
        case elements.btnSubmit: btnSubmit_Click(); break;
      }
    });

    function btnSubmit_Click() {
      let elements = MyForm.m_elements();
      var usr_login = document.getElementById(elements.formUser).value.trim();
      let formData = DtoLoginInformation(
        usr_login,
        document.getElementById(elements.formPass).value.trim()
      );
      if (formData.email == "" || formData.password == "") {
        displaySystemPopup("Preencha os dados corretamente!")
        return;
      }
      formData.email = Crypt(formData.email)
      formData.password = Crypt(formData.password)
      displaySystemPopupLoading();
      postPublicPortal(Endpoints().authenticate, formData)
        .then((resp) => {
          closeSystemPopupLoading();
          if (resp.ok == true) {
            localStorage.setItem("usuario_login", usr_login);
            loginOk(resp.payload);
            location.href = "/";
          } else {
            displaySystemPopup(resp.msg);
          }
        })
        .catch((resp) => {
          closeSystemPopupLoading();
          displaySystemPopup(resp.msg);
        });
    }
  }
}
