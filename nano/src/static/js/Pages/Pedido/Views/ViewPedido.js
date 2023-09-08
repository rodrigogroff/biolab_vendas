import MyCss from "./base.css.js";
import MasterPageHome from "@app/Components/Views/MasterPageHome";

export default class {

  static m_elements() {
    return {
      report: "report",
      reportRepeater: "reportRepeater",
      txtPDV: "txtPDV",
      imgLupa: "imgLupa",
      imgLupaPic: "src/static/img/icon-loupe.png",
      tipoPedidos: "tipoPedidos",
      imgPedidoRep: "imgPedidoRep",
      imgPedidoRepPic: "src/static/img/pedido_rep.png",
    };
  }

  static getHtml() {
    var elements = this.m_elements();
    var colorA = localStorage.getItem("paletaCorA")
    var colorC = localStorage.getItem("paletaCorC")
    var colorD = localStorage.getItem("paletaCorD")
    var _page = `
    <style>${MyCss.getHtml()}</style>
    <div align='left' style='margin-top:10px;margin-bottom:10px;margin-left:16px'>
      <h1>| Pedido</h1>
    </div>
    <table width='100%' style='border-spacing: 0;'>
      <tr>
        <td width='25%' style='padding:0;margin:0;'>
          <div align='center' style='background-color:${colorC};width:100%;height:100%'>
            <br>
            <p style='background-color:${colorD};color:white;width:16px;border-radius: 50%'>1</p>
            <p style='color:white;font-size:12px'>Seleção de PDV e Ofertas</p>
            <br>
            <br>
          </div>
        </td>
        <td width='25%' style='padding:0;margin:0;'>
          <div align='center' style='background-color:${colorA};width:100%;height:100%'>
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
    <div style='height:280px'>
      <div style='margin-left:20px' align='left'>
        <br>
        <p style='color:${colorA}'>Pesquisar PDV (97.222.376/0001-82)</p>
        <table style='background-color:#EAEAEA' width='80%'>
          <tr>
            <td width='32px'>
              <img
                id="${elements.imgLupa}"
                src='${elements.imgLupaPic}'
                srcset="${elements.imgLupaPic} 1x"
                loading="lazy"
                style='margin-left:8px'
                width='16'
                height='16'/>
            </td>
            <td>
              <input
                  id="${elements.txtPDV}"
                  type='text'
                  placeholder='Digite a Bandeira, Bandeira Indústria, CNPJ, Nome Fantasia ou Razão Social'
                  style='outline: none;background-color:#EAEAEA;border:none;margin-left:16px;width:400px'>
              <div style='background-color:EAEAEA;position:absolute;width:865px;margin-top:-12-px;display:none' id='${elements.report}'>
                <div id='${elements.reportRepeater}' />
              </div>
            </td>
          </tr>
        </table>
        <br>
        <br>
        <div id="${elements.tipoPedidos}" style="display:none">
          <img
                id="${elements.imgPedidoRep}"
                src='${elements.imgPedidoRepPic}'
                srcset="${elements.imgPedidoRepPic} 1x"
                loading="lazy"
                style='margin-left:8px;cursor:pointer'
                onclick="window.location.href = '/pedidoDist'"
                width='307'
                height='92'/>
        </div>
      </div>
    </div>`
    return `${MasterPageHome.getHtml(_page)}`;
  }
}
