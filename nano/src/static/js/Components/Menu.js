
import {
  getUserLogged,
  isAdmin,
  isManager
} from "@app/Infra/Auth";

export default class {

  static getHtml() {

    var usr = getUserLogged();
    var admin = isAdmin();
    var manager = isManager();
    var version = process.env.VERSION;
    var curPage = window.location.href;

    var usrMsg = `<br><div style='color:white;padding-left:20px'>Bem-vindo <br><span style='color:white;font-size:large;font-weight:bold'>${usr.nome}</span></div>
                  <div align='left'><br><a href="/exit" style='color:white;cursor:pointer;padding-left:20px;text-align:right'>[Sair]</a>
                  <span style='color:grey;padding-left:25px;font-size:small'>${version}</span></div><br>`;

    var fullMenu = "";

    var b_clients = curPage.endsWith('/clients') || curPage.endsWith('/client')
    var b_users = curPage.endsWith('/users') || curPage.endsWith('/user')
    var b_interactiontype = curPage.endsWith('/interactiontypes') || curPage.endsWith('/interactiontype')
    var b_projects = curPage.endsWith('/projects') || curPage.endsWith('/projects')
    var b_squads = curPage.endsWith('/squads') || curPage.endsWith('/squad')
    var b_userprofiles = curPage.endsWith('/userprofiles') || curPage.endsWith('/userprofile')
    var b_companyUnits = curPage.endsWith('/companyUnits') || curPage.endsWith('/companyUnit')
    var b_usertypes = curPage.endsWith('/usertypes') || curPage.endsWith('/usertype')
    var b_password = curPage.endsWith('/password')

    var bAdministration = b_squads || b_users || b_userprofiles || b_usertypes

    if (admin == true)
      fullMenu += `<li ${bAdministration == true ? "class='active'" : ""}>
                    <a  href="javascript:void(0);"><b>Recursos Humanos</b></a>
                    <ul>
                      <li ${b_users == true ? "class='active'" : ""}><a href="/users"  style='cursor:pointer'>Usuários</a></li>
                      <li ${b_squads == true ? "class='active'" : ""}><a href="/squads"  style='cursor:pointer'>Equipes</a></li>
                      <li ${b_userprofiles == true ? "class='active'" : ""}><a href="/userprofiles"  style='cursor:pointer'>Perfis de usuário</a></li>
                      <li ${b_usertypes == true ? "class='active'" : ""}><a href="/usertypes"  style='cursor:pointer'>Tipos de contratação</a></li>
                    </ul>
                  </li>`

    var bManagement = b_clients || b_projects || b_interactiontype || b_companyUnits

    if (manager == true)
      fullMenu += `<li ${bManagement == true ? "class='active'" : ""}>
                    <a  href="javascript:void(0);"><b>Gestão Comercial</b></a>
                    <ul>
                      <li ${b_clients == true ? "class='active'" : ""}><a href="/clients"  style='cursor:pointer'>Clientes</a></li>
                      <li ${b_projects == true ? "class='active'" : ""}><a href="/projects"  style='cursor:pointer'>Projetos</a></li>
                      <li ${b_interactiontype == true ? "class='active'" : ""}><a href="/interactiontypes"  style='cursor:pointer'>Tipo Interação</a></li>
                      <li ${b_companyUnits == true ? "class='active'" : ""}><a href="/companyUnits"  style='cursor:pointer'>Unidades de negócio</a></li>
                    </ul>
                  </li>`

    var bUserOperations = b_password

    fullMenu += `<li ${bUserOperations == true ? "class='active'" : ""}>
                      <a  href="javascript:void(0);"><b>Minha Conta</b></a>
                        <ul>
                          <li ${b_password == true ? "class='active'" : ""}><a href="/password" style='cursor:pointer'>Troca de senha</a></li>
                        </ul>
                    </li>`;

    return `<div class="nav-menu" align='left'><nav class="menu">
              <div class="nav-container">
                ${usrMsg}<br><ul class="main-menu">${fullMenu}</ul><br><br><br>
              </div>
            </nav>
          </div>`;
  }
}
