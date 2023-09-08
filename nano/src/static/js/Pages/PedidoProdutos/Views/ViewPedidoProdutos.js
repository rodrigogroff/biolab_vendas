import MyCss from "./base.css.js";
import MasterPageHome from "@app/Components/Views/MasterPageHome";

export default class {

  static m_elements() {
    return {
      labelTipoPedido: "labelTipoPedido",
      reportDist: "reportDist",
      reportProdutos: "reportProdutos",
      reportTipos: "reportTipos",
      btnVoltar: "btnVoltar",
      btnAvancar: "btnAvancar",
      btnTalao: "btnTalao",
      txtPesquisa: "txtPesquisa",
      imgLupa: "imgLupa",
      imgLupaPic: "src/static/img/icon-loupe.png",
      btnFiltro_resumo: "btnFiltro_resumo",
      btnFiltro_destaque: "btnFiltro_destaque",
      btnFiltro_compra_recente: "btnFiltro_compra_recente",
      btnFiltro_compra_mais: "btnFiltro_compra_mais",
      btnFiltro_cupom: "btnFiltro_cupom",
      labelTotApresentacoes: "labelTotApresentacoes",
      labelTotUnidades: "labelTotUnidades",
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
          <div align='center' style='background-color:${colorC};width:100%;height:100%'>
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
        <table width='100%'>
          <tr>
            <td>
              <div style='margin-top:20px'>
                <table style='background-color:#EAEAEA' width='480px'>
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
                      <input id="${elements.txtPesquisa}" type='text' placeholder='Pesquisar Produtos (Insira laboratório, Descrição ou EAN)' style='width:400px;outline: none; background-color:#EAEAEA;border:none;margin-left:16px'>
                    </td>
                  </tr>
                </table>
              </div>
            </td>
            <td>
              <div align="right" style='margin-right:26px'>
                <br>
                <table>
                  <tr>
                    <td>
                      <div align="right" id="${elements.btnOfertasMixIdeal}" style='background-color:${colorD};width:160px;color:white;font-size:12px' class="button"> OFERTAS DE MIX IDEAL </div>
                    </td>
                    <td>
                      <div align="right" id="${elements.btnOfertasMixIdeal}" style='background-color:${colorD};width:140px;color:white;font-size:12px' class="button"> CARREGAR PRODUTOS </div>
                    </td>
                    <td>
                      <div align="right" id="${elements.btnOfertasMixIdeal}" style='background-color:${colorD};width:130px;color:white;font-size:12px' class="button"> VISUALIZAR COMBOS </div>
                    </td>
                  </tr>
                </table>
              </div>
            </td>
          </tr>
        </table>
        <br>
        <div style='background-color:${colorA};width:100%'>
          <div style='padding-top:10px;padding-bottom:14px'>
            <div id='${elements.reportTipos}'></div>
          </div>
        </div>
        <br>
        <table>
          <tr>
            <td valign='top'>
              <style> .filtros { font-size:10px;cursor:pointer; padding-top: 8px; padding-bottom: 8px; } </style>
              <p style='color:${colorA}'>Última compra</p><div style='margin-top:-30px;color:${colorA}'>_________________</div><br>
              <p style='color:${colorA}'>Filtros</p><div style='margin-top:-30px;color:${colorA}'>_________________</div>
              <p id='${elements.btnFiltro_resumo}' class='filtros'>Resumo</p>
              <p id='${elements.btnFiltro_destaque}' class='filtros'>Produtos em destaque</p>
              <p id='${elements.btnFiltro_compra_recente}' class='filtros'>Compra recente</p>
              <p id='${elements.btnFiltro_compra_mais}' class='filtros'>Compra Mais 30d</p>
              <p id='${elements.btnFiltro_cupom}' class='filtros'>Cupom de desconto</p>
            </td>
            <td width='20px'></td>
            <td valign='top'>
              <br>
              <div id='${elements.reportProdutos}'></div>
            </td>
          </tr>
        </table>
        <br>
        <div align='right' style='margin-bottom:10px;margin-right:32px;font-size:12px'>
          <table width="100%">
            <tr>
              <td width='25%' align='center' style='font-weight:bold'>
                TOTAL DE APRESENTAÇÕES
              </td>
              <td width='25%' align='center' style='font-weight:bold'>
                TOTAL DE UNIDADES
              </td>
              <td align='right' width='25%' style='font-weight:bold'>
                TOTAL BRUTO
              </td>
              <td align='right' width='25%' style='font-weight:bold'>
                TOTAL LIQUIDO
              </td>
            </tr>
            <tr>
              <td colspan="4" class="separator"></td>
            </tr>
            <tr>
              <td width='25%' align='center'>
                <div id='${elements.labelTotApresentacoes}'>0</div>
              </td>
              <td width='25%' align='center'>
                <div id='${elements.labelTotUnidades}'>0</div>
              </td>
              <td width='25%' align='right'>
                <div id='${elements.labelTotBruto}'>R$ 0,00</div>
              </td>
              <td width='25%' align='right'>
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
        </div>
      </div>`
    return `${MasterPageHome.getHtml(_page)}`;
  }
}
