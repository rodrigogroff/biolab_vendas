
export function logout() {
  localStorage.removeItem("token");
  localStorage.removeItem("nome");
  localStorage.removeItem("perfil");
  localStorage.removeItem("userId");
  localStorage.removeItem("perfilId");
}

export function isAdmin() {
  return localStorage.getItem("admin") == "true";
}

export function isManager() {
  return localStorage.getItem("manager") == "true";
}

export function loginOk(resp) {

  localStorage.setItem("token", resp.token);
  localStorage.setItem("nome", resp.user.nome);
  localStorage.setItem("perfil", resp.user.perfil);
  localStorage.setItem("userId", resp.user.userId);
  localStorage.setItem("perfilId", resp.user.perfilId);

  let dataNow = new Date();
  let minutes = 60 * 4;
  let tokenObsoleteDate = new Date(dataNow.getTime() + minutes * 60000);
  let maxTime = JSON.stringify(tokenObsoleteDate);
  localStorage.setItem("maxTime", maxTime);

  console.log("Logado")
}

export function isAuthenticated() {
  var ret = localStorage.getItem("token");

  if (ret == null || ret == undefined) {
    location.href = "/login";
    return null;
  }

  var strMaxTime = localStorage.getItem("maxTime");

  if (strMaxTime == null || strMaxTime == undefined) {
    location.href = "/login";
    return null;
  }

  var maxTime = new Date(JSON.parse(strMaxTime));
  var dataNow = new Date();

  if (dataNow > maxTime) {
    location.href = "/login";
    localStorage.setItem("warning", "Sessão expirada!");
    return null;
  }
}

export function getUserLogged() {
  var ret = localStorage.getItem("token");
  if (ret == null || ret == undefined) {
    return null;
  }
  return {
    email: localStorage.getItem("email"),
    nome: localStorage.getItem("name"),
  };
}
