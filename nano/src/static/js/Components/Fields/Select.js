
export default class {
  static getHtml(id, label) {
    return `<label for='${id}' style='padding-left:20px'>${label}</label>
            <div style="margin-left:20px;margin-right:10px;margin-top:6px">
              <select id='${id}' />                        
            </div>`
  }
}
