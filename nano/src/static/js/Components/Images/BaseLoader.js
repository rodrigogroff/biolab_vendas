
export function BaseLoader(id, _style) {
  if (id == undefined) id = "loading";
  if (_style == undefined) _style = "display:none;";
  return `<div align='center'><br><div><span class="loadingio-spinner-spinner-4rb4hgyrsge" id='${id}' style="${_style}">
          <div class="ldio-k0jb5gkv3kn"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div>
          </div>
          </span><br>
          </div>
          </div>`;
}

export function BaseLoaderMin(id, _style) {
  if (id == undefined) id = "loading";
  if (_style == undefined) _style = "display:none;";
  return `<span class="loadingio-spinner-spinner-4rb4hgyrsge" id='${id}' style="${_style}">
          <div class="ldio-k0jb5gkv3kn"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div>
          </div></span>`;
}
