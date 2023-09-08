import { isAuthenticated } from "@app/Infra/Auth";
import { buildTableSimple, updateHTML } from "@app/Infra/Builder"
import { displaySystemPopup, CheckPopUpCloseClick, displaySystemPopupLoading,displaySystemPopupOK, closeSystemPopupLoading } from "@app/Infra/PopupControl"
import { formatMonetaryValue } from "@app/Components/Misc/Format"
import { Endpoints } from "@app/Infra/Endpoints";
import { postTokenPortal } from "@app/Infra/Api";
import MasterPageHome from "@app/Components/Views/MasterPageHome";
import MyForm from "./Views/ViewPedidoConfirmar";

export default class {

  getHtml() {
    return MyForm.getHtml();
  }

  constructor() {

    $(document).ready(function () {
      isAuthenticated();
      MasterPageHome.update();
      var elements = MyForm.m_elements();
      // atualiza tipo de pedido
      document.getElementById(elements.labelTipoPedido).textContent = sessionStorage.getItem("tipoPedido");
      // lista de distribuidores
      var dists = JSON.parse(sessionStorage.getItem('dists'))
      var rep_html = '';
      for(let i=0; i < dists.length; i = i + 1) {
        rep_html += "<p>" + dists[i].fantasia + '</p>'
      }
      updateHTML(elements.reportDist, rep_html);
      // relatório dos pedidos
      var mix_produtos = JSON.parse(sessionStorage.getItem("mix_produtos"))
      var pedido = JSON.parse(sessionStorage.getItem('pedido'));
      var htmlFinal = '';
      var colorA = localStorage.getItem("paletaCorA")
      var tot_bruto = 0;
      var tot_liq = 0;
      var tot_desc = 0;
      var tot_qtd = 0;
      var tot_ap = 0;
      // tabela de produtos
      for(let i=1; i <= dists.length; i = i + 1 )
      {
        htmlFinal += "<div align='left'><h1 style='color:" + colorA + "'>Pedido " + i + " - " + dists[i-1].fantasia + " - FORMA DE PAGAMENTO: A VISTA</h1></div>"
        let myTable = {
          id: 'table_report',
          header: ['LABORATÓRIO', 'FAMÍLIA', 'PRODUTO', 'QUANTIDADE', '<div align="right">DESCONTO<br>TOTAL</div>', '<div align="right">PREÇO</div>', '<div align="right">VALOR</div>'],
          sizes: [200, 90, 300, 60, 90, 90, 90],
          data: [],
          titleColor: localStorage.getItem("paletaCorA"),
        };

        for(let i = 0; i < pedido.length; i = i + 1 ) {
          var item = pedido[i]
          if (item.qtd.toString() != '0') {
            for(let j=0; j < mix_produtos.produtos.skus.length; j =j + 1) {
              var item_sku = mix_produtos.produtos.skus[j]
              if (item.ean == item_sku.ean)
              {
                  myTable.data.push([
                    item_sku.laboratorio,
                    item_sku.familia,
                    item_sku.descricao,
                    item.qtd,
                    '<div align="right">' + item.discount + "%</div>",
                    '<div align="right">' + item.bruto + '</div>',
                    '<div align="right">' + item.liq + '</div>',
                ]);
                tot_qtd += parseInt(item.qtd)
                tot_ap += 1
                tot_desc += parseFloat(item.discount)
                tot_bruto += parseFloat(pedido[i].bruto.toString().replace(',',".")) * parseFloat(item.qtd);
                tot_liq += parseFloat(pedido[i].liq.toString().replace(',',".")) * parseFloat(item.qtd);
                break;
              }
            }
          }
        }
        htmlFinal += buildTableSimple(myTable, 'font-size:10px;width:100%') + "<br>"
      }
      updateHTML(elements.report, htmlFinal);
      document.getElementById(elements.labelTotApresentacoes).textContent = tot_ap
      document.getElementById(elements.labelTotUnidades).textContent = tot_qtd
      document.getElementById(elements.labelDescMedio).textContent = parseFloat(tot_desc / tot_ap).toFixed(2).toString() + "%"
      document.getElementById(elements.labelTotBruto).textContent = formatMonetaryValue(tot_bruto.toFixed(2).toString())
      document.getElementById(elements.labelTotLiq).textContent = formatMonetaryValue(tot_liq.toFixed(2).toString())
    });

    document.body.addEventListener("click", (e) => {
      if (CheckPopUpCloseClick(e))
        return;
      MasterPageHome.checkClicks(e)
      var elements = MyForm.m_elements();
      // voltar ao tab anterior
      if (e.target.id == elements.btnVoltar) {
        window.location.href = '/pedidoProdutos'
        return;
      }
      // voltar ao tab de distribuidores
      if (e.target.id == elements.btnVoltarDist) {
        window.location.href = '/pedidoDist'
        return;
      }

      if (e.target.id == elements.btnEnviar) {
        displaySystemPopupLoading();

        var _param = {
          cnpj: sessionStorage.getItem('cnpj'),
          pdvId: sessionStorage.getItem('pdvId'),
          distribuidores: []
        }

        var dists = JSON.parse(sessionStorage.getItem('dists'))
        var pedido = JSON.parse(sessionStorage.getItem('pedido'))
        var mix_produtos = JSON.parse(sessionStorage.getItem('mix_produtos'))

        for(let i=0; i < dists.length; i = i + 1) {
          let _totalBrutoSKU = parseFloat(0)
          let _totalLiquidoSKU = parseFloat(0)
          let _totalUnidades = parseInt(0)
          let _totalApresentacoes = parseInt(0)

          for (let j=0; j < pedido.length; j = j + 1) {
            let qtd_pedido =  parseInt(pedido[j].qtd)
            if (qtd_pedido > 0)
            {
              _totalApresentacoes += 1
              _totalUnidades += qtd_pedido
              _totalBrutoSKU += parseFloat(pedido[j].bruto.toString().replace(',',".")) * parseFloat(qtd_pedido);
              _totalLiquidoSKU += parseFloat(pedido[j].liq.toString().replace(',',".")) * parseFloat(qtd_pedido);
            }
          }
          var _prazo = ""
          var _tipoPedido = sessionStorage.getItem('tipoPedido')
          if (_tipoPedido == "1")
            _prazo = "AVista";

          var m_dist = {
            DistribuidorId: dists[i].dist_id,
            NomeFantasia: dists[i].fantasia,
            Prazo: _prazo,
            IdPrazo: parseInt(sessionStorage.getItem('selPagto')),
            TipoPedido: _tipoPedido,
            totalBrutoSKU: _totalBrutoSKU,
            totalLiquidoSKU: _totalLiquidoSKU,
            totalUnidades: _totalUnidades,
            pedidos: []
          }
          for (let j=0; j < pedido.length; j = j + 1) {
            let ped = pedido[j]
            for (let k=0; k < mix_produtos.produtos.skus.length; k = k + 1) {
              var o = mix_produtos.produtos.skus[k]
              if (o.ean == ped.ean) {
                o.Qtd = ped.qtd,
                o.discount = ped.discount,
                o.bruto = ped.bruto,
                o.liq = ped.liq,
                m_dist.pedidos.push(o)
                break;
              }
            }
          }
          _param.distribuidores.push(m_dist)
        }

        postTokenPortal(Endpoints().mixProdutosEnvio, _param )
          .then((resp) => {
            closeSystemPopupLoading();
            if (resp.ok == true) {
              displaySystemPopupOK( "Pedido " + resp.payload.pedidos[0].numeroPedido + " confirmado!");
              document.getElementById(elements.repPre).style.display = 'none'
              document.getElementById(elements.repPos).style.display = 'block'
              document.getElementById(elements.labelNumPedido).textContent = resp.payload.pedidos[0].numeroPedido;
            }
          })
          .catch((resp) => {
            console.log(resp)
            closeSystemPopupLoading();
            displaySystemPopup(resp.msg);
          });
        }
    });

  }
}
