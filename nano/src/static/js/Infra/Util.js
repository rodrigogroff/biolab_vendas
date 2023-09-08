
export function displaySafeText(text, opt, flag) {
  if (text == null || text == undefined)
    return ''
  else {
    switch (opt) {
      case 'i': return '<i>' + text + '</i>'
      case 'red': return flag == false ? '<span style="color:red">' + text + '</span>' : text
      case 'href': return "<a href='" + text + "' target='_blank'>" + text + "</a>"
    }
    return text
  }
}

export function getParameterByName(name, _default) {
  let url = window.location.href;
  name = name.replace(/[\[\]]/g, "\\$&");
  let regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
    results = regex.exec(url);
  if (!results) return _default;
  if (!results[2]) return _default;
  return decodeURIComponent(results[2].replace(/\+/g, " "));
}

export function getFromStorage(tag) {
  return localStorage.getItem(tag);
}

export function setToStorage(tag, val) {
  localStorage.setItem(tag, val);
}

export function imageChange(id, img) {
  $(id).attr("src", "src/static/img/" + img);
}

export function disableButton(btn) {
  $(btn).prop("disabled", true);
}

export function enableButton(btn) {
  $(btn).prop("disabled", false);
}

export function getFromSession(tag) {
  return sessionStorage.getItem(tag);
}

export function setToSession(tag, val) {
  sessionStorage.setItem(tag, val);
}
