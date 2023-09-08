
export default class {
  static getHtml(page) {
    return `<div class="wrapper" align="center" style='overflow-y:auto;'><main><section class="container"><br>
            <div id='mainFormApp'>${page}</div></section></main></div>`;
  }
}
