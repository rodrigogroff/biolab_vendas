
export default class {
    static getHtml() {
        return `<div class="popup-overlay" id="popUpSystemLoading" align='center'>
                  <div class="popup-container" style='margin-top:50px;max-width:350px'>
                      <div class="popup-header">
                          <table width='100%'>
                              <tr>
                                  <td width='95%'>
                                      <div align='center'>
                                          <span id='popUpSystemTitleOK' style='padding-left:20px;color:black'><b>Sistema</b></span>
                                      </div>
                                  </td>
                                  <td>

                                  </td>
                              </tr>
                          </table>
                      </div>
                      <div class="popup-content">
                          <table>
                              <tr height="28px"><td></td></tr>
                              <tr>
                                  <td>
                                      <div align='center'>
                                          <img src='src/static/img/information.png' style='width:60px;height:60px' alt=' ' />
                                      </div>
                                  </td>
                              </tr>
                              <tr height="22px"><td></td></tr>
                              <tr>
                                  <td>
                                      <div align='center'>
                                        Carregando...
                                        <div align='center'><br><div><span class="loadingio-spinner-spinner-4rb4hgyrsge">
                                        <div class="ldio-k0jb5gkv3kn"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div>
                                        </div>
                                        </span><br>
                                        </div>
                                        </div>
                                      </div>
                                  </td>
                              </tr>
                              <tr height="16px"><td></td></tr>
                          </table>
                      </div>
                  </div>
              </div>`;
    }
}
