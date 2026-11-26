// Configuração usada em build de produção — dentro do Docker, o back-end
// será acessado pelo nome do serviço no docker-compose, não localhost.
export const environment = {
  production: true,
  apiUrl: 'http://api:8080/api',
};
