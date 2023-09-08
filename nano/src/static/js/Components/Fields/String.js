
export default class {
  static getHtml(id, maxLength, label, isNumeric, placeholderMsg) {
    if (placeholderMsg == undefined)
      placeholderMsg = '';
    var mx = 50;
    if (maxLength != null && maxLength != undefined)
      mx = maxLength;
    var numeric = '';
    if (isNumeric == true)
      numeric = 'type="tel" pattern="[0-9]*" inputmode="numeric"';
    if (label != null && label != undefined)
      label = `<label for='${id}' style='padding-left:20px'>${label}</label>`
    else
      label = '';
    return `${label}<div style="margin-right:10px;margin-top:6px">
              <div class="form-row no-padding">
                <input id="${id}" type="text" class="form-element" ${numeric} placeholder="${placeholderMsg}" maxlength='${mx}'>
              </div>
            </div>`;
  }
}
