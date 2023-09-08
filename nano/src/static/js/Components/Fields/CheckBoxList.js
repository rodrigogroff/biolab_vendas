
export default class {
  static getHtml(list) {
    var str = '<table>';
    for (let i = 0; i < list.length; i = i + 1) {
      var obj = list[i];
      var id = obj.id;
      var label = obj.label;
      str += `<tr>
                <td width='150px'><label for='${id}'>${label}</label></td>
                <td>
                  <div class="form-row no-padding">
                    <input type='checkbox' style='height:22px' id='${id}'/>
                  </div>
                </td>
              </tr>`;
    }
    str += '</table>'
    return str;
  }
}
