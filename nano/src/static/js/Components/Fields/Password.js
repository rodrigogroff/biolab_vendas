
export default class {
  static getHtml(id, msg, title, useIcon) {
    if (useIcon == true)
      return `<div class="form-row no-padding">
                <table width='100%'>
                    <tr>
                        <td valign='top' width='20px'>
                            <img src='src/static/img/lock.png' alt='Password Field' id='fail${id}' style='padding-top:9px' />
                        </td>
                        <td valign='top' width='90%'>
                            <input id="${id}" type="password" class="form-element" placeholder="${msg}">
                        </td>
                        <td valign='top' width='20px' style='padding-top:9px'>
                            <img src='src/static/img/eye.png' alt='Password Field' id='seePass${id}' title='${title}' style='padding-top:9px' />                            
                        </td>
                    </tr>
                </table>
            </div>`;
    else
      return `<div class="form-row no-padding">
            <table width='100%'>
                <tr>
                    <td valign='top' width='90%'>
                        <input id="${id}" type="password" class="form-element" placeholder="${msg}">
                    </td>
                    <td valign='top' width='20px' style='padding-top:9px'>
                        <img src='src/static/img/eye.png' alt='Password Field' id='seePass${id}' title='${title}' style='padding-top:9px' />                            
                    </td>
                </tr>
            </table>
        </div>`;
  }

  static btnSeePassword(id) {
    $(id).removeAttr("type");
    if ($("#seePass" + id)[0].src.indexOf("_err.png") > 0) {
      $("#seePass" + id).attr("src", "src/static/img/eye.png");
      $("#" + id).attr("type", "password");
    } else {
      $("#seePass" + id).attr("src", "src/static/img/eye_err.png");
      $("#" + id).attr("type", "text");
    }
    $("#" + id)[0].focus();
  }

}
