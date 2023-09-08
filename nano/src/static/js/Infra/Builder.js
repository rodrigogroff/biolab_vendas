
export function updateHTML(idElement, html) {
  $("#" + idElement).html(html);
}

export function buildCheckLine(chk, id, name) {
  return "<table><tr><td><input type='checkbox' id='" + chk + id +
    "' value='2' /></td><td width='8px'></td><td><label for='" + chk + id + "'>" +
    name + "</label></td></tr></table>";
}

export function removeOptions(selectElement) {
  let i, L = selectElement.options.length - 1;
  for (i = L; i >= 0; i--) {
    selectElement.remove(i);
  }
}

export function buildSelectOption(text, val) {
  let a = document.createElement("option");
  a.setAttribute("value", val);
  let b = document.createTextNode(text);
  a.appendChild(b);
  return a;
}

export function buildTableSimple(tableobj, style, separator) {
  let lineData = "";
  if (tableobj.data.length > 0) {
    let size = tableobj.header.length;
    lineData = "<table class='table' id='" + tableobj.id + "' style='" + style + "'><thead><tr>";
    for (let h = 0; h < size; ++h)
      lineData +=
        //"<th align='left' width='" + tableobj.sizes[h] + "px'>" + tableobj.header[h] + "</th > ";
        "<th align='left'><span style='color:" + tableobj.titleColor + "'>" + tableobj.header[h] + "</span></th> ";
    lineData += "</tr></thead><tbody>";
    for (let d = 0; d < tableobj.data.length; ++d) {
      let ar = tableobj.data[d];
      lineData += "<tr>";
      for (let h = 0; h < size; ++h)
        lineData +=
          "<td width='" +tableobj.sizes[h] + "px' id='" +
          d +
          "' _par_table='" +
          tableobj.id +
          "' valign='top'>" +
          ar[h] +
          "</td>";
      lineData += "</tr>";
      if (separator != undefined) {
        lineData += separator
      }
    }
    lineData += "</tbody></table>";
  }
  else
    lineData = "<br><p>Nenhum registro encontrado</p><br>";
  return lineData;
}
