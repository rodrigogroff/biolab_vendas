import { isAuthenticated } from "@app/Infra/Auth";
import { updateHTML } from "@app/Infra/Builder"
import { displaySystemPopup, CheckPopUpCloseClick, displaySystemPopupLoading, closeSystemPopupLoading } from "@app/Infra/PopupControl"
import { Endpoints } from "@app/Infra/Endpoints";
import { postTokenPortal } from "@app/Infra/Api";
import MasterPageHome from "@app/Components/Views/MasterPageHome";
import MyForm from "./Views/ViewPedidoDist";

export default class {

  getHtml() {
    return MyForm.getHtml();
  }

  constructor() {
    $(document).ready(function () {
      var elements = MyForm.m_elements();
      var colorA = localStorage.getItem("paletaCorA")
      isAuthenticated();
      MasterPageHome.update();
      // atualiza tipo de pedido
      document.getElementById(elements.labelTipoPedido).textContent = sessionStorage.getItem("tipoPedido");
      // buscar distribuidores
      displaySystemPopupLoading();
      postTokenPortal(Endpoints().pdvDistInfo, { IdPdv: sessionStorage.getItem('pdvId') } )
        .then((resp) => {
          closeSystemPopupLoading();
          if (resp.ok == true) {

            var item_style = 'font-size:11px;height:26px;cursor:pointer;color:black;margin-left:12px;margin-right:12px;height:32px'
            var span_style = 'margin-left:8px;'

            // se já temos dados persistidos
            if (sessionStorage.getItem('dists') != undefined) {
              // tipo de pagto
              var mySelect = document.getElementById(elements.selPagto)
              var optionToSelect = sessionStorage.getItem('selPagto')
              for (var i = 0; i < mySelect.options.length; i++) {
                  if (mySelect.options[i].value === optionToSelect) {
                    mySelect.options[i].selected = true;
                    break;
                  }
                }
              // distribuidores selecionados
              var dists = JSON.parse(sessionStorage.getItem('dists'))
              var repHtml_right = "";
              for(let i=0; i < dists.length; i= i+ 1) {
                let o = dists[i];
                repHtml_right += `<p id='repeater' dist_id='${o.dist_id}' fantasia='${o.fantasia}' style='${item_style}'><br><span style='${span_style}'>${o.fantasia}</span></p>`
              }
              updateHTML(elements.reportRepeaterDistSel, repHtml_right);
              // distribuidores disponiveis
              var repHtml_left = "";
              for(let i=0; i < resp.payload.length; i= i+ 1) {
                let o  = resp.payload[i];
                var found = false;
                for(let j=0; j < dists.length; j= j+ 1) {
                  var y = dists[j]
                  if (y.dist_id == o.id)
                    found = true;
                }
                if (found == false)
                  repHtml_left += `<p id='repeater' dist_id='${o.id}'fantasia='${o.nomeFantasia}'style='${item_style}'><br><span style='${span_style}'>${o.nomeFantasia}</span></p>`
              }
              updateHTML(elements.reportRepeaterDistPref, repHtml_left);
            }
            // colocar tudo para disponiveis
            else {
              var repHtml_left = "";
              for(let i=0; i < resp.payload.length; i= i+ 1) {
                let o  = resp.payload[i];
                repHtml_left += `<p id='repeater'dist_id='${o.id}'fantasia='${o.nomeFantasia}'style='${item_style}'><br><span style='${span_style}'>${o.nomeFantasia}</span></p>`
              }
              updateHTML(elements.reportRepeaterDistPref, repHtml_left);
            }
          }
        })
        .catch((resp) => {
          console.log(resp)
          closeSystemPopupLoading();
          displaySystemPopup(resp.msg);
        });
    });

    document.body.addEventListener("click", (e) => {
      if (CheckPopUpCloseClick(e))
        return;
      MasterPageHome.checkClicks(e)
      // --------------------------------------- page clicks ---------------------------------------

      // selecionar um distribuidor para ação
      if (e.target != null) {
        if (e.target.id == 'repeater') {
          var obj = e.target
          var colorA = localStorage.getItem("paletaCorA")
          if (obj.style.backgroundColor === 'white' || obj.style.backgroundColor === '') {
              obj.style.color = 'white'
              obj.style.backgroundColor = colorA;
          } else {
              obj.style.backgroundColor = 'white';
              obj.style.color = 'black'
          }
        }
      }

      var elements = MyForm.m_elements();
      // ir para proxima tela
      if (e.target.id == elements.btnAvancar) {
        // confere combo
        var selPagto = document.getElementById(elements.selPagto)
        if (selPagto.value != 1) {
          displaySystemPopup("Somente venda à vista nesta POC")
          return;
        }
        var rightDists = document.getElementById(elements.reportRepeaterDistSel);
        var dists = [];
        for(let i=0; i < rightDists.childNodes.length; i = i + 1) {
          let obj = rightDists.childNodes[i];
          if (obj.attributes != undefined) {
            dists.push({
              dist_id: obj.attributes.getNamedItem("dist_id").value,
              fantasia: obj.attributes.getNamedItem("fantasia").value,
            })
          }
        }
        // confere distribuidores selecionados
        if (dists.length == 0) {
          displaySystemPopup("Escolha um distribuidor")
          return;
        }
        // ir para próxima tela
        sessionStorage.setItem('selPagto',selPagto.value)
        sessionStorage.setItem('dists',  JSON.stringify(dists))
        window.location.href = '/pedidoProdutos'
      }
      // voltar para tela inicial de pedido
      if (e.target.id == elements.btnVoltar) {
        window.location.href = '/pedido'
        return;
      }
      // mover itens selecionados da esquerda pra direita
      if (e.target.id == elements.btnMoveOneRight) {
        var left_div = document.getElementById(elements.reportRepeaterDistPref);
        var rightDists = document.getElementById(elements.reportRepeaterDistSel);
        var lst = []
        for(let i=0; i < left_div.childNodes.length; i = i + 1) {
          let obj = left_div.childNodes[i];
          if (obj.style != undefined)
            if (obj.style.backgroundColor != "")
              lst.push(obj)
        }
        for (let u=0; u < lst.length; u= u + 1) {
          let obj = lst[u]
          left_div.removeChild(obj)
          rightDists.appendChild(obj)
          obj.backgroundColor = 'white'
        }
      }
      // mover todos itens da esquerda pra direita
      if (e.target.id == elements.btnMoveAllRight) {
        var left_div = document.getElementById(elements.reportRepeaterDistPref);
        var rightDists = document.getElementById(elements.reportRepeaterDistSel);
        var lst = []
        for(let i=0; i < left_div.childNodes.length; i = i + 1) {
          lst.push(left_div.childNodes[i])
        }
        for (let u=0; u < lst.length; u= u + 1) {
          let obj = lst[u]
          left_div.removeChild(obj)
          rightDists.appendChild(obj)
          obj.backgroundColor = 'white'
        }
      }
      // mover um distribuidor selecionado para disponível
      if (e.target.id == elements.btnMoveOneLeft) {
        var left_div = document.getElementById(elements.reportRepeaterDistPref);
        var rightDists = document.getElementById(elements.reportRepeaterDistSel);
        var lst = []
        for(let i=0; i < rightDists.childNodes.length; i = i + 1) {
          let obj = rightDists.childNodes[i];
          if (obj.style != undefined)
            if (obj.style.backgroundColor != "")
              lst.push(obj)
        }
        for (let u=0; u < lst.length; u= u + 1) {
          let obj = lst[u]
          rightDists.removeChild(obj)
          obj.backgroundColor = ''
          left_div.appendChild(obj)
        }
      }
      // mover todos distribuidores selecionados para disponível
      if (e.target.id == elements.btnMoveAllLeft) {
        var left_div = document.getElementById(elements.reportRepeaterDistPref);
        var rightDists = document.getElementById(elements.reportRepeaterDistSel);
        var lst = []
        for(let i=0; i < rightDists.childNodes.length; i = i + 1) {
          lst.push(rightDists.childNodes[i])
        }
        for (let u=0; u < lst.length; u= u + 1) {
          let obj = lst[u]
          rightDists.removeChild(obj)
          left_div.appendChild(obj)
        }
      }


    });
  }
}
