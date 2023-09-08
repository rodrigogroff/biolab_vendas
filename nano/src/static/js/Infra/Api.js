
export function getLocation() {
  let srvTag = "__API_indexPos";
  let _idx = localStorage.getItem(srvTag);
  if (_idx == null) _idx = 0;
  let indexPos = parseInt(_idx);
  let lstNodes = JSON.parse(process.env.API_NODES);
  indexPos++;
  if (indexPos >= lstNodes.length) indexPos = 0;
  localStorage.setItem(srvTag, indexPos);
  return lstNodes[indexPos];
}

export function getTokenPortal(location, parameters) {
  let ApiLocation = getLocation();
  let ret = new Promise((resolve, reject) => {
    let _params = "";
    if (parameters !== null) _params = "?" + parameters;
    fetch(
      ApiLocation.api_host +
      ":" +
      ApiLocation.api_port +
      "/api/" +
      location +
      _params,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + localStorage.getItem("token"),
        },
      }
    )
      .then((res) => {
        if (res.status === 401) {
          resolve({
            ok: false,
            unauthorized: true,
          });
        } else if (res.status === 400) {
          res.json().then((data) => {
            reject({
              ok: false,
              msg: data.message,
            });
          });
        } else if (res.ok === true) {
          res.json().then((data) => {
            resolve({
              ok: true,
              payload: data,
            });
          });
        } else
          res.json().then((data) => {
            resolve({
              ok: false,
              msg: data.message,
            });
          });
      })
      .catch((errorMsg) => {
        ret = false;
        resolve({
          ok: false,
          msg: errorMsg.message,
        });
      });
  });
  return ret;
}

export function postTokenPortalFile(location, formData) {
  let ApiLocation = getLocation();
  return new Promise((resolve, reject) => {
    fetch(ApiLocation.api_host + ":" + ApiLocation.api_port + "/api/" + location,
      {
        method: 'POST',
        headers: { Authorization: "Bearer " + localStorage.getItem("token") },
        body: formData
      })
      .then((res) => {
        if (res.status === 401) {
          reject({
            ok: false,
            unauthorized: true,
          });
        } else if (res.status === 400) {
          res.json().then((data) => {
            reject({
              ok: false,
              msg: data.message,
            });
          });
        } else if (res.status === 204) {
          resolve({
            ok: true,
            payload: {},
          });
        } else if (res.ok === true) {
          res.json().then((data) => {
            resolve({
              ok: true,
              payload: data,
            });
          });
        } else
          res.json().then((data) => {
            resolve({
              ok: false,
              msg: data.message,
            });
          });
      })
      .catch((errorMsg) => {
        resolve({
          ok: false,
          msg: errorMsg.toString(),
        });
      });
  });
}

export function postTokenPortal(location, _obj) {
  let ApiLocation = getLocation();
  let obj = JSON.stringify(_obj);
  return new Promise((resolve, reject) => {
    fetch(
      ApiLocation.api_host + ":" + ApiLocation.api_port + "/api/" + location,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + localStorage.getItem("token"),
        },
        body: obj,
      }
    )
      .then((res) => {
        if (res.status === 401) {
          reject({
            ok: false,
            unauthorized: true,
          });
        } else if (res.status === 400) {
          res.json().then((data) => {
            reject({
              ok: false,
              msg: data.message,
            });
          });
        } else if (res.status === 204) {
          resolve({
            ok: true,
            payload: {},
          });
        } else if (res.ok === true) {
          res.json().then((data) => {
            resolve({
              ok: true,
              payload: data,
            });
          });
        } else
          res.json().then((data) => {
            resolve({
              ok: false,
              msg: data.message,
            });
          });
      })
      .catch((errorMsg) => {
        resolve({
          ok: false,
          msg: errorMsg.toString(),
        });
      });
  });
}

export function postPublicPortal(location, _obj) {
  let ApiLocation = getLocation();
  let obj = JSON.stringify(_obj);
  return new Promise((resolve, reject) => {
    fetch(
      ApiLocation.api_host + ":" + ApiLocation.api_port + "/api/" + location,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
        },
        body: obj,
      }
    )
      .then((res) => {
        if (res.status === 400) {
          res.json().then((data) => {
            reject({
              ok: false,
              msg: data.message,
            });
          });
        }
        else if (res.status === 204) {
          resolve({
            ok: true,
            payload: {},
          });
        }
        else if (res.ok === true) {
          res.json().then((data) => {
            resolve({
              ok: true,
              payload: data,
            });
          });
        } else {
          res.json().then((data) => {
            resolve({
              ok: false,
              msg: data.message,
            });
          });
        }
      })
      .catch((errorMsg) => {
        resolve({
          ok: false,
          msg: errorMsg.toString(),
        });
      });
  });
}

export function getPublicPortal(location) {
  let ApiLocation = getLocation();
  return new Promise((resolve, reject) => {
    fetch(
      ApiLocation.api_host + ":" + ApiLocation.api_port + "/api/" + location,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      }
    )
      .then((res) => {
        if (res.status === 400) {
          res.json().then((data) => {
            reject({
              ok: false,
              msg: data.message,
            });
          });
        }
        else if (res.status === 204) {
          resolve({
            ok: true,
            payload: {},
          });
        }
        else if (res.ok === true) {
          res.json().then((data) => {
            resolve({
              ok: true,
              payload: data,
            });
          });
        } else {
          res.json().then((data) => {
            resolve({
              ok: false,
              msg: data.message,
            });
          });
        }
      })
      .catch((errorMsg) => {
        resolve({
          ok: false,
          msg: errorMsg.toString(),
        });
      });
  });
}
