import { isAuthenticated } from "@app/Infra/Auth";
import { buildTableSimple, updateHTML } from "@app/Infra/Builder"
import { displaySystemPopup, CheckPopUpCloseClick, displaySystemPopupLoading, closeSystemPopupLoading } from "@app/Infra/PopupControl"
import { RetDescontoBase, formatMonetaryValue, applyDiscountAndFormat, numberValue } from "@app/Components/Misc/Format"
import { Endpoints } from "@app/Infra/Endpoints";
import { postTokenPortal } from "@app/Infra/Api";
import MasterPageHome from "@app/Components/Views/MasterPageHome";
import MyForm from "./Views/ViewPedidoProdutos";

export default class {

  getHtml() {
    return MyForm.getHtml();
  }

  constructor() {

    $(document).ready(function () {
      isAuthenticated();
      MasterPageHome.update();
      var elements = MyForm.m_elements();
      const inputElement = document.getElementById(elements.txtPesquisa);
      inputElement.addEventListener("paste", (event) => {
        event.preventDefault();
        var x = document.getElementById(elements.txtPesquisa);
        const pastedText = event.clipboardData.getData("text/plain");
        x.value = pastedText;
        generateReport();
      });
      inputElement.addEventListener("input", (event) => {
        generateReport();
      });
      document.getElementById(elements.labelTipoPedido).textContent = sessionStorage.getItem("tipoPedido");
      var dists = JSON.parse(sessionStorage.getItem('dists'))
      var rep_html = '';
      for(let i=0; i < dists.length; i = i + 1) {
        rep_html += "<p>" + dists[i].fantasia + '</p>'
      }
      updateHTML(elements.reportDist, rep_html);
      sessionStorage.setItem("index_page", 1);
      generateReport();
      generateTiposProdutos()
    });

    $(document).on("keydown", function (e) {
      switch (e.keyCode) {
        case 13: generateReport(); break; // enter no filtro de pesquisa
      }
    });

    document.body.addEventListener("click", (e) => {
      if (CheckPopUpCloseClick(e))
        return;
      MasterPageHome.checkClicks(e)
      var elements = MyForm.m_elements();
      var colorD = localStorage.getItem("paletaCorD")
      if (e.target.id == "btnUp") {
        var ean = e.target.attributes.getNamedItem("ean").value;
        var tag = "prod" + ean;
        var textBox = document.getElementById(tag)
        let currentValue = parseInt(textBox.value);
        if (isNaN(currentValue)) textBox.value = 1; else textBox.value = currentValue + 1;
        increaseEan(ean, 1);
        return;
      }
      if (e.target.id == "btnDown") {
        var ean = e.target.attributes.getNamedItem("ean").value;
        var tag = "prod" + ean;
        var textBox = document.getElementById(tag)
        let currentValue = parseInt(textBox.value);
        if (isNaN(currentValue)) {
          textBox.value = '';
        } else if (currentValue >= 1) {
          textBox.value = currentValue - 1;
          if (textBox.value == '0')
            textBox.value = '';
        }
        decreaseEan(ean, 1);
      }
      if (e.target.id == elements.btnVoltar) {
        window.location.href = '/pedidoDist'
        return;
      }
      if (e.target.id == elements.btnAvancar) {
        window.location.href = '/pedidoConfirmar'
        return;
      }
      if (e.target.id == elements.btnFiltro_resumo ||
        e.target.id == elements.btnFiltro_destaque ||
        e.target.id == elements.btnFiltro_compra_recente ||
        e.target.id == elements.btnFiltro_compra_mais ||
        e.target.id == elements.btnFiltro_cupom) {
        var obj = e.target;
        if (obj.style.backgroundColor === '') obj.style.backgroundColor = '#EAEAEA'; else obj.style.backgroundColor = '';
        generateReport();
      }
      if (e.target.id == 'tipo') {
        var obj = e.target;
        if (obj.style.backgroundColor === '') obj.style.backgroundColor = colorD; else obj.style.backgroundColor = '';
        generateReport();
      }
      if (e.target.id == 'nav_produtos') {
        sessionStorage.setItem("index_page", e.target.attributes[2].nodeValue);
        generateReport();
      }
    });

    function updateTotals() {
      var elements = MyForm.m_elements();
      var pedido = JSON.parse(sessionStorage.getItem('pedido'))
      var ap = 0;
      var tot_qtd = 0;
      var tot_bruto = parseFloat(0);
      var tot_liq = parseFloat(0);
      if (pedido != null && pedido != undefined) {
        for(let i=0; i < pedido.length;i =i +1) {
          var qtd_pedido = parseInt(pedido[i].qtd)
          tot_qtd += qtd_pedido;
          if (qtd_pedido > 0)
            ap = ap + 1;
          tot_bruto += parseFloat(pedido[i].bruto.toString().replace(',',".")) * parseFloat(qtd_pedido);
          tot_liq += parseFloat(pedido[i].liq.toString().replace(',',".")) * parseFloat(qtd_pedido);
        }
      }
      document.getElementById(elements.labelTotApresentacoes).textContent = ap;
      document.getElementById(elements.labelTotUnidades).textContent = tot_qtd;
      document.getElementById(elements.labelTotBruto).textContent = formatMonetaryValue(tot_bruto.toFixed(2).toString())
      document.getElementById(elements.labelTotLiq).textContent = formatMonetaryValue(tot_liq.toFixed(2).toString())
    }

    function increaseEan(ean, qtd) {
      var pedido = sessionStorage.getItem('pedido');
      if (pedido == null) pedido = []; else pedido = JSON.parse(sessionStorage.getItem('pedido'))
      var found = false;
      for(let i=0; i < pedido.length; i = i + 1) {
        var o = pedido[i];
        if (o.ean == ean) {
          o.qtd += parseInt(qtd)
          found = true;
          break;
        }
      }
      if (!found) {
        pedido.push({
          ean: ean,
          qtd: parseInt(qtd),
          discount: document.getElementById('desc' + ean).innerText.replace("%",""),
          bruto: document.getElementById('bruto' + ean).innerText.substring(2).trim(),
          liq: document.getElementById('liq' + ean).innerText.substring(2).trim(),
        })
      }
      sessionStorage.setItem('pedido', JSON.stringify(pedido))
      updateTotals()
    }

    function decreaseEan(ean,qtd){
      var pedido = sessionStorage.getItem('pedido');
      if (pedido == null) pedido = []; else pedido = JSON.parse(sessionStorage.getItem('pedido'))
      for(let i=0; i < pedido.length; i = i + 1) {
        var o = pedido[i];
        if (o.ean == ean) {
          o.qtd = parseInt(o.qtd) - parseInt(qtd)
          if (o.qtd < 0)
            o.qtd = 0;
        }
      }
      sessionStorage.setItem('pedido', JSON.stringify(pedido))
      updateTotals()
    }

    function generateTiposProdutos() {
      var elements = MyForm.m_elements();
      var mix_produtos = JSON.parse(sessionStorage.getItem("mix_produtos"))
      var htmlTipos = `
      <style> .tipoProd { color:white; font-size:10px; cursor:pointer; margin-left:10px; margin-right:10px; padding-top:8px; padding-bottom:8px; padding-left:8px; padding-right:8px } </style
      <table><tr>`;
      mix_produtos.tiposProdutos.forEach(function (ar) {
        htmlTipos += "<td><span id='tipo' class='tipoProd' tipo_id='" + ar.idTipoProduto + "'>" + ar.descricao + "</span></td>"
      });
      htmlTipos += '</tr></table>';
      updateHTML(elements.reportTipos, htmlTipos)
    }

    function generateReport() {
      var elements = MyForm.m_elements();
      var curIndex = parseInt(sessionStorage.getItem('index_page'))
      var _param = {
        IdPdv: sessionStorage.getItem('pdvId'),
        IdPrazoPagamento: sessionStorage.getItem('selPagto'),
        cnpj: sessionStorage.getItem('cnpj'),
        distribuidores: [],
        texto: document.getElementById(elements.txtPesquisa).value,
        destaque: document.getElementById(elements.btnFiltro_destaque).style.backgroundColor != '',
        recente: document.getElementById(elements.btnFiltro_compra_recente).style.backgroundColor != '',
        mais30: document.getElementById(elements.btnFiltro_compra_mais).style.backgroundColor != '',
        pageIndex: curIndex,
        pageSize: 20,
        IdTipoProduto: [],
      }
      var tipoProdSpans = document.getElementsByClassName('tipoProd');
      for(let i=0; i < tipoProdSpans.length; i = i + 1 ) {
        if (tipoProdSpans[i].style.backgroundColor != '')
          _param.IdTipoProduto.push(parseInt(tipoProdSpans[i].attributes[2].nodeValue))
      }
      var dists = JSON.parse(sessionStorage.getItem('dists'))
      for(let i=0; i < dists.length; i = i + 1) {
        _param.distribuidores.push({
          IdDistribuidor: dists[i].dist_id,
          Ordem: i
        })
      }
      displaySystemPopupLoading();
      postTokenPortal(Endpoints().mixProdutosPage, _param )
        .then((resp) => {
          closeSystemPopupLoading();
          if (resp.ok == true) {
            sessionStorage.setItem('mix_produtos', JSON.stringify(resp.payload))
            var elements = MyForm.m_elements();
            var mix_produtos = resp.payload;
            var pedido = JSON.parse(sessionStorage.getItem('pedido'));
            var colorA = localStorage.getItem("paletaCorA")
            var qtd = '';
            let myTable = {
              id: 'table_report',
              header: ['FAMÍLIA', 'PRODUTO', '<span style="padding-left:45px">QTDE</span>', 'DESC', 'RANGE', 'DISP.REP', 'DISP. GESTOR', 'DESC. ADICIONAL', 'PREÇO'],
              sizes: [90, 300, 60, 30, 30, 50, 50, 50, 50],
              data: [],
              titleColor: localStorage.getItem("paletaCorA"),
            };
            for(let i = 0; i < mix_produtos.produtos.skus.length; i = i + 1 ) {
              var ar = mix_produtos.produtos.skus[i]
              var desc = RetDescontoBase(ar, 1)
              myTable.data.push([
                "<span style='color:" + colorA + "'>" + ar.familia + "</span>",
                "<span style='color:" + colorA + "'>" + ar.descricao + "</span><br>EAN: " + ar.ean,
                "<table class='tb2'><tr><td width='20px'>" +
                "<p id='btnDown' ean='" + ar.ean + "' style='color:white;background-color:" + colorA +";width:16px;border-radius: 50%;text-align:center'>-</p></td><td width='20px'>" +
                "<input type='text' id='prod" + ar.ean + "' style='width:26px;height:22px;font-size;12px' onKeyUp='money(this,ValorNum);' disabled value='" + qtd + "'/></td><td width='20px'>"+
                "<p id='btnUp' ean='" + ar.ean + "' style='color:white;background-color:" + colorA +";width:16px;border-radius: 50%;text-align:center'>+</p></td></tr></table>",//qtde
                "<br><br><span id='desc" + ar.ean + "'>" + desc + "%</span>", // desc
                "<br><br>%", // range
                "<br><br>0", // disp rep
                "<br><br>0", // disp gestor
                "<br><input type='text' id='prodDesc" + ar.ean + "' style='width:50px;height:22px;font-size;12px' onKeyUp='money(this,Valor);' value='0,00'/><br>" +
                  "<br>Total:" + desc + "%", // desc ad
                "<br><span id='bruto" + ar.ean + "' style='text-decoration: line-through;'>" + formatMonetaryValue(ar.preco.toString()) +
                "</span><br><span id='liq" + ar.ean + "' style='color:" + colorA + "'>" + applyDiscountAndFormat(ar.preco.toString(), desc) + "</span>", // preço
                ]);
            }
            var separator = `<tr><td colspan="10"><div width='100% style='height:1px;background-color:${colorA}'></div></tr>`
            var totais = '<br><p style="font-size:12px">Total de produtos: ' + resp.payload.totalItens + '<br><p style="font-size:14px">';
            for(let u=1; u <= resp.payload.totalPages; u = u + 1) {
              let col = '#CACACA'
              if (u == curIndex) col = "black"
              totais += "<span style='cursor:pointer;color:" + col + "' id='nav_produtos' tipo_id='" + u +"' style='cursor:pointer;color:" + col + "'>" + u + "</span> &nbsp;"
            }
            totais += "</p></p><br><br>"
            updateTotals();
            updateHTML(elements.reportProdutos, buildTableSimple(myTable, 'font-size:10px;width:100%', separator) + totais)
          }
        })
        .catch((resp) => {
          console.log('generateReport FAIL')
          console.log(resp)
          closeSystemPopupLoading();
          displaySystemPopup(resp.msg);
        });
    }
  }
}
