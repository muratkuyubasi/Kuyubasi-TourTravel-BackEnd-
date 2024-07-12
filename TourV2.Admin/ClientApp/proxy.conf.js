const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:48068';

const PROXY_CONFIG = [
  {
    context: [
      "/userHub",
      "/api/action",
      "/api/announcement",
      "/api/appsetting",
      "/api/country",
      "/api/dashboard",
      "/api/email",
      "/api/emailtemplate",
      "/api/home",
      "/api/loginaudit",
      "/api/menu",
      "/api/nlog",
      "/api/page",
      "/api/pageaction",
      "/api/role",
      "/api/user",
      "/api/category",
      "/api/period",
      "/api/region",
      "/api/departure",
      "/api/activetour",
      "/api/reservation",
      "/api/contact",
      "/api/tourComment",
      "/api/tour",
      "/api/FrontAnnouncement"



    ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
