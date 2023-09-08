import Popup from "@app/Components/Modals/Popup";
import PopupLoading from "@app/Components/Modals/PopupLoading";
import MasterPageApp from "@app/Components/Views/MasterPageApp";

export default class {

  static m_elements() {
    return  {
      labelUser: "labelUser",
      btnUser: "btnUser",
      labelEmail: "labelEmail",
      labelTelefone: "labelTelefone",
      labelChat: "labelChat",
      imgHeader: "imgHeader",
      imgHeaderPic: "src/static/img/logo-pharmalink-header.png",
      imgBiolab: "imgBiolab",
      imgBiolabPic: "src/static/img/novo logo branco (1) corte.png",
      imgPedido: "imgPedido",
      imgPedidoPic: "src/static/img/cart.png",
      imgPhone: "imgPhone",
      imgPhonePic: "src/static/img/icon-phone.png",
      imgEmail: "imgEmail",
      imgEmailPic: "src/static/img/icon-email.png",
      imgComputer: "imgComputer",
      imgComputerPic: "src/static/img/icon-computer.png",
      imgRodape: "imgRodape",
      imgRodapePic: "src/static/img/logos-pharmalink-gip-rodape.png",
      imgAvatar: "imgAvatar",
      imgAvatarPic: "src/static/img/avatar_padrao.png",
      divPalA_1: "divPalA_1",
      divPalA_2: "divPalA_2",
      divPalC_1: "divPalC_1",
      menuPedidos: "menuPedidos",
    };
  }

  static update() {
      var elements = this.m_elements();
      var r = {
        usuario_login: localStorage.getItem("usuario_login"),
        email: localStorage.getItem("email"),
        telefone: localStorage.getItem("telefone"),
        chat: localStorage.getItem("chat"),
        paletaCorA: localStorage.getItem("paletaCorA"),
        paletaCorB: localStorage.getItem("paletaCorB"),
        paletaCorC: localStorage.getItem("paletaCorC"),
        paletaCorD: localStorage.getItem("paletaCorD"),
      }
      document.getElementById(elements.labelEmail).innerText = r.email;
      document.getElementById(elements.labelTelefone).innerText = r.telefone;
      document.getElementById(elements.labelChat).href = r.chat;
      document.getElementById(elements.labelUser).innerText = r.usuario_login;
      document.getElementById(elements.btnUser).style.backgroundColor =  r.paletaCorD;
      var x1 = document.getElementById(elements.divPalA_1);
      if (x1 != undefined)
        x1.style.backgroundColor = r.paletaCorA;
      var x2 = document.getElementById(elements.divPalA_2);
      if (x2 != undefined)
        x2.style.backgroundColor = r.paletaCorA;
      var x3 = document.getElementById(elements.divPalC_1);
      if (x3 != undefined)
        x3.style.backgroundColor = r.paletaCorC;
  }

  static checkClicks(e) {
    var elements = this.m_elements();
    if (e.srcElement.id == elements.btnUser) {
      var menu1 = document.getElementById('menuRight');
      menu1.style.display = (menu1.style.display === 'block') ? 'none' : 'block';
    }

    if(e.target != null)
      if (e.target.innerText == "PEDIDOS") {
        var menu2 = document.getElementById('menuPedidos');
        menu2.style.display = (menu2.style.display === 'block') ? 'none' : 'block';
      }

    if (e.srcElement != null)
      if (e.srcElement.id == elements.imgPedido) {
        var menu2 = document.getElementById('menuPedidos');
        menu2.style.display = (menu2.style.display === 'block') ? 'none' : 'block';
      }
  }

