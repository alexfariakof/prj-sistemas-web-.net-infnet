const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:5146';

const PROXY_CONFIG = [
  {
    context: [
      "/auth",
      "/customer",
      "/customer/myplaylist",
      "/merchant",
      "/album",
      "/band",
      "/playlist",
      "/music",
    ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;

