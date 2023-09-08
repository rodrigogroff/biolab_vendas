
import { fadeIn, fadeOut } from "@app/Infra/ScreenFX";

export function CheckPopUpCloseClick(e) {
  if (e.target != null) {
    if (e.target.parentElement != null) {
      if (e.target.parentElement.childNodes != null) {
        if (e.target.parentElement.childNodes.length > 1) {
          if (e.target.parentElement.childNodes[1].id == "popupClose") {
            fadeOut($("#popUpSystem")[0], 120);
            return true;
          }
          if (e.target.parentElement.childNodes[1].id == "popupCloseOK") {
            fadeOut($("#popUpSystemOK")[0], 120);
            return true;
          }
        }
      }
    }
  }
  return false;
}

export function displaySystemPopup(_text) {
  let btn = sessionStorage.getItem("myBtn");
  $("#loading").hide();
  if (btn != undefined) $(btn).addClass("green");
  fadeIn($("#popUpSystem")[0], 60);
  $("#popUpSystemText").text(_text);
}

export function displaySystemPopupLoading() {
  fadeIn($("#popUpSystemLoading")[0], 60);
}

export function closeSystemPopupLoading() {
  fadeOut($("#popUpSystemLoading")[0], 120);
}

export function displaySystemPopupOK(_text) {
  if (_text == null || _text == "" || _text == undefined)
    _text = "Sistema atualizado!"
  let btn = sessionStorage.getItem("myBtn");
  $("#loading").hide();
  if (btn != undefined) $(btn).addClass("green");
  fadeIn($("#popUpSystemOK")[0], 60);
  $("#popUpSystemTextOK").text(_text);
}

