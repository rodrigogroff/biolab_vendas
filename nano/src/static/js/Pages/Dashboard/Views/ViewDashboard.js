import MyCss from "./base.css.js";
import MasterPageHome from "@app/Components/Views/MasterPageHome";

export default class {

  static m_elements() {
    return {
      // intentionally blank
    };
  }

  static getHtml() {
    var elements = this.m_elements();
    var _page = `<style>${MyCss.getHtml()}</style>`
    return `${MasterPageHome.getHtml(_page)}`;
  }
}
