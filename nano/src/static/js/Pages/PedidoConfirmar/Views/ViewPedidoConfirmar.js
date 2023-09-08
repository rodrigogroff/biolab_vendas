import MyCss from "./base.css.js";
import MasterPageHome from "@app/Components/Views/MasterPageHome";

export default class {

  static m_elements() {
    return {
      repPos: "repPos",
      repPre: "repPre",
      labelNumPedido: "labelNumPedido",
      report: "report",
      reportDist: "reportDist",
      btnTalao: "btnTalao",
      btnEnviar: "btnEnviar",
      btnVoltar: "btnVoltar",
      btnSalvar: "btnSalvar",
      btnVoltarDist: "btnVoltarDist",
      labelTotApresentacoes: "labelTotApresentacoes",
      labelTotUnidades: "labelTotUnidades",
      labelDescMedio: "labelDescMedio",
      labelTotBruto: "labelTotBruto",
      labelTotLiq: "labelTotLiq",
    };
  }

  static getHtml() {
    var elements = this.m_elements();
    var colorA = localStorage.getItem("paletaCorA")
    var colorC = localStorage.getItem("paletaCorC")
    var colorD = localStorage.getItem("paletaCorD")
    var pdv = sessionStorage.getItem('pdv');
    var _page = `
    <style>${MyCss.getHtml()}</style>
    <div align='left' style='margin-top:10px;margin-bottom:10px;margin-left:16px'>
      <h1>| Pedido <span id='${elements.labelTipoPedido}'></span></h1>
    </div>
    <style>
      .separator { height: 1px;  background-color: ${colorA}; }
    </style>
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
          <div align='center' style='background-color:${colorC};width:100%;height:100%'>
            <br>
            <p style='background-color:${colorD};color:white;width:16px;border-radius: 50%'>4</p>
            <p style='color:white;font-size:12px'>Validação e Envio de pedido</p>
            <br>
            <br>
          </div>
        </td>
      </tr>
    </table>
    <div style='height:100%;min-height:420px;display:none' id='${elements.repPos}' >
      <div style='margin-left:20px' align='left'>
        <p style='color:${colorA};font-size:14px;padding-top:8px'>Pedido <span id='${elements.labelNumPedido}' style='color:black'></span> realizado!</p>
      </div>
    </div>
    <div style='height:100%;min-height:420px;display:block' id='${elements.repPre}'>
      <div style='margin-left:20px' align='left'>
        <table width='100%'>
          <tr>
            <td valign='top' width='400px'>
              <p style='color:${colorA};font-size:14px;padding-top:8px'>PDV: <span style='color:black'>${pdv}</span></p>
            </td>
            <td valign='top' width='32px'>
            </td>
            <td valign='top' width='200px'>
              <p style='color:${colorA};font-size:14px;padding-top:8px'>DISTRIBUIDOR:</p>
              <div id='${elements.reportDist}' style='margin-top:-8px' />
            </td>
            <td valign='top' width='300px'>
              <div align="right" style='margin-right:26px'>
                <br>
                <div align="right" id="${elements.btnTalao}" style='background-color:${colorD};width:90px;color:white;font-size:12px;width:200px' class="button"> CONFERIR TALÃO DE OFERTAS </div>
              </div>
            </td>
          </tr>
        </table>
      </div>
      <br>
      <div id='${elements.report}' style='margin-right:26px;margin-left:26px'>
      </div>
      <br>
      <div align='right' style='margin-bottom:10px;margin-right:32px;font-size:12px'>
        <table width="100%">
          <tr>
            <td width='20%' align='center' style='font-weight:bold;color:${colorA}'>
              TOTAL DE APRESENTAÇÕES
            </td>
            <td width='20%' align='center' style='font-weight:bold;color:${colorA}'>
              TOTAL DE UNIDADES
            </td>
            <td width='20%' align='center' style='font-weight:bold;color:${colorA}'>
              DESCONTO MÉDIO
            </td>
            <td align='right' width='25%' style='font-weight:bold;color:${colorA}'>
              TOTAL BRUTO
            </td>
            <td align='right' width='25%' style='font-weight:bold;color:${colorA}'>
              TOTAL LIQUIDO
            </td>
          </tr>
          <tr>
            <td colspan="5" class="separator"></td>
          </tr>
          <tr>
            <td width='20%' align='center'>
              <div id='${elements.labelTotApresentacoes}'>0</div>
            </td>
            <td width='20%' align='center'>
              <div id='${elements.labelTotUnidades}'>0</div>
            </td>
            <td width='20%' align='center'>
              <div id='${elements.labelDescMedio}'>0 %</div>
            </td>
            <td width='20%' align='right'>
              <div id='${elements.labelTotBruto}'>R$ 0,00</div>
            </td>
            <td width='20%' align='right'>
              <div id='${elements.labelTotLiq}'>R$ 0,00</div>
            </td>
          </tr>
        </table>
      </div>
      <br>
      <div align='right' style='margin-bottom:10px;margin-right:32px'>
        <table>
          <tr>
            <td>
              <div align="right" id="${elements.btnVoltarDist}" style='background-color:${colorD};width:300px;color:white;font-size:12px' class="button"> VOLTAR PARA SELEÇÃO DE DISTRIBUIDORES </div>
            </td>
            <td width='20px'>
            <td>
              <div align="right" id="${elements.btnSalvar}" style='background-color:${colorD};width:90px;color:white;font-size:12px' class="button"> SALVAR </div>
            </td>
            <td width='20px'>
            <td>
              <div align="right" id="${elements.btnVoltar}" style='background-color:${colorD};width:90px;color:white;font-size:12px' class="button"> VOLTAR </div>
            </td>
            <td width='20px'>
            </td>
            <td>
              <div align="right" id="${elements.btnEnviar}" style='background-color:${colorD};width:90px;color:white;font-size:12px' class="button"> AVANÇAR </div>
            </td>
          </tr>
        </table>
        <br>
        <br>
      </div>
    </div>`
    return `${MasterPageHome.getHtml(_page)}`;
  }
}
