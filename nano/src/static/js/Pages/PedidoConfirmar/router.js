import Controller from "./CtrlPedidoConfirmar";
const router = () => {document.getElementById("myApp").innerHTML = new Controller().getHtml(); };
window.addEventListener("popstate", router);
document.addEventListener("DOMContentLoaded", () => {router();});
