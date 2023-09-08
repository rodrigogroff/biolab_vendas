import Controller from "./CtrlLogin";
const router = () => {document.getElementById("myApp").innerHTML = new Controller().getHtml(); };
window.addEventListener("popstate", router);
document.addEventListener("DOMContentLoaded", () => {router();});
