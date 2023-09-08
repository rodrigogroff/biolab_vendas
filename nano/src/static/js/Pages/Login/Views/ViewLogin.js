import MyCss from "./base.css.js";
import MasterPageLogin from "@app/Components/Views/MasterPageLogin";

export default class {

  static m_elements() {
    return {
      labelWelcome: "labelWelcome",
      imgWorld: "imgWorld",
      imgWorldPic: "src/static/img/world.png",
      formUser: "formUser",
      formPass: "formPass",
      btnSubmit: "btnSubmit",
      divPalA_1: "divPalA_1",
    };
  }

  static getHtml() {
    var elements = this.m_elements();
    var _page = `
              <style>${MyCss.getHtml()}</style>
              <div id="${elements.divPalA_1}" style='background-color: green;width:100%;height:100%'>
              <div class="row">
                <div class="column" align='left'>
                  <b><span style="color: white">Usuário</span></b>
                  <div>
                    <input id="${elements.formUser}" type="text"/>
                  </div>
                </div>
                <div class="column" align='left'>
                  <b><span style="color: white">Senha</span></b>
                  <div>
                    <input id="${elements.formPass}" type="password"/>
                    <div style="margin-top: 8px">
                      <a style="color: white;text-decoration: underline;" href="/recupSenha">Esqueci minha senha</a>
                    </div>
                  </div>
                </div>
                <div class="column" align='left'>
                  <div>
                    <br>
                    <div align="center" id="${elements.btnSubmit}" class="button red">
                      ENTRAR
                    </div>
                  </div>
                </div>
              </div>
              <br>
              <br>
            </div>
            <div style='height:100%;width:100%;margin-top:10px' align='left'>
              <div style='margin-left:32px'>
                <h1>| Bem-vindo!</h1>
                <table width='98%'>
                <tr>
                <td width='2%' valign='top'>
                  <img
                    id="${elements.imgWorld}"
                    src='${elements.imgWorldPic}'
                    srcset="${elements.imgWorldPic} 1x"
                    loading="lazy"
                    width='81'
                    height='77'
                    style='margin-left:10px;margin-top:10px'/>
                </td>
                <td width='98%' valign='top'>
                  <p id="${elements.labelWelcome}" style='margin-left:10px;margin-right:2px'>
                  </p>
                </td>
                </tr>
                </table>
                <br>
                <br>
              </div>
            </div>`
    return `${MasterPageLogin.getHtml(_page)}`;
  }
}
