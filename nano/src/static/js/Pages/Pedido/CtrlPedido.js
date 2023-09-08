import { isAuthenticated } from "@app/Infra/Auth";
import { updateHTML } from "@app/Infra/Builder"
import { displaySystemPopup, CheckPopUpCloseClick, displaySystemPopupLoading, closeSystemPopupLoading } from "@app/Infra/PopupControl"
import { Endpoints } from "@app/Infra/Endpoints";
import { postTokenPortal } from "@app/Infra/Api";
import MasterPageHome from "@app/Components/Views/MasterPageHome";
import MyForm from "./Views/ViewPedido";

export default class {

  getHtml() {
    return MyForm.getHtml();
  }

  constructor() {
    $(document).ready(function () {
      // -- system -----------------------------------------
      isAuthenticated();
      MasterPageHome.update();
      // -- system -----------------------------------------
      var elements = MyForm.m_elements();
      const inputElement = document.getElementById(elements.txtPDV); // Replace with your actual input element ID
      inputElement.addEventListener("paste", (event) => {
        Submit()
      });
    });

    function Submit() {
      var elements = MyForm.m_elements();
      document.getElementById(elements.tipoPedidos).style.display = 'none'
      var formData = {
          filter: document.getElementById(elements.txtPDV).value.trim()
      }
      displaySystemPopupLoading();
      postTokenPortal(Endpoints().pdvInfo, formData)
        .then((resp) => {
          closeSystemPopupLoading();
          if (resp.ok == true) {
              var report = document.getElementById(elements.report);
              var colorA = localStorage.getItem("paletaCorA")
              report.style.display = 'block'
              var repHtml = '';
              for(let i=0; i < resp.payload.length; i = i + 1) {
                let o  = resp.payload[i];
                repHtml += `
                  <p
                    id='repeater'
                    pdv='${o.cnpj} - ${o.razaoSocial}'
                    pdvId = '${o.id}'
                    cnpj = '${o.cnpj}'
                    style='font-size:14px;height:42px;cursor:pointer;color:white;background-color:${colorA};margin-left:12px;margin-top:2px;'>
                    <br>
                    <span id='span${i}' style='margin-left:8px;'>${o.cnpj} - ${o.razaoSocial}</span>
                    <br>
                  </p>`
              }
              updateHTML(elements.reportRepeater, repHtml);
          }
        })
        .catch((resp) => {
          console.log(resp)
          closeSystemPopupLoading();
          displaySystemPopup(resp.msg);
        });
    }

    $(document).on("keydown", function (e) {
      switch (e.keyCode) {
        case 13:
          Submit();
          break;
      }
    });

    document.body.addEventListener("click", (e) => {
      if (CheckPopUpCloseClick(e))
        return;
      MasterPageHome.checkClicks(e)
      // --------------------------------------- page clicks ---------------------------------------
      var elements = MyForm.m_elements();
      // proceder com pedido representante
      if (e.target.id == elements.imgPedidoRep) {
        sessionStorage.setItem('tipoPedido', 'REP')
        return;
      }
      // clicks dentro das caixas
      if (e.target != null)
        if (e.target.parentNode != null) {
          if (e.target.parentNode.id == 'repeater') {
            if (e.target.parentNode.attributes != undefined) {
              let pdv = e.target.parentNode.attributes.getNamedItem("pdv").value;
              let pdvId =e.target.parentNode.attributes.getNamedItem("pdvId").value;
              let cnpj =e.target.parentNode.attributes.getNamedItem("cnpj").value;
              sessionStorage.clear();
              sessionStorage.setItem('pdv',pdv)
              sessionStorage.setItem('pdvId',pdvId)
              sessionStorage.setItem('cnpj',cnpj)
              var report = document.getElementById(elements.report);
                report.style.display = 'none'
              var tipoPedidos = document.getElementById(elements.tipoPedidos);
              tipoPedidos.style.display = 'block'
            }
          }
        }
    });
  }
}
