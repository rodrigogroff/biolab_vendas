
export default class {
  static getHtml(id, label) {
    var str = '<table style="padding-left:20px">';
    str += `<tr>
                <td width='150px'><label for='${id}'>${label}</label></td>
                <td>
                  <div class="form-row no-padding">
                    <input type='checkbox' style='height:22px' id='${id}'/>
                  </div>
                </td>
              </tr>`;
    str += '</table>'
    return str;
  }
}