  static getHtml(page) {
    var elements = this.m_elements();
    var colorA = localStorage.getItem("paletaCorA")
    var colorC = localStorage.getItem("paletaCorC")
    var _page = `${Popup.getHtml()}${PopupLoading.getHtml()}
          <div style="width:100%;max-width:1150px;background-color:white">
            <div align='left' style='height:16px'>
              <img
                id="${elements.imgHeader}"
                src='${elements.imgHeaderPic}'
                srcset="${elements.imgHeaderPic} 1x"
                loading="lazy"
                width='175'
                height='32'
                style='padding-left:20px;padding-top:8px;'/>
            </div>
            <div id="${elements.divPalC_1}" style='background-color: blue;height:120px;width:100%;margin-top:30px'>
              <div align='left'>
                <table width='100%'>
                  <tr>
                    <td valign='top'>
                      <div align='left'>
                        <img
                          id="${elements.imgBiolab}"
                          src='${elements.imgBiolabPic}'
                          srcset="${elements.imgBiolabPic} 1x"
                          loading="lazy"
                          width='167'
                          height='94'
                          onclick="window.location.href = '/'"
                          style='margin-left:10px;margin-top:10px;cursor:pointer'/>
                      </div>
                    </td>
                    <td width='50%' valign='top'>
                      <div align='right' style='margin-top:6px;margin-right:16px'>
                        <table>
                          <tr>
                            <td valign='top'>
                              <div align='right'>
                                <p style='color:white;margin-right:20px;font-size:12px;margin-top:0px'>Bem-vindo,<br>
                                  <span id="${elements.labelUser}" style='color:white;font-size:18px'></span><br>
                                </p>
                                <div align="center" id="btnUser" class="button" style='font-size:10px;margin-right:22px;color:white'> MINHA CONTA </div>
                                <div style='background-color:${colorA};position:absolute;width:120px;display:none' id='menuRight'>
                                  <style> .MenuItem { font-size:12px; cursor:pointer; color:#EAEAEA; margin-left:12px } </style>
                                  <div align='left' style='padding-left:8px;padding-top:2px'>
                                    <p class='MenuItem' onmouseover="this.style.color='white';"onmouseout="this.style.color='#EAEAEA';">Meu Perfil</p>
                                    <p class='MenuItem' onmouseover="this.style.color='white';"onmouseout="this.style.color='#EAEAEA';">Trocar Senha</p>
                                    <p class='MenuItem' onmouseover="this.style.color='white';"onmouseout="this.style.color='#EAEAEA';">Linhas</p>
                                    <p class='MenuItem' onmouseover="this.style.color='white';"onmouseout="this.style.color='#EAEAEA';">Dúvidas</p>
                                    <p class='MenuItem' onmouseover="this.style.color='white';"onmouseout="this.style.color='#EAEAEA';"onclick="window.location.href='/login'">Sair</p>
                                  </div>
                                </div>
                                <br>
                              </div>
                            </td>
                            <td valign='top'>
                              <img
                                id="${elements.imgAvatar}"
                                src='${elements.imgAvatarPic}'
                                srcset="${elements.imgAvatarPic} 1x"
                                loading="lazy"
                                width='60'
                                height='60'/>
                            </td>
                          </tr>
                        </table>
                      </div>
                    </td>
                  </tr>
                </table>
              </div>
            </div>
            <div id="${elements.divPalA_1}" style='background-color:green;height:60px;width:100%'>
              <div align='left'>
                <style>
                  #menu1 { align: left; margin-left: 10px; cursor:pointer }
                  #menu1:hover { background-color: ${colorC}; }
                </style>
                <div style='width:200px' id='menu1'>
                  <table>
                    <tr>
                      <td valign='top'>
                        <style> .MenuPedidos { margin-top:0px; font-size:14px; height:42px; width:200px; cursor:pointer; color:#EAEAEA; margin-left:0px }</style>
                        <img
                          id="${elements.imgPedido}"
                          src='${elements.imgPedidoPic}'
                          srcset="${elements.imgPedidoPic} 1x"
                          loading="lazy"
                          width='43'
                          height='40'
                          style='margin-left:10px;margin-top:10px;cursor:pointer'/>
                          <div style='background-color:${colorC};position:absolute;width:200px;display:none;margin-left:-3px;padding-top:12px' id='menuPedidos'>
                            <p class='MenuPedidos' onmouseover="this.style.backgroundColor = '${colorA}';" onmouseout="this.style.backgroundColor = 'transparent';">
                                <br><span style='margin-left:8px;'>Gerenciamento de lote</span><br>
                            </p>
                            <p class='MenuPedidos' onmouseover="this.style.backgroundColor = '${colorA}'" onmouseout="this.style.backgroundColor = 'transparent';">
                              <br><span style='margin-left:8px;'>Painel de controle</span><br>
                            </p>
                            <p class='MenuPedidos' onmouseover="this.style.backgroundColor = '${colorA}'" onmouseout="this.style.backgroundColor = 'transparent';" onclick="window.location.href = '/pedido'">
                              <br><span style='margin-left:8px;'>Pedido</span><br>
                            </p>
                            <p class='MenuPedidos' onmouseover="this.style.backgroundColor = '${colorA}'" onmouseout="this.style.backgroundColor = 'transparent';">
                              <br><span style='margin-left:8px;'>Pedido em lote</span><br>
                            </p>
                          </div>
                      </td>
                      <td valign='top'>
                        <p style='margin-left:10px;color:white;margin-top:20px;font-size:14px'>
                        PEDIDOS
                        </p>
                      </td>
                    </tr>
                  </table>
                </div>
              </div>
            </div>
            <div style='min-height:200px'>
            ${page}
            </div>
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
