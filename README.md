# Backend about Forms Platform

## Before Cloning:

- Make sure to have the .NET 7 compatible version installed.
- Have Visual Studio 2022 IDE with .NET 7 support.

## In the Project:

1. Review the connection string
2. Clean the solution.
3. Restore package nuggets.
4. Rebuild the solution.

## Correr Backend con Base de Datos en Docker

Util para correr en MacOS y/o Linux.

1. Correr el archivo de docker-compose.

```bash
docker-compose up -d
```

2. Obtener el id del contenedor creado.

```bash
docker ps
```

3. Ejecutar el siguiente comando para crear tablas.

```bash
docker exec -it <docker-container-id> /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Passw0rd -i ./init/QueryBackendCas.sql
```
