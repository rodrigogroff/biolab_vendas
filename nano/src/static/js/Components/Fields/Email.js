
export default class {
  static getHtml(id, msg) {
    return `<div class="form-row no-padding">
                <table width='100%'>
                    <tr>
                        <td width='20px'>
                            <img src='src/static/img/email.png' alt='Email' id='fail${id}' style='padding-top:6px' />
                        </td>
                        <td>
                            <input id="${id}" type="text" class="form-element" placeholder="${msg}">
                        </td>
                    </tr>
                </table>
            </div>`;
  }
}
