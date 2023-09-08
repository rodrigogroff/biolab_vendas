import MyCss from "./base.css.js";
import MasterPageHome from "@app/Components/Views/MasterPageHome";

export default class {

  static m_elements() {
    return {
      reportRepeaterDistPref: "reportRepeaterDistPref",
      reportRepeaterDistSel: "reportRepeaterDistSel",
      btnMoveOneRight: "btnMoveOneRight",
      btnMoveOneLeft: "btnMoveOneLeft",
      btnMoveAllRight: "btnMoveAllRight",
      btnMoveAllLeft: "btnMoveAllLeft",
      btnAvancar: "btnAvancar",
      btnVoltar: "btnVoltar",
      selPagto: "selPagto",
      labelTipoPedido: "labelTipoPedido",
    };
  }

  static getHtml() {
    var elements = this.m_elements();
    var colorA = localStorage.getItem("paletaCorA")
    var colorC = localStorage.getItem("paletaCorC")
    var colorD = localStorage.getItem("paletaCorD")
    var btnStyle = `background-color:${colorA};color:white;border-radius:75%;width:18px;font-size:12px`
    var pdv = sessionStorage.getItem('pdv');

    var _page = `
    <style>${MyCss.getHtml()}</style>
    <div align='left' style='margin-top:10px;margin-bottom:10px;margin-left:16px'>
      <h1>| Pedido <span id='${elements.labelTipoPedido}'></span></h1>
    </div>
    <table width='100%' style='border-spacing: 0;'>
      <tr>
        <td width='25%' style='padding:0;margin:0;'>
          <div align='center' style='background-color:${colorA};width:100%;height:100%'>
            <br>
            <p style='background-color:${colorD};color:white;width:16px;border-radius: 50%'>1</p>
            <p style='color:white;font-size:12px'>Seleção de PDV e Ofertas</p>
            <br>
            <br>
          </div>
        </td>
        <td width='25%' style='padding:0;margin:0;'>
          <div align='center' style='background-color:${colorC};width:100%;height:100%'>
            <br>
            <p style='background-color:${colorD};color:white;width:16px;border-radius: 50%'>2</p>
            <p style='color:white;font-size:12px'>Seleção de distribuidores e Pagamento</p>
            <br>
            <br>
          </div>
        </td>
        <td width='25%' style='padding:0;margin:0;'>
          <div align='center' style='background-color:${colorA};width:100%;height:100%'>
            <br>
            <p style='background-color:${colorD};color:white;width:16px;border-radius: 50%'>3</p>
            <p style='color:white;font-size:12px'>Seleção de Produtos</p>
            <br>
            <br>
          </div>
        </td>
        <td width='25%' style='padding:0;margin:0;'>
          <div align='center' style='background-color:${colorA};width:100%;height:100%'>
            <br>
            <p style='background-color:${colorD};color:white;width:16px;border-radius: 50%'>4</p>
            <p style='color:white;font-size:12px'>Validação e Envio de pedido</p>
            <br>
            <br>
          </div>
        </td>
      </tr>
    </table>
    <div style='height:100%;min-height:420px'>
      <div style='margin-left:20px' align='left'>
        <p style='color:${colorA};font-size:14px;padding-top:8px'>PDV: <span style='color:black'>${pdv}</span></p>
        <p style='color:${colorA};font-size:14px;padding-top:12px'>FORMA DE PAGAMENTO</p>
        <select id='${elements.selPagto}' style='width:400px'>
          <option value="">(Selecione)</option>
          <option value="1">À Vista</option>
          <option value="2">À Prazo</option>
          <option value="4">42 dias</option>
          <option value="5">35 / 49 / 63 dias</option>
          <option value="3">28 dias</option></select>
        </select>
        <br>
        <table width='800px'>
          <tr>
            <td width='280px' valign='top'>
              <p>
                Selecione os distribuidores de sua preferência.<br>
              </p>
              <p style='color:${colorA};font-size:14px;padding-top:12px'>DISTRIBUIDORES DE PREFERÊNCIA</p>
              <div style='height:200px;border: 1px solid #EAEAEA; box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.25);'>
                <div id='${elements.reportRepeaterDistPref}' />
              </div>
            </td>
            <td width='60px' valign='top'>
              <div align="center">
                <br><br><br><br><br><br>
                <div style='margin-top:10px'><div align="center" id="${elements.btnMoveOneRight}" style='${btnStyle}' class="button"> > </div></div>
                <div style='margin-top:10px'><div align="center" id="${elements.btnMoveAllRight}" style='${btnStyle}' class="button"> >> </div></div>
                <div style='margin-top:10px'><div align="center" id="${elements.btnMoveAllLeft}" style='${btnStyle}' class="button"> << </div></div>
                <div style='margin-top:10px'><div align="center" id="${elements.btnMoveOneLeft}" style='${btnStyle}' class="button"> < </div></div>
              </div>
            </td>
            <td width='280px' valign='top'>
              <p>
              <br>
              </p>
              <p style='color:${colorA};font-size:14px;padding-top:12px'>DISTRIBUIDORES SELECIONADOS</p>
              <div style='height:200px;border: 1px solid #EAEAEA; box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.25);'>
                <div id='${elements.reportRepeaterDistSel}' />
              </div>
            </td>
          </tr>
        </table>
        <br>
      </div>
    </div>
    <div align='right' style='margin-bottom:10px;margin-right:32px'>
      <table>
        <tr>
          <td>
            <div align="right" id="${elements.btnVoltar}" style='background-color:${colorD};width:90px;color:white;font-size:12px' class="button"> VOLTAR </div>
          </td>
          <td width='20px'>
          </td>
          <td>
            <div align="right" id="${elements.btnAvancar}" style='background-color:${colorD};width:90px;color:white;font-size:12px' class="button"> AVANÇAR </div>
          </td>
        </tr>
      </table>
      <br>
      <br>
    </div>`
    return `${MasterPageHome.getHtml(_page)}`;
  }
}
