import Popup from "@app/Components/Modals/Popup";
import PopupLoading from "@app/Components/Modals/PopupLoading";
import MasterPageApp from "@app/Components/Views/MasterPageApp";

export default class {

  static m_elements()
  {
    return  {
      labelEmail: "labelEmail",
      labelTelefone: "labelTelefone",
      labelChat: "labelChat",
      imgHeader: "imgHeader",
      imgHeaderPic: "src/static/img/logo-pharmalink-header.png",
      imgBiolab: "imgBiolab",
      imgBiolabPic: "src/static/img/novo logo branco (1) corte.png",
      imgPhone: "imgPhone",
      imgPhonePic: "src/static/img/icon-phone.png",
      imgEmail: "imgEmail",
      imgEmailPic: "src/static/img/icon-email.png",
      imgComputer: "imgComputer",
      imgComputerPic: "src/static/img/icon-computer.png",
      imgRodape: "imgRodape",
      imgRodapePic: "src/static/img/logos-pharmalink-gip-rodape.png",
      divPalA_1: "divPalA_1",
      divPalA_2: "divPalA_2",
      divPalC_1: "divPalC_1",
    };
  }

  static update(payload)
  {
      var elements = this.m_elements();
      var r = { }
      if (payload != undefined)
      {
        r.email = payload.email;
        r.telefone = payload.telefone;
        r.chat = payload.chat;
        r.paletaCorA = payload.paletaCorA;
        r.paletaCorB = payload.paletaCorB;
        r.paletaCorC = payload.paletaCorC;
        r.paletaCorD = payload.paletaCorD;

        localStorage.setItem("nome", payload.nome);
        localStorage.setItem("email", payload.email);
        localStorage.setItem("telefone", payload.telefone);
        localStorage.setItem("chat", payload.chat);
        localStorage.setItem("paletaCorA", payload.paletaCorA);
        localStorage.setItem("paletaCorB", payload.paletaCorB);
        localStorage.setItem("paletaCorC", payload.paletaCorC);
        localStorage.setItem("paletaCorD", payload.paletaCorD);
      }
      else
      {
        r.email = localStorage.getItem("email");
        r.telefone = localStorage.getItem("telefone");
        r.chat = localStorage.getItem("chat");
        r.paletaCorA = localStorage.getItem("paletaCorA");
        r.paletaCorB = localStorage.getItem("paletaCorB");
        r.paletaCorC = localStorage.getItem("paletaCorC");
        r.paletaCorD = localStorage.getItem("paletaCorD");
      }

      document.getElementById(elements.labelEmail).innerText = r.email;
      document.getElementById(elements.labelTelefone).innerText = r.telefone;
      document.getElementById(elements.labelChat).href = r.chat;

      var x1 = document.getElementById(elements.divPalA_1);
      x1.style.backgroundColor = r.paletaCorA;

      var x2 = document.getElementById(elements.divPalA_2);
      x2.style.backgroundColor = r.paletaCorA;

      var x3 = document.getElementById(elements.divPalC_1);
      x3.style.backgroundColor = r.paletaCorC;
  }

  static getHtml(page) {
    var elements = this.m_elements();

    var _page = `${Popup.getHtml()}${PopupLoading.getHtml()}
          <div style="width:100%;max-width:1150px;background-color:white">
            <div align='left' style='height:50px'>
              <img
                id="${elements.imgHeader}"
                src='${elements.imgHeaderPic}'
                srcset="${elements.imgHeaderPic} 1x"
                loading="lazy"
                width='175'
                height='32'
                style='padding-left:20px;margin-top:20px'/>
            </div>
            <div id="${elements.divPalC_1}" style='background-color: blue;height:120px;width:100%;margin-top:30px'>
              <div align='left'>
                <img
                  id="${elements.imgBiolab}"
                  src='${elements.imgBiolabPic}'
                  srcset="${elements.imgBiolabPic} 1x"
                  loading="lazy"
                  width='167'
                  height='94'
                  style='margin-left:10px;margin-top:10px'/>
              </div>
            </div>
            ${page}
            <div id="${elements.divPalA_2}" style='background-color:green;height:100%;width:100%'>
              <br>
              <div class="row">
                <div class="column" align='left' style='margin-top:20px;margin-bottom:20px'>
                  <img
                    id="${elements.imgPhone}"
                    src='${elements.imgPhonePic}'
                    srcset="${elements.imgPhonePic} 1x"
                    loading="lazy"
                    width='50'
                    height='49'/>
                  <span id="${elements.labelTelefone}" style='color:white;margin-left:70px;margin-top:-35px'></span>
                </div>
                <div class="column" align='left' style='margin-top:20px;margin-bottom:20px'>
                  <img
                    id="${elements.imgEmail}"
                    src='${elements.imgEmailPic}'
                    srcset="${elements.imgEmailPic} 1x"
                    loading="lazy"
                    width='50'
                    height='42'/>
                  <span id="${elements.labelEmail}" style='color:white;margin-left:70px;margin-top:-30px'></span>
                </div>
                <div class="column" align='left' style='margin-top:20px;margin-bottom:20px'>
                  <img
                    id="${elements.imgComputer}"
                    src='${elements.imgComputerPic}'
                    srcset="${elements.imgComputerPic} 1x"
                    loading="lazy"
                    width='54'
                    height='44'/>
                  <a
                  id="${elements.labelChat}"
                  style='text-decoration: underline; color: white;margin-left:70px;margin-top:-30px'
                    href='http://chat.pharmalinkonline.com.br/orbium/frontServices/default.aspx?chat=46&group=Origem&url=http://chat.pharmalinkonline.com.br/orbium/frontServices/test.aspx'>
                    Clique aqui</a>
                </div>
              </div>
              <br>
              <br>
            </div>
            <div style='height:120px;width:100%'>
              <div style='margin-left:32px;margin-top:30px'>
                <table style='width:100%'>
                  <tr>
                    <td valign='top'>
                      Copyright 2023 - Todos os direitos reservados<br>
                      Av. Eng° Eusébio Stevaux, 1.566 - Santo Amaro - CEP: 04696-000 - São Paulo - SP
                    </td>
                    <td align='right'>
                      <img
                        id="${elements.imgRodape}"
                        src='${elements.imgRodapePic}'
                        srcset="${elements.imgRodapePic} 1x"
                        loading="lazy"
                        width='235'
                        height='115'
                        style='margin-right:10px' />
                    </td>
                  </tr>
                </table>
              </div>
            </div>
            <br>
          </div>
          <br>
        </div>`;
      return MasterPageApp.getHtml(_page);
  }
}
