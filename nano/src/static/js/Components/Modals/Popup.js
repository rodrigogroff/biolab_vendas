
export default class {
    static getHtml() {
        return `<div class="popup-overlay" id="popUpSystem" align='center'>
                  <div class="popup-container" style='margin-top:50px;max-width:350px'>
                      <div class="popup-header">
                          <table width='100%'>
                              <tr>
                                  <td width='95%'>
                                      <div align='center'>
                                          <span id='popUpSystemTitleOK' style='padding-left:20px;color:black'><b>Falha!</b></span>
                                      </div>
                                  </td>
                                  <td>
                                      <span id='popupClose' class="popup-closeOK" data-dismiss="true">X</span>                        
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
                                          <img src='src/static/img/error_big.png' style='width:60px;height:60px' alt=' ' />
                                      </div>                
                                  </td>
                              </tr>
                              <tr height="28px"><td></td></tr>
                              <tr>
                                  <td>
                                      <div align='center'>
                                          <span id='popUpSystemText' style="padding-top:16px;color:black" /><br><br>
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
