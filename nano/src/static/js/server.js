
var fs = require('fs');
var http = require('http');
var https = require('https');

var privateKey = '';
var certificate = '';

if (fs.existsSync('poweradmin.com.br.key')) {
  //openssl x509 -text -in poweradmin.com.br.pem -out poweradmin.com.br.crt
  privateKey = fs.readFileSync('poweradmin.com.br.key', 'utf8');
  certificate = fs.readFileSync('poweradmin.com.br.crt', 'utf8');
}

var credentials = { key: privateKey, cert: certificate };

const compression = require('compression');
const express = require("express");
const path = require("path");
const app = express();

const shouldCompress = (req, res) => {
  if (req.headers['x-no-compression']) return false;
  return compression.filter(req, res);
};

app.use(compression({ filter: shouldCompress, threshold: 0 }));
app.use("/src", express.static(path.resolve(__dirname, "src")));

app.get("/", (req, res) => { res.sendFile(path.resolve(__dirname, "./index_dashboard.html")); });
app.get("/login", (req, res) => { res.sendFile(path.resolve(__dirname, "./index_login.html")); });
app.get("/pedido", (req, res) => { res.sendFile(path.resolve(__dirname, "./index_pedido.html")); });
app.get("/pedidoDist", (req, res) => { res.sendFile(path.resolve(__dirname, "./index_pedidoDist.html")); });
app.get("/pedidoProdutos", (req, res) => { res.sendFile(path.resolve(__dirname, "./index_pedidoProdutos.html")); });
app.get("/pedidoConfirmar", (req, res) => { res.sendFile(path.resolve(__dirname, "./index_pedidoConfirmar.html")); });

const ports = [10961,10962];

const servers = ports.map((port) => {
  const server = http.createServer(app);
  server.listen(port, () => {
    console.log(`Server is running on port ${port}`);
  });
  return server;
});
