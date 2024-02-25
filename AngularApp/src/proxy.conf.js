const PROXY_CONFIG = [
  {
    context: [
      "/Customer",
    ],
    target: "https://localhost:7204",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
