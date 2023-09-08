
export function IsLoading() {
  return $("#loading")[0].style.display != "none";
}

export function loadingOn(btn) {
  $("#loading").show();
}

export function loadingOff(btn) {
  $("#loading").hide();
}

